using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MyCoreMVC.Service
{
    public class GlobalService
    {
        public  void IocPut() {
            var Properties = Assembly.GetEntryAssembly().GetType()?.GetProperties();
            foreach (var p in Properties.Where(a=>a.PropertyType.IsInterface)) { 
              
            }
        }
    }
}
