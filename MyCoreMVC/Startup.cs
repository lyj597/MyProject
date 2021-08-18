using Autofac;
using Autofac.Configuration;
using Autofac.Extras.DynamicProxy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using MyCoreMVC.Controllers;
using MyCoreMVC.MiddleWares;
using MyCoreMVC.Models;
using MyCoreMVC.Models.utility;
using MyCoreMVC.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MyCoreMVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
            LogHelper.Configure(); //使用前先配置
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // 或者将Controller加入到Services中
            services.AddControllersWithViews(options =>
            {
                //options.Filters.Add<MyExceptionFilter>();
                //options.Filters.Add<MyGlobalFilter>();
            })
                .AddControllersAsServices()
                ;

            //services.AddControllersWithViews(options =>
            //{
            //    //options.Filters.Add<MyExceptionFilter>();
            //    //options.Filters.Add<MyGlobalFilter>();
            //});

            services.AddSession();

            services.AddOptions();

            //services.AddMemoryCache(option =>
            //{

            //});

            //redis通过配置注入
            services.AddServiceStackRedisCache(Configuration.GetSection("ServiceStackRedisOptions"));




            services.Configure<FddSettings>(Configuration.GetSection("FddSettings"));

            #region IOC依赖注入
            //每次都是一个新的实例，即使相同请求也是一个新的实例

            //services.AddTransient<MyExceptionFilter>();

            //services.AddTransient<ITestService, TestService>();

            #endregion
        }

        //autofac
        public void ConfigureContainer(ContainerBuilder builder)
        {

            var controllerBaseType = typeof(ControllerBase);
            builder.RegisterAssemblyTypes(typeof(Program).Assembly)
                .Where(t => controllerBaseType.IsAssignableFrom(t) && t != controllerBaseType)
                .PropertiesAutowired();


            #region autofac 拦截器注入方式

            //1、首先注册拦截器
            builder.RegisterType<TestInterceptor>();

            //#region 控制器上添加拦截器
            //builder.RegisterType<HomeController>().EnableClassInterceptors().InterceptedBy(typeof(TestInterceptor));
            //#endregion


            #region 接口添加拦截器
            ////2.1、在注入接口,使用这种方式，需要在接口上添加[Intercept(typeof(TestInterceptor))]标签
            //builder.RegisterType<TestService>().EnableInterfaceInterceptors().As<ITestService>();


            //2.2、通过lamada表达式直接注入,使用该种方式，不需要添加[Intercept(typeof(TestInterceptor))]标签
            builder.RegisterType<TestService>().EnableInterfaceInterceptors().InterceptedBy(typeof(TestInterceptor)).As<ITestService>();
            builder.RegisterType<Test1Service>().EnableInterfaceInterceptors().InterceptedBy(typeof(TestInterceptor)).As<ITestService>();
            #endregion

            // 为不同的实现指定名称，这个比较简单，推荐
            //builder.RegisterType<TestService>().As<ITestService>();
            //builder.RegisterType<Test1Service>().As<ITestService>();


            #endregion


            //builder.RegisterType<Test1Service>().As<ITestService>();


        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            //自定义中间键
            //app.Use(next =>
            //{
            //    return new RequestDelegate(async context=> {
            //        if (context.Request.Headers.ContainsKey("Bearer "))
            //        {
            //            await next.Invoke(context);
            //        }
            //        else {
            //            await context.Response.WriteAsync("NoAuthration");
            //        }

            //    });
            //});

            var aa=Configuration["Logging:LogLevel:Default"];

            app.UseMiddleware<MyMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot"))
            //});

            app.UseStaticFiles(new StaticFileOptions() {
               FileProvider=new PhysicalFileProvider(Directory.GetCurrentDirectory() + @"/wwwroot")
            });


            app.UseRouting();

            app.UseSession();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index2}/{id?}");
            });
        }
    }
}
