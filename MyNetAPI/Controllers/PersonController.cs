using MyNetAPI.Models;
using MyNetAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyNetAPI.Controllers
{
    [RoutePrefix("api/Area/Person")]
    public class PersonController : ApiController
    {
        [Route("PersonGet1")]
        [HttpGet]
        public string PersonGet1(string Name,int Age)
        {
            return Name + Age;
        }

        [Route("PersonGet")]
        [HttpGet]
        public string PersonGet([FromUri]Person person) {
            return GlobalService.ClassToJoin<Person>(person);
        }

        [Route("PersonPost")]
        [HttpPost]
        public string PersonPost([FromBody]Person person) {
            return GlobalService.ClassToJoin<Person>(person);
        }

        //[Route("PersonPost2")]
        [HttpPost]
        public string PersonPost1(string Name, int Age)
        {
            return Name + Age;
        }

        [HttpPost]
        public IHttpActionResult PersonPostAction([FromBody] Person person) {

            return NotFound() ;
        }

    }
}
