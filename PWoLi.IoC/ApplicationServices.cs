using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PWoLi.Application;
using PWoLi.Application.Contracts;

namespace PWoLi.IoC
{
    internal class ApplicationServices
    {
        internal static void AddApplicationServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ISystemService, SystemService>();
            services.AddTransient<IGroupRoleService, GroupRoleService>();
            services.AddTransient<IConfigurationObjectService, ConfigurationObjectService>();
            services.AddTransient<IConfigurationObjectService, ConfigurationObjectService>();
            services.AddTransient<IEnvironmentValueService, EnvironmentValueService>();
        }
    }
}