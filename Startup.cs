using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sklep_api
{
    public class Startup
    {
        private readonly string _appVersion;
        private readonly bool _httpsRedirect;
        private readonly string _ipAdress;
        private readonly bool _useLocalhost;
        private readonly string _activeDatabase;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _appVersion = configuration["Setup:AppVersion"];
            _ipAdress = configuration["Setup:IpAdress"];
            _activeDatabase = configuration["Setup:ActiveDatabase"];
            _httpsRedirect = bool.Parse(configuration["Setup:HttpsRedirect"]);
            _useLocalhost = bool.Parse(configuration["Setup:UseLocalhost"]);

            /*
             * Przyk³ad appsettings.json
             * ...
                 "Setup": {
                    "AppVersion": "v1",
                    "IpAdress": "localhost:5005",
                    "HttpsRedirect": false,
                    "UseLocalhost": true
                }
            ...
            */
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(_appVersion, new Microsoft.OpenApi.Models.OpenApiInfo { Title = "sklep-api", Version = _appVersion });
            });

            //todo configure CORS policy 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("v1/swagger.json", "Melodeon-api");
                });
            }

            if (_httpsRedirect) // enables https redirection
            {
                app.UseHttpsRedirection();
            }

            app.UseRouting();

            // Enables cors policy
            // app.UseCors("MyCORS");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
