using MyNetConsole.EF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using MyWCFDLL;

namespace MyNetConsole
{
    class Program
    {
        public static string ConnectString = "Data Source=.;Initial Catalog=Test;User ID=sa;Password=sa1234";

        static void Main(string[] args)
        {
            var url = @"http://localhost:1000/MyWCFDLL.UserService.svc";
            var fatory = WcfInvokeFactory.CreateServiceByUrl<IUserService>(url);
            var aaa= fatory.GetString();
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
