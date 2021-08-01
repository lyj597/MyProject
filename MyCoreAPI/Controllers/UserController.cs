using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCoreAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IConfiguration _configuration;

        public UserController(ILogger<UserController> logger
            , IConfiguration configuration
            ) {
            _logger = logger;
            _configuration = configuration;

        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string getValue() {
            //_logger.LogDebug($"这是{_configuration["port"]}的 invoke");
            Console.WriteLine($"这是{_configuration["port"]}的 invoke");
            return $"这是端口 {_configuration["port"]} 的 invoke";
        }
    }
}
