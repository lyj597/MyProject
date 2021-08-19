using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyCoreAPI.Models.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyCoreAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IConfiguration _configuration;
        private readonly RedisExchangeHelp _redisHelp;
        private readonly RedisHelp _redis;

        public UserController(ILogger<UserController> logger
            , IConfiguration configuration
            , RedisExchangeHelp redisHelp
            , RedisHelp redis
            ) {
            _logger = logger;
            _configuration = configuration;
            _redisHelp = redisHelp;
            _redis = redis;

        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string getValue() {
            Console.WriteLine($"这是{_configuration["port"]}的 invoke");
      
            var url =new StringBuilder()
             .Append(HttpContext.Request.Scheme)
             .Append("://")
             .Append(HttpContext.Request.Host)
             .Append(HttpContext.Request.PathBase)
             .Append(HttpContext.Request.Path)
             .Append(HttpContext.Request.QueryString)
             .ToString();

            Console.WriteLine($"链接是{url}");
            _redisHelp.getRedisDB();
            _redisHelp.RedisLock("testcount", url);
            _redisHelp._redisDB.StringIncrement("testcount", 1);
            Thread.Sleep(100000);
            _redisHelp.RedisUnLock("testcount", url);
            _redisHelp.Dispose();
            return $"这是端口 {_configuration["port"]} 的 invoke";
        }

       


    }
}
