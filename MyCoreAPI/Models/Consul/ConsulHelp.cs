using Consul;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCoreAPI.Models.Consul
{
    public static class ConsulHelp
    {
        
        public static void ConsulRegister(this IConfiguration configuration) {
            //实例化consul对象，默认8500端口和dc1
            ConsulClient client = new ConsulClient(a=> {
                a.Address = new Uri("http://localhost:8500/");
                a.Datacenter = "dc1";
            });

            string ip = configuration["ip"];
            string port = configuration["port"];
            int weight = string.IsNullOrEmpty(configuration["weight"]) ? 1 : int.Parse(configuration["weight"]);

            client.Agent.ServiceRegister(new AgentServiceRegistration()
            {
                ID = "server" + Guid.NewGuid(),
                Name = "MyCoreAPI",
                Address = ip,
                Port = int.Parse(port),
                Tags=new string[] { weight .ToString()},
                Check = new AgentServiceCheck()
                {
                    Interval = TimeSpan.FromSeconds(12), //间隔12秒，一次
                    HTTP = $"http://{ip}:{port}/api/Health/Index",
                    Timeout = TimeSpan.FromSeconds(5), //检测等待时间
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(60) //失败后多久移除,最小60秒
                }
            }) ;

        }

    }
}
