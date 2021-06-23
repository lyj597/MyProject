using MyNetConsole.EF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNetConsole
{
    class Program
    {
        public static string ConnectString = "Data Source=.;Initial Catalog=Test;User ID=sa;Password=sa1234";

        static void Main(string[] args)
        {
            var existingBlog = new Blog { BlogId = 3, Name = "ADO.NET Blog" };

            using (ProductContext _context = new ProductContext(ConnectString)) {

                var con=_context.Database.Connection;
                if (con.State == ConnectionState.Open) { 
                
                }
                var blog = _context.Blogs.Find(1);

                // Make a modification to Name in the tracked entity
                blog.Name = "My Cool Blog2";

                ;

                var Blog = new Blog();



            }
            Console.Read();
        }

        public static void PrintValues(DbPropertyValues values)
        {
            foreach (var propertyName in values.PropertyNames)
            {
                Console.WriteLine("Property {0} has value {1}",
                                  propertyName, values[propertyName]);
            }
        }

    }
}
