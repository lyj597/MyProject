using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyCoreAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoreAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            #region swagger
              services.AddSwaggerGen(c => {
                  c.SwaggerDoc("V1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "jwtSwagger", Version = "V1" });
                  var basepath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                  var xmlpath = Path.Combine(basepath, "MyCoreAPI.xml");//��ȡxml·��
                  c.IncludeXmlComments(xmlpath);

                  #region ���jwt
                  c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                  {
                      In=Microsoft.OpenApi.Models.ParameterLocation.Header,
                      Type=Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                      Description="���¿���������Ҫ��ӵ�jwt��ȨToken:Bearer Token",
                      Name="Authorization",
                      BearerFormat="JWT",
                      Scheme="Bearer"
                  });
                  c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                  {
                     { 
                        new OpenApiSecurityScheme{
                            Reference=new OpenApiReference{
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                          }
                        },new string[]{ }
                      }
                  });
                  #endregion
              });

            #endregion

            #region ע��JwT��֤
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                //��ȡappsettings����ֵ
                var jwtmodel = Configuration.GetSection(nameof(JwtIssuerOptions));
                var iss = jwtmodel[nameof(JwtIssuerOptions.Issuer)];
                var key = jwtmodel[nameof(JwtIssuerOptions.SecurityKey)];
                var audience = jwtmodel[nameof(JwtIssuerOptions.Audience)];
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,//�Ƿ���֤Issuer
                    ValidateAudience = true,//�Ƿ���֤Audience
                    ValidateLifetime = true,//�Ƿ���֤ʧЧʱ��
                    ClockSkew = TimeSpan.FromSeconds(30),
                    ValidateIssuerSigningKey = true,//�Ƿ���֤SecurityKey
                    ValidAudience = audience,//Audience
                    ValidIssuer = iss,//Issuer���������ǰ��ǩ��jwt������һ��
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))//�õ�SecurityKey
                };
            });
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region Swagger

            app.UseSwagger();
            app.UseSwaggerUI(x=> {
                x.SwaggerEndpoint("/swagger/V1/swagger.json", "MyCore API");
            });

            #endregion

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
