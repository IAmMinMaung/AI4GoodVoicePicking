using System;
using AI4GoodSite.Infrastructure.Repository;
using AI4GoodSite.Infrastructure.Repository.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace AI4GoodSite.Infrastructure.Extensions
{
    public static class StartupExtension
    {
        public static IServiceCollection InjectInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient<IResourceRepository, ResourceRepository>();

            return services;
        }
    }
}

