using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donut_Deliverable1.Models
{
    public class EFStudentRepository : IStudentRepository
    {
        private AppDbContext context;
        public EFStudentRepository(AppDbContext ctx)
        {
            context = ctx;
        }
        public IEnumerable<Student> Students => context.Students;
    }
}
