using Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using AutoWrapper;

namespace WebAPI
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddHttpContextAccessor();

            //swagger service
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "random.IT API",
                    Description = "A system focusing on sharing random or pseudo-random data via API.",
                    Contact = new OpenApiContact
                    {
                        Name = "Mateusz Donhefner",
                        Email = string.Empty,
                        Url = new Uri("https://github.com/matdon90"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under MIT license",
                        Url = new Uri("https://github.com/matdon90/random.IT")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            //

            services.AddRouting(opt => opt.LowercaseUrls = true);
            services.AddControllers();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "random.IT API");
                c.RoutePrefix = string.Empty;
            });


            //autowrapper
            app.UseApiResponseAndExceptionWrapper(
                new AutoWrapperOptions 
                { 
                    UseCustomSchema = true,  
                    ShowStatusCode = true
                });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
