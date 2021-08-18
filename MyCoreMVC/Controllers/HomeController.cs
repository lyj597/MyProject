using Autofac.Extras.DynamicProxy;
//using Castle.Core.Configuration;
using Castle.DynamicProxy;
using log4net.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.ServiceStackRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyCoreMVC.Models;
using MyCoreMVC.Models.utility;
using MyCoreMVC.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyCoreMVC.Controllers
{
    //[MyControllerFilter]
    public class HomeController : Controller
    {
        private readonly ITestService _service;
        private readonly ITestService _service1;
        private readonly IOptions<FddSettings> _settings;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _cache;
        private readonly IServiceStackRedisCache _redisCache;


        public HomeController(
            IOptions<FddSettings> settings
            ,IConfiguration configuration
            , IServiceProvider provider
            ,IMemoryCache cache
            , IServiceStackRedisCache redisCache
            )
        {
            _configuration = configuration;
            // 如果知道注册的顺序，可以用这种方式，
            // 第一个注册是TestUtil1，所以这里返回TestService
            //_service = provider.GetServices<ITestService>().ElementAtOrDefault(0);
            //_service1 = provider.GetServices<ITestService>().ElementAtOrDefault(1);

            // 一般情况下用这种方式，指定成具体的类型 TestService

            //_service = provider.GetServices<ITestService>().SingleOrDefault(a => a.GetType() == typeof(TestService));
            //_service1 = provider.GetServices<ITestService>().SingleOrDefault(a => a.GetType() == typeof(Test1Service));
            //_service = service;
            //_service1 = service1;
            _settings = settings;
            _cache = cache;
            _redisCache = redisCache;
        }

        private async Task GetTest() {
           
        }
        private async Task GetTest2()
        {

        }

        public IActionResult Index2() {
            //var userString = this.HttpContext.Session.GetString("CurrUser");
            //var user=JsonConvert.DeserializeObject<Users>(userString);
            //ViewData["User"] = user;

            string key = string.Format(MemoryCacheKey.MemoryCacheSendEmail, "yujie.lin@fsg.com", "1111");

            _cache.Set<string>(key, "这是yujie.lin@fsg.com.cn的邮件",DateTimeOffset.Now.AddMinutes(1));

            _redisCache.Set("aa", new Users
            {
                IDNumber = "1111111",
                Password = "2222",
                UserName = "张三"
            });
            //var ret=_redisCache.Get<Users>("aa");


            var port= _configuration["Settings:Port"];
            this.ViewBag.Port = port;
            return View();
        }

        public IActionResult Index()
        {
            string key = string.Format(MemoryCacheKey.MemoryCacheSendEmail, "yujie.lin@fsg.com", "1111");

            ViewBag.message=_cache.Get<string>(key);
            return View();
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

      
    }
}
