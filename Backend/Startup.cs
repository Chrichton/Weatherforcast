using AutoMapper;
using Backend.OpenWeathermap;
using Backend.OpenWeathermap.Service;
using Backend.Weatherforecast;
using Backend.Weatherforecast.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using System.Net.Http;
using System.Runtime.CompilerServices;

namespace Backend
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
            services.AddCors(options => 
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyMethod();
                    });
            });

            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();

            services.AddScoped<HttpClient>();
            services.AddSingleton<ICityToIdProvider, CityToIdProvider>();
            services.AddScoped<IOpenWeathermapService, OpenWeathermapService>();
            services.AddSingleton<IZipCodeToCitiesProvider, ZipCodeToCitiesProvider>();
            services.AddScoped<IWeatherService, WeatherService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            logger.LogInformation("Configure called");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            logger.LogInformation("Configure finished");
        }
    }
}
