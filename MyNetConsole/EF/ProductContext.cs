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
        //public DbSet<Course> Courses { get; set; }
        //public DbSet<Department> Departments { get; set; }

        public ProductContext(string ConnectString) : base(ConnectString) {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

    }
}
