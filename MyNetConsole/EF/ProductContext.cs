using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNetConsole.EF
{
    public class ProductContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
