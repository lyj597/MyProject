using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace MyCoreMVC.Models.utility
{
    public class MyExceptionFilter: ExceptionFilterAttribute
    {
        private readonly ILogger<MyExceptionFilter> logger;
        public MyExceptionFilter(ILogger<MyExceptionFilter> _logger) {
            logger = _logger;
        }
        public override void OnException(ExceptionContext context) {
            if (!context.ExceptionHandled) {
                LogHelper.Error(context.Exception.Message);
                context.Result = new JsonResult(
                    new { 
                        result=false,
                        msg=new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                    }
                );
                context.ExceptionHandled = true;
            }
        }
    }

    public class MyActionFilter : Attribute, IActionFilter, IOrderedFilter
    {
        public int Order { get; set; }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine($"{typeof(MyActionFilter)} OnActionExecuted");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"{typeof(MyActionFilter)} OnActionExecuting");
        }
    }

    public class MyControllerFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine($"{typeof(MyControllerFilter)} OnActionExecuted");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"{typeof(MyControllerFilter)} OnActionExecuting");
        }
    }

    public class MyGlobalFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine($"{typeof(MyGlobalFilter)} OnActionExecuted");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"{typeof(MyGlobalFilter)} OnActionExecuting");
        }
    }
}
