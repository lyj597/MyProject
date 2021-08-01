using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCoreMVC.Service
{
    public class TestService : ITestService
    {

        public TestService() { 
          
        }

        public async Task SendEmail()
        {
            Console.WriteLine("TestService SendEmail成功");
             Task t= Task.Run(()=> {
               Console.WriteLine("异步方法");
            });
            await t;
        }

        public string Show()
        {
            return "TestService Show";
        }
    }
}
