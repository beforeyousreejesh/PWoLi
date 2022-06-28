using PWoLi.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PWoLi.DataAccess.Contracts
{
    public interface IConfigurationObjectDataAccess
    {
        Task<IEnumerable<ConfigurationObject>> GetConfigurationObjectsAsync(string configurationName, Guid moduleId);

        Task InsertIntoConfigurationObjectAsync(
              Guid ObjectId,
              Guid ModuleId,
              string Name,
              Guid ObjectType,
              Guid? ParentObjectId,
              bool IsSecret,
              string CreatedBy,
              DateTime CreatedOn);

        Task UpdateConfigurationObjectAsync(
            Guid objectId,
            string name,
            Guid objectType,
            bool isSecret,
            string updatedBy,
            DateTime updatedOn);

        Task DeleteConfigurationObjectAsync(
            Guid objectId,
            string updatedBy,
            DateTime updatedOn);

        Task DeleteSystemsConfigurationObjectAsync(
            Guid systemId,
            string updatedBy,
            DateTime updatedOn);
    }
}