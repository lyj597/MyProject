using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCoreMVC.MiddleWares
{
    public class MyMiddleware
    {
        public readonly RequestDelegate _next;

        public MyMiddleware(RequestDelegate next) {
            _next = next;
        }

        public async Task Invoke(HttpContext context) {

            //if (context.Request.Headers.ContainsKey("Bearer "))
            //{
            //    var token = context.Request.Headers.TryGetValue("Bearer ", out var outPut);
            //    var aa = outPut.FirstOrDefault().ToString();
            //    await _next(context);
            //    await context.Response.WriteAsync("<p>Response1</p>");//响应出去时逻辑，为了验证顺序性，输出一句话
            //}
            //else {
            //    //throw new Exception("Please Input Token");
            //    await context.Response.WriteAsync("Please Input Token");//响应出去时逻辑，为了验证顺序性，输出一句话
            //}
            await _next(context);

        }

    }
}
