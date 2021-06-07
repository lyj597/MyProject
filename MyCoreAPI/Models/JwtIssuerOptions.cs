using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCoreAPI.Models
{
    public class JwtIssuerOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string ValidFor { get; set; }
        public string ValidAudience { get; set; }
        public string SecurityKey { get; set; }
    }
}
