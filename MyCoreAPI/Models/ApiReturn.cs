using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCoreAPI.Models
{
    public class ApiReturn<T> 
    {
        public string Code { get; set; }
        public string ErrMsg { get; set; }

        public T Data { get; set; }

    }
}
