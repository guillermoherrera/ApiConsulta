using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Helpers;
using WebApi.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog.Context;
using FinMejoraWebApi.Helpers;
using System.Collections.Generic;
using ApiConsulta.Services;
using ApiConsulta.Helpers;
using ApiConsulta;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private AppSettings appSettings;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                };
            });

            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ICarteraService, CarteraService>();
            services.AddScoped<IRenovacionService, RenovacionService>();
            services.AddScoped<IConsultaService, ConsultaService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(appSettings.versionAPI, new OpenApiInfo
                {
                    Title = appSettings.nombreAPI,
                    Version = appSettings.versionAPI
                });

                c.AddSecurityDefinition(Constants.ApiKey, new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Name = Constants.ApiKey
                });


                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id= Constants.ApiKey
                            },
                            Scheme = Constants.ApiKey,
                            Name = Constants.ApiKey,
                            In = ParameterLocation.Header

                        }, new List<string>()
                    }
                });



                //c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                //{
                //    {
                //        new OpenApiSecurityScheme()
                //        {
                //            Reference = new OpenApiReference
                //            {
                //                Type= ReferenceType.SecurityScheme,
                //                Id = appSettings.scheme
                //            },
                //            Scheme = appSettings.scheme,
                //            Name = appSettings.scheme,
                //            In = ParameterLocation.Header
                //        }
                //        , new List<string>()
                //    }
                //});
            });

        }
                        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", appSettings.nombreAPI);
            });

            app.UseRouting();
                        
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseAPIValidation(appSettings);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}