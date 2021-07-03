using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MyNetAPI.Services
{
    public class GlobalService
    {
        public static string ClassToJoin<T>(T t) where T:class {
            StringBuilder sb = new StringBuilder();
            ///获取对象t中的所有属性
            var Properties = t.GetType().GetProperties();
            foreach (var propertyInfo in Properties) {
                var str = propertyInfo.Name + @"=" + propertyInfo.GetValue(t) + " ";
                sb.Append(str);
            }

            return sb.ToString();
        }
    }
}