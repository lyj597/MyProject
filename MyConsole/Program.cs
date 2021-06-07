using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyConsole
{
    class Program
    {
        static void Main(string[] args)
        
        {
            using (var _dbcontext = new CoreDbContext()) {
                var aaa = _dbcontext.Person.Include(a => a.Courses.Select(p=>p.TestCourses)).ToList();                
            }
        }
    }
}
