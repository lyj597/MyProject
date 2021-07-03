using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using MyCoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCoreAPI.Jwt
{
    public class TokenFilter : Attribute, IActionFilter
    {

        private ITokenHelper tokenHelper;
        private IOptions<JWTConfig> _options;
        public TokenFilter(ITokenHelper _tokenHelper,IOptions<JWTConfig> options) //通过依赖注入得到数据访问层实例
        {
            tokenHelper = _tokenHelper;
            _options = options;
        }


        public void OnActionExecuted(ActionExecutedContext context)
        {
           
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            ApiReturn<string> ret = new ApiReturn<string>();
            var tokenString = context.HttpContext.Request.Headers["token"].ToString();
            if (tokenString == null) {
                ret.Code = "400";
                ret.ErrMsg = "Token不能为null";
                context.Result = new JsonResult(ret);
                return;
            }
            string token = tokenString.ToString();
            //验证jwt,同时取出来jwt里边的用户ID
            TokenType tokenType = tokenHelper.ValiTokenState(token, a => a["iss"] == _options.Value.Issuer && a["aud"] == _options.Value.Audience, null);
            if (tokenType == TokenType.Fail)
            {
                ret.Code = "400";
                ret.ErrMsg = "token验证失败";
                context.Result = new JsonResult(ret);
                return;
            }

            if (tokenType == TokenType.Expired)
            {
                ret.Code = "300";
                ret.ErrMsg = "token已经过期";
                context.Result = new JsonResult(ret);
            }
            //if (!string.IsNullOrEmpty(userId))
            //{
            //    //给控制器传递参数(需要什么参数其实可以做成可以配置的，在过滤器里边加字段即可)
            //    //context.ActionArguments.Add("userId", Convert.ToInt32(userId));
            //}

        }
    }
}
