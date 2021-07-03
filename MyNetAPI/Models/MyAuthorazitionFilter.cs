using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MyNetAPI.Models
{
    public class MyAuthorazitionFilter : IAuthorizationFilter
    {
        public  bool AllowMultiple => true;

        public async Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            actionContext.Request.Headers.TryGetValues("Token ", out IEnumerable<string> Tokens);
            if (Tokens.Count() <= 0)
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            string token = Tokens.First();
            if (token != "admin") {
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}