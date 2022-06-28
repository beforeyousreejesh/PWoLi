using GI.MedUW.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PWoLi.DataAccess;
using PWoLi.DataAccess.Contracts;

namespace PWoLi.IoC
{
    internal class DataAccessServices
    {
        internal static void AddDataAccessServices(IServiceCollection services, IConfiguration configuration)
        {
            BindSqlServerDataAccess(services, configuration);

            services.AddTransient<IGroupRoleDataAccess, GroupRoleDataAccess>();
            services.AddTransient<ISystemDataAccess, SystemDataAccess>();

            services.AddTransient<IConfigurationObjectDataAccess, ConfigurationObjectDataAccess>();
            services.AddTransient<IObjectTypeDataAccess, ObjectTypeDataAccess>();
            services.AddTransient<IEnvironmentValueDataAccess, EnvironmentValueDataAccess>();
            services.AddTransient<ISystemEnvironmentDataAccess, SystemEnvironmentDataAccess>();
        }

        private static void BindSqlServerDataAccess(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PWoLi");
            var passphrase = configuration.GetSection("AppSettings").GetSection("PassPhrase").Value;

            services.AddSingleton<ISqlServerDataAccess>(new SqlServerDataAccess(new ConnectionStringManager(connectionString, passphrase).GetConnectionString()));
        }
    }
}