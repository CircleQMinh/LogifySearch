using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVTQ.LogifySearch.Domain.Setting;

namespace MVTQ.LogifySearch.Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomainLayer(this IServiceCollection services, IConfiguration configuration)
        {

            return services;
        }
    }
}
