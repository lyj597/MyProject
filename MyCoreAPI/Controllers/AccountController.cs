using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyCoreAPI.Jwt;
using MyCoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCoreAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ITokenHelper tokenHelper;
        private IOptions<JWTConfig> _options;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_tokenHelper"></param>
        public AccountController(ITokenHelper _tokenHelper, IOptions<JWTConfig> options)
        {
            tokenHelper = _tokenHelper;
            _options = options;
        }


        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiReturn<TnToken> Login([FromBody] LoginInput input)
        {
            var ret = new ApiReturn<TnToken>();
            try
            {
                if (string.IsNullOrWhiteSpace(input.UserName) || string.IsNullOrWhiteSpace(input.Password))
                {
                    ret.Code = "400";
                    ret.ErrMsg = "用户名密码不能为空";
                    return ret;
                }

                if (input.UserName == "admin" && input.Password == "0000")
                {

                    ret.Code = "200";
                    ret.Data = tokenHelper.CreateToken<LoginInput>(input);
                }
                else {
                    ret.Code = "400";
                    ret.ErrMsg = "登录失败:用户名或密码不正确!";
                }
            }
            catch (Exception ex)
            {
                ret.Code = "400";
                ret.ErrMsg = "登录失败:" + ex.Message;
            }
            return ret;
        }

        /// <summary>
        /// 验证Token
        /// </summary>
        /// <param name="tokenStr">token</param>
        /// <returns></returns>
        [HttpGet]
        public ApiReturn<TnToken> ValiToken(string tokenStr)
        {
            var ret = new ApiReturn<TnToken>() { Data = new TnToken() };
            
            bool isvilidate = tokenHelper.ValiToken(tokenStr,(a) => a["iss"] == _options.Value.Issuer && a["aud"] == _options.Value.Audience);
            if (isvilidate)
            {
                ret.Code = "200";
                ret.Data.TokenStr = tokenStr;
            }
            else
            {
                ret.Code = "400";
                ret.ErrMsg = "Token验证失败";
            }
            return ret;
        }


        /// <summary>
        /// 验证Token 带返回状态
        /// </summary>
        /// <param name="tokenStr"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiReturn<TnToken> ValiTokenState(string tokenStr)
        {
            var ret = new ApiReturn<TnToken>();
            
            TokenType tokenType = tokenHelper.ValiTokenState(tokenStr, (a) => a["iss"] == _options.Value.Issuer && a["aud"] == _options.Value.Audience, null);
            if (tokenType == TokenType.Fail)
            {
                ret.Code = "400";
                ret.ErrMsg = "token验证失败";
                return ret;
            }
            if (tokenType == TokenType.Expired)
            {
                ret.Code = "500";
                ret.ErrMsg = "token已经过期";
                return ret;
            }

            ret.Code = "200";
            return ret;
        }


    }
}
