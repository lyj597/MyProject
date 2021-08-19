using Microsoft.Extensions.Configuration;
using ServiceStack.Redis;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyCoreAPI.Models.Redis
{
    /// <summary>
    /// 基于ServiceStack.redis的分布式锁
    /// </summary>
    public  class RedisHelp
    {
        private readonly string SingleServer;
        private readonly object _lock = new object();
        private readonly IConfiguration _configuration;

        public  RedisClient _client = null;

        public RedisHelp(IConfiguration configuration) {
            _configuration = configuration;
            SingleServer = configuration["RedisConfig:SingleServer"];
        }

        public RedisClient CreateRedisClient() {
            if (_client == null) {
                lock (_lock)
                {
                    if (_client == null) {
                        _client = new RedisClient(SingleServer);
                    }
                }
            }
            return _client;
        }

        /// <summary>
        /// redis加锁
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        public void RedisLock(string Key, string Value) {
            if (_client == null)
                return;
            var lockKey = Key  +  "RedisLock" ;
            while (true)
            {
                var keyRet = _client.SetNX(lockKey, Encoding.UTF8.GetBytes(Value));
                if (keyRet > 0) { //成功加锁
                    //添加过期时间
                    _client.Expire(lockKey, 120);
                    break;
                }
                Thread.Sleep(200);
            }
          
        }

        /// <summary>
        /// 取消分布式锁
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        public void RedisUnLock(string Key, string Value) {
            if (_client == null)
                return;
            var lockKey = Key + "RedisLock";
            while (true) {
                var lockvalue = _client.Get<string>(lockKey);
                if (lockvalue == Value) {
                    _client.Remove(lockKey);
                    break;
                }
                Thread.Sleep(200);
            }
           
           
        }

        /// <summary>
        /// 释放redis
        /// </summary>
        public void RedisDispose() {
            _client.Dispose();
        }

    }

    public class RedisExchangeHelp {
        private readonly ConnectionMultiplexer _connectionMultiplexer;
        private readonly IConfiguration _configuration;
        public IDatabase _redisDB = null;

        public RedisExchangeHelp(IConfiguration configuration) {
            _configuration = configuration;
            _connectionMultiplexer = ConnectionMultiplexer.Connect(configuration["RedisConfig:SingleServer"]);
        }

        /// <summary>
        /// 获取db，默认16个，默认0
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public IDatabase getRedisDB(int num=0) {
            _redisDB=_connectionMultiplexer.GetDatabase(num);
            return _redisDB;
        }

        /// <summary>
        /// 加分布式锁
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        public void RedisLock(string Key, string Value) {
            var lockKey = Key + "RedisLock";
            while (true) {
              var ret=  _redisDB.LockTake(lockKey, Value, TimeSpan.FromSeconds(120));
                //判断是否锁定，未锁定继续
                if (ret) {
                    break;
                }
                Thread.Sleep(10);
            }
        }

        /// <summary>
        /// 删除分布式锁
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        public void RedisUnLock(string Key, string Value)
        {
            var lockKey = Key + "RedisLock";
            while (true)
            {
                var ret = _redisDB.LockRelease(lockKey, Value);
                //判断是否锁定，未锁定继续
                if (ret)
                {
                    break;
                }
                Thread.Sleep(10);
            }
        }

        public void Dispose() {
            _connectionMultiplexer.Dispose();
        }

    }
}
