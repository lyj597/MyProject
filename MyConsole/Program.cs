using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string ConnectString = "Data Source=.;Initial Catalog=Test;User ID=sa;Password=sa1234";
            using (var _context = CoreDbContext.GetContext(ConnectString)) {
                var blog = _context.Blogs.Include(a=>a.Posts).First();
                _context.Blogs.Remove(blog);
                _context.SaveChanges();
            }
        }
    }
}
