using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCoreAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public HealthController(IConfiguration configuration) {
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult Index() {
            Console.WriteLine($"这是health检查  {_configuration["port"]}");
            return Content("");
        }
    }
}
