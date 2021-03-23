using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Donut_Deliverable1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System.IO;
using Microsoft.Azure;

namespace Donut_Deliverable1.Controllers
{
    public class StudentController : Controller
    {
        private IStudentRepository repository;
        private readonly AppDbContext context;
        private readonly CloudBlobClient _client;
        private readonly CloudBlobContainer _container;

        public StudentController(AppDbContext context, IStudentRepository repo)
        {
            this.context = context;
            repository = repo;
            var connString = "DefaultEndpointsProtocol=https;AccountName=thearcstorage;AccountKey=mLyac/N8Rb3J/1ZBauoEgO6BBtpZ4nZwGcEpWt7p0793E7VfvbYjFY5p8NrKjmcnWS+LduN4EpyKOUeu2GzUkA==;EndpointSuffix=core.windows.net";
            var account = CloudStorageAccount.Parse(connString);

            _client = account.CreateCloudBlobClient();
            _container = _client.GetContainerReference("studentimages");
        }
        public IActionResult StudentView()
        {
            string absolutepath = HttpContext.Request.Path;
            var lastPart = absolutepath.Split('/').Last();
            int studentId = Int32.Parse(lastPart);
            var students = repository.GetStudent(studentId);
            return View(students);
        }

        public ActionResult GetImage()
        {
            string absolutepath = Request.Headers["Referer"].ToString();
            var lastPart = absolutepath.Split('/').Last();
            int studentId = Int32.Parse(lastPart);
            var students = repository.GetStudent(studentId);
            byte[] imageByteData = students.studentImage;
            return File(imageByteData, "image/jpg");
        }

        public IActionResult ArchiveStudent()
        {
            string absolutepath = HttpContext.Request.Path;
            var lastPart = absolutepath.Split('/').Last();
            int studentId = Int32.Parse(lastPart);
            var students = repository.GetStudent(studentId);
            return View(students);
        }

        [HttpPost]
        public IActionResult ArchiveStudent(String answer)
        {
            string absolutepath = HttpContext.Request.Path;
            var lastPart = absolutepath.Split('/').Last();
            int studentId = Int32.Parse(lastPart);
            var students = repository.GetStudent(studentId);
            students.archived = true;
            context.Update(students);
            context.SaveChanges();
            return RedirectToAction("StudentSearch");
        }

        public IActionResult UnarchiveStudent()
        {
            string absolutepath = HttpContext.Request.Path;
            var lastPart = absolutepath.Split('/').Last();
            int studentId = Int32.Parse(lastPart);
            var students = repository.GetStudent(studentId);
            return View(students);
        }

        [HttpPost]
        public IActionResult UnarchiveStudent(String answer)
        {
            string absolutepath = HttpContext.Request.Path;
            var lastPart = absolutepath.Split('/').Last();
            int studentId = Int32.Parse(lastPart);
            var students = repository.GetStudent(studentId);
            students.archived = false;
            context.Update(students);
            context.SaveChanges();
            return RedirectToAction("StudentSearch");
        }

        public IActionResult StudentSearch()
        {
            var students = repository.Students.ToList();
            return View(students);
        }
        public IActionResult StudentSearchArchived()
        {
            var students = repository.Students.ToList();
            return View(students);
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student, List<IFormFile> studentImage)
        {
            foreach (var img in studentImage)
            {
                using(var stream = new MemoryStream())
                {
                    await img.CopyToAsync(stream);
                    student.studentImage = stream.ToArray();
                }
                var blockBlob = _container.GetBlockBlobReference(img.FileName);
                await blockBlob.UploadFromByteArrayAsync(student.studentImage, 0, 1);
            }
            student.archived = false;
            context.Add(student);
            context.SaveChanges();
            return RedirectToAction("StudentSearch");
        }

        public ActionResult Edit()
        {
            string absolutepath = HttpContext.Request.Path;
            var lastPart = absolutepath.Split('/').Last();
            int studentId = Int32.Parse(lastPart);
            var students = repository.GetStudent(studentId);
            return View(students);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Student student, List<IFormFile> studentImage)
        {
            foreach (var img in studentImage)
            {
                using (var stream = new MemoryStream())
                {
                    await img.CopyToAsync(stream);
                    student.studentImage = stream.ToArray();
                }
                var blockBlob = _container.GetBlockBlobReference(img.FileName);
                await blockBlob.UploadFromByteArrayAsync(student.studentImage, 0, 1);
            }
            context.Update(student);
            //context.Remove(student);
            context.SaveChanges();
            return RedirectToAction("StudentSearch");
        }

    }
}
