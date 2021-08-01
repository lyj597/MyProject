using Castle.DynamicProxy;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCoreMVC.Models.utility
{
    public class TestInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine($"你正在调用方法 \"{invocation.Method.Name}\"  " +
                $"参数是 {string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray())}... ");

            invocation.Proceed();

            var returnType  = invocation.Method.ReturnType;
            if (returnType == typeof(Task)) {
                Console.WriteLine("异步方法!");
                Func<Task> res1 = async () => await (Task)invocation.ReturnValue;


            }

        }
    }
}
