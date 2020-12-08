using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donut_Deliverable1.Models
{
    public class EFStudentRepository : IStudentRepository
    {
        private AppDbContext context;
        private List<Student> _studentList;
        public EFStudentRepository(AppDbContext ctx)
        {
            context = ctx;
        }
        public IEnumerable<Student> Students
        {
            get
            {
                return context.Students;
            }
        }

        public Student GetStudent(int Id)
        {
            _studentList = context.Students.ToList();
            return _studentList.FirstOrDefault(e => e.Id == Id);
        }
    }
}
