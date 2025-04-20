using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using MVTQ.LogifySearch.Domain.Common;
using MVTQ.LogifySearch.Domain.Enums;
using MVTQ.LogifySearch.Domain.Extensions;
using System.Reflection;

namespace MVTQ.LogifySearch.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(DependencyInjection).Assembly;
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }

    }
}
