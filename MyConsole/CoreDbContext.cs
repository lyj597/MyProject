using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyConsole
{
    public  class CoreDbContext:DbContext
    {
        public static string ConnectString = "Data Source=.;Initial Catalog=Test;User ID=sa;Password=sa1234";

        public DbSet<Person> Person { get; set; }

        public DbSet<Course> Course { get; set; }

        public DbSet<TestCourse> TestCourse { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectString);
        }
    }
}
