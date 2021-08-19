using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyCoreAPI.Models.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoreAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GoodsController : ControllerBase
    {
        private readonly ILogger<GoodsController> _logger;
        private readonly IConfiguration _configuration;
        private readonly RedisExchangeHelp _redisHelp;
        private readonly RedisHelp _redis;

        public GoodsController(ILogger<GoodsController> logger
            , IConfiguration configuration
            , RedisExchangeHelp redisHelp
            , RedisHelp redis
            )
        {
            _logger = logger;
            _configuration = configuration;
            _redisHelp = redisHelp;
            _redis = redis;
        }

        /// <summary>
        /// 抢购
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult FlushSales()
        {

            StringBuilder sb = new StringBuilder();
            //链接DB
            _redisHelp.getRedisDB();
            _redisHelp._redisDB.StringSet("Count", 10);

            List<Task> list = new List<Task>();

            var nums = Enumerable.Range(1, 1000);

            //通过并发去抢购
            Parallel.For(1, 1000, (i) =>
            {
                _redisHelp.RedisLock("Count", i.ToString());
                var num = (int)_redisHelp._redisDB.StringGet("Count");
                string message = "";
                if (num < 1)
                {
                    message = $"客户：{i.ToString()} 未抢到商品";
                    _redisHelp._redisDB.ListLeftPush("notGetGoodsList", message);
                }
                else
                {
                    message = $"客户：{i.ToString()} 抢到了商品";
                    _redisHelp._redisDB.StringDecrement("Count", 1);
                    _redisHelp._redisDB.ListLeftPush("getGoodsList", message);
                }
                _redisHelp.RedisUnLock("Count", i.ToString());
            });

            ////通过foreach去抢购
            //foreach (var i in nums)
            //{
            //    var task = Task.Run(() => {
            //        _redisHelp.RedisLock("Count", i.ToString());
            //        var num = (int)_redisHelp._redisDB.StringGet("Count");
            //        string message = "";
            //        if (num < 1)
            //        {
            //            message = $"客户：{i.ToString()} 未抢到商品";
            //            _redisHelp._redisDB.ListLeftPush("notGetGoodsList", message);
            //        }
            //        else
            //        {
            //            message = $"客户：{i.ToString()} 抢到了商品";
            //            _redisHelp._redisDB.StringDecrement("Count", 1);
            //            _redisHelp._redisDB.ListLeftPush("getGoodsList", message);
            //        }
            //        _redisHelp.RedisUnLock("Count", i.ToString());
            //    });
            //    list.Add(task);
            //}

            //Task.WaitAll(list.ToArray());

            var value = "";

            while ((value = _redisHelp._redisDB.ListLeftPop("getGoodsList")) != null)
            {
                sb.AppendLine(value);
            }

            while ((value = _redisHelp._redisDB.ListLeftPop("notGetGoodsList")) != null)
            {
                sb.AppendLine(value);
            }

            _redisHelp.Dispose();

            return Content(sb.ToString());
        }


    }
}
