using PWoLi.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PWoLi.Application.Contracts
{
    public interface IConfigurationObjectService
    {
        Task<IEnumerable<ConfigurationObjectModel>> GetConfigurationObjectAsync(string name, Guid moduleId);

        Task InsertIntoConfigurationObjectAsync(
            Guid moduleId,
            ConfigurationObjectModel configurationObjectModel,
            string createdBy);

        Task UpdateConfigurationObjectAsync(
           Guid moduleId,
           ConfigurationObjectModel configurationObjectModel,
           string updatedBy);

        Task DeleteConfigurationObjectAsync(
           ConfigurationObjectModel configurationObjectModel,
           string updatedBy);

    }
}