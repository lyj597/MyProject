using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyCoreAPI.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyCoreAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public string Gettoken([FromBody] LoginRequest request)
        {
            var jwtmodel = _configuration.GetSection(nameof(JwtIssuerOptions));
            var iss = jwtmodel[nameof(JwtIssuerOptions.Issuer)];
            var key = jwtmodel[nameof(JwtIssuerOptions.SecurityKey)];
            var audience = jwtmodel[nameof(JwtIssuerOptions.Audience)];
            if (request.LoginID == "admin" && request.LoginPwd == "1")
            {
                var claims = new[]{
                    new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                    new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddMinutes(30)).ToUnixTimeSeconds()}"),
                    new Claim(ClaimTypes.Name, request.LoginID),
                    new Claim("Role", "角色")
                };
                var m5dkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var creds = new SigningCredentials(m5dkey, SecurityAlgorithms.HmacSha256);//生成签名
                var jwttoken = new JwtSecurityToken(
                //颁发者
                issuer: iss,
                //接收者
                audience: audience,
                //参数
                claims: claims,
                //过期时间
                expires: DateTime.Now.AddMinutes(30),
                //证书签名
                signingCredentials: creds
                );
                var token = new JwtSecurityTokenHandler().WriteToken(jwttoken);//生成token
                return token;
            }
            return "密码错误";
        }
    }
}
