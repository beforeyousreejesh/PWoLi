using Microsoft.AspNetCore.Http;
using PWoLi.Application.Contracts;
using PWoLi.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PWoLi.Web.Services
{
    public class ConfigurationService
    {
        private readonly IConfigurationObjectService _configurationObjectService;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IEnvironmentValueService _environmentValueService;

        public ConfigurationService(
            IConfigurationObjectService configurationObjectService,
            IHttpContextAccessor httpContextAccessor,
            IEnvironmentValueService environmentValueService)
        {
            if (configurationObjectService == null)
            {
                throw new ArgumentNullException(nameof(configurationObjectService));
            }

            if (httpContextAccessor == null)
            {
                throw new ArgumentNullException(nameof(httpContextAccessor));
            }

            if (environmentValueService == null)
            {
                throw new ArgumentNullException(nameof(environmentValueService));
            }

            _configurationObjectService = configurationObjectService;
            _httpContextAccessor = httpContextAccessor;
            _environmentValueService = environmentValueService;
        }

        public async Task InsertConfigurationObjectAsync(ConfigurationObjectModel configurationObjectModel, Guid moduleId)
        {
            var createdby = _httpContextAccessor.HttpContext.User?.Identity?.Name;

            if (string.IsNullOrEmpty(createdby))
            {
                throw new Exception("Invalid user");
            }

            await _configurationObjectService.InsertIntoConfigurationObjectAsync(moduleId, configurationObjectModel, createdby);
        }

        public async Task UpdateConfigurationObjectAsync(ConfigurationObjectModel configurationObjectModel, Guid moduleId)
        {
            var updatedBy = _httpContextAccessor.HttpContext.User?.Identity?.Name;

            if (string.IsNullOrEmpty(updatedBy))
            {
                throw new Exception("Invalid user");
            }

            await _configurationObjectService.UpdateConfigurationObjectAsync(moduleId, configurationObjectModel, updatedBy);
        }

        public async Task DeleteConfigurationObjectAsync(ConfigurationObjectModel configurationObjectModel)
        {
            var updatedBy = _httpContextAccessor.HttpContext.User?.Identity?.Name;

            if (string.IsNullOrEmpty(updatedBy))
            {
                throw new Exception("Invalid user");
            }

            await _configurationObjectService.DeleteConfigurationObjectAsync(configurationObjectModel, updatedBy);
        }

        public async Task SaveEnvironmentValuesAsync(Guid objectId,IEnumerable<ModuleEnvironmentValue> moduleEnvironmentValues)
        {
            var updatedBy = _httpContextAccessor.HttpContext.User?.Identity?.Name;

            if (string.IsNullOrEmpty(updatedBy))
            {
                throw new Exception("Invalid user");
            }

            await _environmentValueService.SaveEnvironmentValuesAsync(objectId, moduleEnvironmentValues, updatedBy);
        }
    }
}