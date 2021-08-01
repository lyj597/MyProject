using Autofac.Extras.DynamicProxy;
using MyCoreMVC.Models.utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCoreMVC.Service
{
    //[Intercept(typeof(TestInterceptor))]
    public interface ITestService
    {
        string Show();

         Task SendEmail();
    }
}
