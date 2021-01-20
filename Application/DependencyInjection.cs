using Application.Common.Interfaces;
using Application.Common.Services;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddSingleton<IUriService>(o =>
            {
                var accessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(uri);
            });

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IGuidGenerator, GuidGeneratorService>();
            services.AddTransient<INetworkConfigGenerator, NetworkConfigGeneratorService>();
            return services;
        }
    }
}
