using PWoLi.Application.Contracts;
using PWoLi.DataAccess.Contracts;
using PWoLi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWoLi.Application
{
    public class ConfigurationObjectService : IConfigurationObjectService
    {
        #region Private Variables
        private const string ObjectObjectType = "Object";

        private const string ValueObjectType = "Value";

        private readonly IConfigurationObjectDataAccess _configurationObjectDataAccess;

        private readonly IObjectTypeDataAccess _objectTypeDataAccess;

        private readonly IEnvironmentValueDataAccess _environmentValueDataAccess;
        #endregion

        #region Ctor
        public ConfigurationObjectService(
            IConfigurationObjectDataAccess configurationObjectDataAccess,
            IObjectTypeDataAccess objectTypeDataAccess,
            IEnvironmentValueDataAccess environmentValueDataAccess)
        {
            if (configurationObjectDataAccess == null)
            {
                throw new ArgumentNullException(nameof(configurationObjectDataAccess));
            }

            if (objectTypeDataAccess == null)
            {
                throw new ArgumentNullException(nameof(objectTypeDataAccess));
            }

            if (environmentValueDataAccess == null)
            {
                throw new ArgumentNullException(nameof(environmentValueDataAccess));
            }

            _configurationObjectDataAccess = configurationObjectDataAccess;
            _objectTypeDataAccess = objectTypeDataAccess;
            _environmentValueDataAccess = environmentValueDataAccess;
        }
        #endregion

        #region Public Methods
        public async Task<IEnumerable<ConfigurationObjectModel>> GetConfigurationObjectAsync(string name, Guid moduleId)
        {
            var configurationObjects = await _configurationObjectDataAccess.GetConfigurationObjectsAsync(name, moduleId);

            return configurationObjects
                    .Where(x => x.ParentObjectId == null)
                    .Select(x => new ConfigurationObjectModel
                    {
                        Id = x.ObjectId,
                        ParentObjectId = null,
                        Name = x.Name,
                        IsSecret = x.IsSecret,
                        FullName = x.Name,
                        Childs = GetConfigurationObjectModels(configurationObjects, x.ObjectId, x.Name).ToList()
                    });
        }

        public async Task InsertIntoConfigurationObjectAsync(Guid moduleId, ConfigurationObjectModel configurationObjectModel, string createdBy)
        {
            var configuraionObjectId = Guid.NewGuid();
            var isSecret = ((configurationObjectModel.Childs == null || configurationObjectModel.Childs.Count <= 0) && configurationObjectModel.IsSecret);

            var objectTypes = await _objectTypeDataAccess.GetObjectTypesAsync().ConfigureAwait(false);

            Guid? objectTypeId = null;

            if (configurationObjectModel.Childs?.Count > 0)
            {
                objectTypeId = objectTypes.FirstOrDefault(x => x.Name.Equals(ObjectObjectType, StringComparison.OrdinalIgnoreCase))?.Id;
            }
            else
            {
                objectTypeId = objectTypes.FirstOrDefault(x => x.Name.Equals(ValueObjectType, StringComparison.OrdinalIgnoreCase))?.Id;
            }

            await _configurationObjectDataAccess.InsertIntoConfigurationObjectAsync(
                configuraionObjectId,
                moduleId,
                configurationObjectModel.Name,
                objectTypeId.Value,
                null,
                isSecret,
                createdBy,
                DateTime.Now
                ).ConfigureAwait(false);

            if (configurationObjectModel.Childs != null && configurationObjectModel.Childs.Count > 0)
            {
                foreach (var configurationObjectModelChild in configurationObjectModel.Childs)
                {
                    await InsertConfigurationObjectChildsAsync(
                        moduleId,
                        configuraionObjectId,
                        configurationObjectModelChild,
                        createdBy,
                        objectTypes
                        ).ConfigureAwait(false);
                }
            }
        }

        public async Task UpdateConfigurationObjectAsync(
            Guid moduleId,
            ConfigurationObjectModel configurationObjectModel,
            string updatedBy)
        {
            var objectTypes = await _objectTypeDataAccess.GetObjectTypesAsync().ConfigureAwait(false);

            var isSecret = ((configurationObjectModel.Childs == null || configurationObjectModel.Childs.Count <= 0) && configurationObjectModel.IsSecret);

            Guid? objectTypeId = null;

            if (configurationObjectModel.Childs?.Count > 0)
            {
                objectTypeId = objectTypes.FirstOrDefault(x => x.Name.Equals(ObjectObjectType, StringComparison.OrdinalIgnoreCase))?.Id;
            }
            else
            {
                objectTypeId = objectTypes.FirstOrDefault(x => x.Name.Equals(ValueObjectType, StringComparison.OrdinalIgnoreCase))?.Id;
            }

            await _configurationObjectDataAccess.UpdateConfigurationObjectAsync(
                configurationObjectModel.Id,
                configurationObjectModel.Name,
                objectTypeId.Value,
                isSecret,
                updatedBy,
                DateTime.Now).ConfigureAwait(false);

            if (configurationObjectModel.Childs != null && configurationObjectModel.Childs.Count > 0)
            {
                foreach (var configurationObjectModelChild in configurationObjectModel.Childs)
                {
                    if (configurationObjectModelChild.Id != Guid.Empty && configurationObjectModelChild.IsDeleted)
                    {
                        await DeleteConfigurationObjectAsync(configurationObjectModelChild, updatedBy).ConfigureAwait(false);

                        continue;
                    }

                    if (configurationObjectModelChild.Id == Guid.Empty && !configurationObjectModelChild.IsDeleted)
                    {
                        await InsertConfigurationObjectChildsAsync(
                            moduleId,
                            configurationObjectModel.Id,
                            configurationObjectModelChild,
                            updatedBy,
                            objectTypes).ConfigureAwait(false);

                        continue;
                    }

                    if (configurationObjectModelChild.Id != Guid.Empty && !configurationObjectModelChild.IsDeleted)
                    {
                        await UpdateConfigurationObjectChildsAsync(moduleId, configurationObjectModelChild, updatedBy, objectTypes).ConfigureAwait(false);

                        continue;
                    }
                }
            }
        }

        public async Task DeleteConfigurationObjectAsync(
            ConfigurationObjectModel configurationObjectModel,
            string updatedBy)
        {

            await _environmentValueDataAccess.DeleteEnvironmentValuesAsync(configurationObjectModel.Id, updatedBy, DateTime.Now).ConfigureAwait(false);

            await _configurationObjectDataAccess.DeleteConfigurationObjectAsync(
                       configurationObjectModel.Id,
                       updatedBy,
                       DateTime.Now).ConfigureAwait(false);


            if (configurationObjectModel.Childs != null && configurationObjectModel.Childs.Count > 0)
            {
                foreach (var configurationObjectModelChild in configurationObjectModel.Childs)
                {
                    await DeleteConfigurationObjectAsync(configurationObjectModelChild, updatedBy).ConfigureAwait(false);
                }
            }
        }
        #endregion

        #region Private Methods
        private IEnumerable<ConfigurationObjectModel> GetConfigurationObjectModels(IEnumerable<ConfigurationObject> configurationObjects, Guid parentObtectId, string heirachyName)
        {
            return configurationObjects
                .Where(x => x.ParentObjectId == parentObtectId)
                .Select(x => new ConfigurationObjectModel
                {
                    Id = x.ObjectId,
                    ParentObjectId = parentObtectId,
                    Name = x.Name,
                    IsSecret = x.IsSecret,
                    FullName = $"{heirachyName}.{x.Name}",
                    Childs = GetConfigurationObjectModels(configurationObjects, x.ObjectId, $"{heirachyName}.{x.Name}").ToList()
                });
        }

        private async Task InsertConfigurationObjectChildsAsync(
            Guid moduleId,
            Guid parentObjectId,
            ConfigurationObjectModel configurationObjectModel,
            string createdBy,
            IEnumerable<ObjectType> objectTypes)
        {
            var configuraionObjectId = Guid.NewGuid();
            configurationObjectModel.Id = configuraionObjectId;

            var isSecret = ((configurationObjectModel.Childs == null || configurationObjectModel.Childs.Count <= 0) && configurationObjectModel.IsSecret);

            Guid? objectTypeId = null;

            if (configurationObjectModel.Childs?.Count > 0)
            {
                objectTypeId = objectTypes.FirstOrDefault(x => x.Name.Equals(ObjectObjectType, StringComparison.OrdinalIgnoreCase))?.Id;
            }
            else
            {
                objectTypeId = objectTypes.FirstOrDefault(x => x.Name.Equals(ValueObjectType, StringComparison.OrdinalIgnoreCase))?.Id;
            }

            await _configurationObjectDataAccess.InsertIntoConfigurationObjectAsync(
                configuraionObjectId,
                moduleId,
                configurationObjectModel.Name,
                objectTypeId.Value,
                parentObjectId,
                isSecret,
                createdBy,
                DateTime.Now
                ).ConfigureAwait(false);

            if (configurationObjectModel.Childs != null && configurationObjectModel.Childs.Count > 0)
            {
                foreach (var configurationObjectModelChild in configurationObjectModel.Childs)
                {
                    await InsertConfigurationObjectChildsAsync(
                        moduleId,
                        configuraionObjectId,
                        configurationObjectModelChild,
                        createdBy,
                        objectTypes
                        ).ConfigureAwait(false);
                }
            }
        }

        private async Task UpdateConfigurationObjectChildsAsync(
            Guid moduleId,
            ConfigurationObjectModel configurationObjectModel,
            string updatedBy,
            IEnumerable<ObjectType> objectTypes)
        {
            var isSecret = ((configurationObjectModel.Childs == null || configurationObjectModel.Childs.Count <= 0) && configurationObjectModel.IsSecret);

            Guid? objectTypeId = null;

            if (configurationObjectModel.Childs?.Count > 0)
            {
                objectTypeId = objectTypes.FirstOrDefault(x => x.Name.Equals(ObjectObjectType, StringComparison.OrdinalIgnoreCase))?.Id;
            }
            else
            {
                objectTypeId = objectTypes.FirstOrDefault(x => x.Name.Equals(ValueObjectType, StringComparison.OrdinalIgnoreCase))?.Id;
            }

            await _configurationObjectDataAccess.UpdateConfigurationObjectAsync(
                configurationObjectModel.Id,
                configurationObjectModel.Name,
                objectTypeId.Value,
                isSecret,
                updatedBy,
                DateTime.Now).ConfigureAwait(false);

            if (configurationObjectModel.Childs != null && configurationObjectModel.Childs.Count > 0)
            {
                foreach (var configurationObjectModelChild in configurationObjectModel.Childs)
                {
                    if (configurationObjectModelChild.Id != Guid.Empty && configurationObjectModelChild.IsDeleted)
                    {
                        await DeleteConfigurationObjectAsync(configurationObjectModelChild, updatedBy).ConfigureAwait(false);

                        continue;
                    }

                    if (configurationObjectModelChild.Id == Guid.Empty && !configurationObjectModelChild.IsDeleted)
                    {
                        await InsertConfigurationObjectChildsAsync(
                            moduleId,
                            configurationObjectModel.Id,
                            configurationObjectModelChild,
                            updatedBy,
                            objectTypes).ConfigureAwait(false);

                        continue;
                    }

                    if (configurationObjectModelChild.Id != Guid.Empty && !configurationObjectModelChild.IsDeleted)
                    {
                        await UpdateConfigurationObjectChildsAsync(moduleId, configurationObjectModelChild, updatedBy, objectTypes).ConfigureAwait(false);

                        continue;
                    }
                }
            }
        }
        #endregion
    }
}