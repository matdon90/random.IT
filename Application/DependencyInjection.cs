using Application.Common.Interfaces;
using Application.Common.Services;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IGuidGenerator, GuidGeneratorService>();
            return services;
        }
    }
}
