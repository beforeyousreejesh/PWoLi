using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PWoLi.IoC
{
    public static class ServiceCollectionExtension
    {
        public static void AddPWoLiServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            ApplicationServices.AddApplicationServices(serviceCollection, configuration);
            DataAccessServices.AddDataAccessServices(serviceCollection, configuration);
        }
    }
}