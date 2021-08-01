using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCoreMVC.Service
{
    public class Test1Service : ITestService
    {

        public Test1Service() { 
           
        }

        public async Task SendEmail()
        {
            Console.WriteLine("Test1Service SendEmail 失败");
            Task t = Task.Run(() => {
                Console.WriteLine("异步方法");
            });
            await t;
        }      

        public string Show()
        {
            return "Test1Service Show";
        }
    }
}
