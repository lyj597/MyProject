using MyNetConsole.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNetConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ProductContext _context = new ProductContext()) {

                using (var tran = _context.Database.BeginTransaction()) {
                    tran.Commit();
                }
            }
            Console.Read();
        }
    }
}
