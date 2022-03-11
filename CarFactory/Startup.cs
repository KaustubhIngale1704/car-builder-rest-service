using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CarFactory_Assembly;
using CarFactory_Chasis;
using CarFactory_Engine;
using CarFactory_Factory;
using CarFactory_Interior;
using CarFactory_Interior.Builders;
using CarFactory_Interior.Interfaces;
using CarFactory_Paint;
using CarFactory_Storage;
using CarFactory_SubContractor;
using CarFactory_Wheels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace CarFactory
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
            services.AddControllersWithViews()
                .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Car markers API",
                    Version = "v1",
                    Description = ""
                });
            });

            services.AddSingleton<IMemoryCache, MemoryCache>();

            services.AddTransient<ICarAssembler, CarAssembler>();
            services.AddScoped<IChassisProvider, ChassisProvider>();
            services.AddScoped<IEngineProvider, EngineProvider>();
            services.AddScoped<IPainter, Painter>();
            services.AddScoped<IInteriorProvider, InteriorProvider>();
            services.AddScoped<IWheelProvider, WheelProvider>();
            services.AddScoped<ICarFactory, CarFactory_Factory.CarFactory>();
            services.AddTransient<IStorageProvider, StorageProvider>();
            services.AddScoped<IEngineProvider, EngineProvider>();
            services.AddScoped<ISteelSubcontractor, SteelSubcontractor>();
            services.AddScoped<IGetRubberQuery,GetRubberQuery>();
            services.AddScoped<IDashboardBuilder, DashboardBuilder>();
            services.AddScoped<ISeatBuilder, SeatBuilder>();
            services.AddScoped<ISpeakerBuilder, SpeakerBuilder>();
            services.AddScoped<IGetPistons, GetPistons>();
            services.AddScoped<IGetChassisRecipeQuery, GetChassisRecipeQuery>();
            services.AddScoped<IGetEngineSpecificationQuery, GetEngineSpecificationQuery>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Zomato API V1");

                // To serve SwaggerUI at application's root page, set the RoutePrefix property to an empty string.
                c.RoutePrefix = string.Empty;
            });


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
