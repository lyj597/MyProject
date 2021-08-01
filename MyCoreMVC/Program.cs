using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MyCoreMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var hostBuilder = CreateHostBuilder(args);
            //var hostBuild = hostBuilder.Build();
            // hostBuild.Run();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)  //默认建造者
                .ConfigureWebHostDefaults(webBuilder =>  //指定web服务器---kerstrel，默认服务器
                {
                    //webBuilder.UseKestrel(a => a.Listen(IPAddress.Loopback, 12344))
                    //.Configure(app => app.Run(async context => await context.Response.WriteAsync("Hello World")))
                    //.UseIIS()
                    //.UseIISIntegration();
                    //;
                    webBuilder.UseStartup<Startup>();
                })
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            ;
    }
}
