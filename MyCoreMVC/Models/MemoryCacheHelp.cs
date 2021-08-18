using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCoreMVC.Models
{
    public class MemoryCacheHelp
    {
        //private readonly IMemoryCache _cache;
        //public MemoryCacheHelp(IMemoryCache cache) {
        //    _cache = cache;
        //}
    }

    public class MemoryCacheKey {
        public static string MemoryCacheSendEmail = "Onboarding_MemoryCache_SendEmail_{0}_{1}";

    }

}
