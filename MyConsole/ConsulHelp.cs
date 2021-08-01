using Consul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyConsole
{
    public class ConsulHelp
    {
        private readonly string Address = @"http://localhost:8500/";
        private readonly string Datacenter = @"dc1";

        public ConsulClient consulClient = null;

        public ConsulHelp(string url,string center)
        {
            var adrress = string.IsNullOrEmpty(url) ? Address : url;
            var cen = string.IsNullOrEmpty(center) ? Datacenter : center;

            consulClient = new ConsulClient(a => {
                a.Address = new Uri(adrress);
                a.Datacenter = cen;
            });

        }

        public AgentService getRandomService(string Name) {
            AgentService service = null;
            if (consulClient == null) {
                return service;
            }
            try
            {
                var allService = consulClient.Agent.Services().GetAwaiter().GetResult();
                var services = allService.Response.Values.Where(a => a.Service.Equals(Name, StringComparison.OrdinalIgnoreCase)).ToList();

                service = services[new Random(DateTime.Now.Millisecond).Next(0, services.Count() - 1)];
            }
            catch (Exception ex) { 
            
            }
            return service;
        }


    }
}
