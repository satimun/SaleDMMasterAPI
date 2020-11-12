using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace SALEDM_API
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup()
        {
            var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //AllowCors
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders", builder =>
                {
                    builder
                    .AllowAnyOrigin()                                                                                                                     
                    .WithOrigins("http://localhost:8080", "http://127.0.0.1:8887/", "http://assetapi.kkfnets.com", "https://ASSETKKF.kkfnets.com", "https://kkfauditasset.kkfnets.com")
                    //.WithMethods("GET", "PUT", "POST", "DELETE")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithExposedHeaders("x-custom-header");
                });
            });

            
            //services.AddWebSocketManager();

            //services.AddHostedService<JobService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<IConfiguration>(Configuration);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //--set DB configuration 
            // 0 จริง 1 สำรอง  2 local
            switch (Configuration["DBMOD"])
            {
                case "1":
                    SALEDM_ADO.Mssql.Base.conString = Configuration["ConnSALEDMBak"];
                    break;

                case "2":
                    SALEDM_ADO.Mssql.Base.conString = Configuration["ConnSALEDMLocal"];

                    break;
                default:
                    SALEDM_ADO.Mssql.Base.conString = Configuration["ConnSALEDM"];
                    break;
            }

            Core.Recaptha.Recaptha.secret = Configuration["RecaptchaSecretKey"];

            //StaticValue.GetInstant().LoadInstantAll();
            Constant.StaticValue.GetInstant().LoadInstantAll();
            //AllowCors
            app.UseCors("AllowAllHeaders");
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
