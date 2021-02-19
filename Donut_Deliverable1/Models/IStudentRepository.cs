using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donut_Deliverable1.Models
{
    public interface IStudentRepository
    {
        IEnumerable<Student> Students { get; }
        Student GetStudent(int Id);
    }
}
