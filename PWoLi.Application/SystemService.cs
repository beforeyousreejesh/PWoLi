using PWoLi.Application.Contracts;
using PWoLi.DataAccess.Contracts;
using PWoLi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWoLi.Application
{
    public class SystemService : ISystemService
    {
        private readonly ISystemDataAccess _systemDataAccess;

        private readonly IConfigurationObjectDataAccess _configurationObjectDataAccess;

        private readonly IGroupRoleDataAccess _groupRoleDataAccess;

        private readonly ISystemEnvironmentDataAccess _systemEnvironmentDataAccess;

        private const string ApplicationSystemName = "ApplicationSystem";

        public SystemService(
            ISystemDataAccess systemDataAccess,
            IConfigurationObjectDataAccess configurationObjectDataAccess,
            IGroupRoleDataAccess groupRoleDataAccess,
            ISystemEnvironmentDataAccess systemEnvironmentDataAccess)
        {
            if (systemDataAccess == null)
            {
                throw new ArgumentNullException(nameof(systemDataAccess));
            }

            if (configurationObjectDataAccess == null)
            {
                throw new ArgumentNullException(nameof(configurationObjectDataAccess));
            }

            if (groupRoleDataAccess == null)
            {
                throw new ArgumentNullException(nameof(groupRoleDataAccess));
            }

            if (systemEnvironmentDataAccess == null)
            {
                throw new ArgumentNullException(nameof(systemEnvironmentDataAccess));
            }

            _systemDataAccess = systemDataAccess;
            _configurationObjectDataAccess = configurationObjectDataAccess;
            _groupRoleDataAccess = groupRoleDataAccess;
            _systemEnvironmentDataAccess = systemEnvironmentDataAccess;
        }

        public Task<Systems> GetApplicationSystemAsync()
        {
            return _systemDataAccess.GetApplicationSystemAsync(ApplicationSystemName);
        }

        public async Task<IEnumerable<SystemModel>> GetSystemsAsync(string systemNamePattern)
        {
            var systems = (await _systemDataAccess.GetSystemsAsync(systemNamePattern)).Where(x => x.Name != ApplicationSystemName);

            var systemModels = systems.Where(x => x.ParentId == null).Select(x => new SystemModel
            {
                Id = x.Id,
                Name = x.Name,
                ParentId = x.ParentId,
                TopParentId = x.TopParentId,
                Childs = GetSystemModels(systems, x.Id)
            });

            return systemModels;
        }

        public async Task InsertIntoSystemsAsync(SystemModel systemModel, string createdBy)
        {
            var systemId = Guid.NewGuid();
            systemModel.Id = systemId;

            await _systemDataAccess.InsertIntoSystemsAsync(
                systemId,
                systemModel.Name,
                null,
                createdBy,
                DateTime.Now
                ).ConfigureAwait(false);

            if (systemModel.Childs != null && systemModel.Childs.Count > 0)
            {
                foreach (var systemModelChild in systemModel.Childs)
                {
                    await InsertSystemsChildAsync(systemId, systemModelChild, createdBy).ConfigureAwait(false);
                }
            }
        }

        public async Task UpdateSystemsAsync(SystemModel systemModel, string updatedBy)
        {
            await _systemDataAccess.UpdateSystemsAsync(
                systemModel.Id,
                systemModel.Name,
                updatedBy,
                DateTime.Now).ConfigureAwait(false);

            if (systemModel.Childs != null && systemModel.Childs.Count > 0)
            {
                foreach (var systemModelChild in systemModel.Childs)
                {
                    if (systemModelChild.Id != Guid.Empty && systemModelChild.IsDeleted)
                    {
                        await DeleteSystemsAsync(systemModelChild, updatedBy).ConfigureAwait(false);

                        continue;
                    }

                    if (systemModelChild.Id == Guid.Empty && !systemModelChild.IsDeleted)
                    {
                        await InsertSystemsChildAsync(
                            systemModel.Id,
                            systemModel,
                            updatedBy).ConfigureAwait(false);

                        continue;
                    }

                    if (systemModelChild.Id != Guid.Empty && !systemModelChild.IsDeleted)
                    {
                        await UpdateSystemModalChildsAsync(systemModel.Id, systemModelChild, updatedBy).ConfigureAwait(false);

                        continue;
                    }
                }
            }
        }

        public async Task DeleteSystemsAsync(SystemModel systemModel, string updatedBy)
        {
            // await _environmentValueDataAccess.DeleteEnvironmentValuesAsync(configurationObjectModel.Id, updatedBy, DateTime.Now).ConfigureAwait(false);

            await _systemDataAccess.DeleteSystemsAsync(
                       systemModel.Id,
                       updatedBy,
                       DateTime.Now).ConfigureAwait(false);

            await _configurationObjectDataAccess.DeleteSystemsConfigurationObjectAsync(
                            systemModel.Id,
                            updatedBy,
                            DateTime.Now).ConfigureAwait(false);

            await _groupRoleDataAccess.DeleteSystemsGroupRolesAsync(
                            systemModel.Id,
                            updatedBy,
                            DateTime.Now).ConfigureAwait(false);

            await _systemEnvironmentDataAccess.DeleteSystemEnvironmentRolesAsync(
                            systemModel.Id,
                            updatedBy,
                            DateTime.Now).ConfigureAwait(false);

            if (systemModel.Childs != null && systemModel.Childs.Count > 0)
            {
                foreach (var systemModelChild in systemModel.Childs)
                {
                    await DeleteSystemsAsync(systemModelChild, updatedBy).ConfigureAwait(false);
                }
            }
        }


        private IList<SystemModel> GetSystemModels(IEnumerable<Systems> systems, Guid parentId)
        {
            return systems.Where(x => x.ParentId == parentId).Select(x => new SystemModel
            {
                Id = x.Id,
                Name = x.Name,
                ParentId = x.ParentId,
                TopParentId = x.TopParentId,
                Childs = GetSystemModels(systems, x.Id)
            }).ToList();
        }

        private async Task InsertSystemsChildAsync(
            Guid parentId,
            SystemModel systemModel,
            string createdBy)
        {
            var systemId = Guid.NewGuid();
            systemModel.Id = systemId;

            await _systemDataAccess.InsertIntoSystemsAsync(
                systemId,
                systemModel.Name,
                parentId,
                createdBy,
                DateTime.Now
                ).ConfigureAwait(false);

            if (systemModel.Childs != null && systemModel.Childs.Count > 0)
            {
                foreach (var systemModelChild in systemModel.Childs)
                {
                    await InsertSystemsChildAsync(systemId, systemModelChild, createdBy).ConfigureAwait(false);
                }
            }
        }
        private async Task UpdateSystemModalChildsAsync(
           Guid parentId,
           SystemModel systemModel,
           string updatedBy)
        {

            await _systemDataAccess.UpdateSystemsAsync(
                systemModel.Id,
                systemModel.Name,
                updatedBy,
                DateTime.Now).ConfigureAwait(false);

            if (systemModel.Childs != null && systemModel.Childs.Count > 0)
            {
                foreach (var systemModelChild in systemModel.Childs)
                {
                    if (systemModelChild.Id != Guid.Empty && systemModelChild.IsDeleted)
                    {
                        await DeleteSystemsAsync(systemModelChild, updatedBy).ConfigureAwait(false);

                        continue;
                    }

                    if (systemModelChild.Id == Guid.Empty && !systemModelChild.IsDeleted)
                    {
                        await InsertSystemsChildAsync(
                            parentId,
                            systemModel,
                            updatedBy).ConfigureAwait(false);

                        continue;
                    }

                    if (systemModelChild.Id != Guid.Empty && !systemModelChild.IsDeleted)
                    {
                        await UpdateSystemModalChildsAsync(parentId, systemModelChild, updatedBy).ConfigureAwait(false);

                        continue;
                    }
                }
            }
        }
    }
}