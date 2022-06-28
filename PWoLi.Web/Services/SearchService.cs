using PWoLi.Application.Contracts;
using PWoLi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWoLi.Web.Services
{
    public class SearchService
    {
        private const string ApplicationSystemName = "ApplicationSystem";

        private readonly ISystemService _systemService;

        private readonly IConfigurationObjectService _configurationObjectService;

        private RoleLevelService _roleLevelService;

        public SearchService(
            ISystemService systemService,
            IConfigurationObjectService configurationObjectService)
        {
            if (systemService == null)
            {
                throw new ArgumentNullException(nameof(systemService));
            }

            if (configurationObjectService == null)
            {
                throw new ArgumentNullException(nameof(configurationObjectService));
            }

            _systemService = systemService;
            _configurationObjectService = configurationObjectService;
        }

        public SearchService SetService(RoleLevelService roleLevelService)
        {
            _roleLevelService = roleLevelService;

            return this;
        }

        public async Task<IEnumerable<SystemModel>> GetSystemModelsAsync(string systemNamePattern)
        {
            var dSystemModels = new List<SystemModel>();

            var systemModels = (await _systemService.GetSystemsAsync(systemNamePattern)).Where(x => !x.Name.Equals(ApplicationSystemName, StringComparison.OrdinalIgnoreCase));

            foreach (var systemModel in systemModels)
            {
                var topParentId = systemModel.TopParentId ?? systemModel.Id;

                var roleLevel = await _roleLevelService?.GetRoleLevelAsync(topParentId);

                if (roleLevel.CanRead())
                {
                    systemModel.RoleLevel = roleLevel;
                    systemModel.TopParentId = topParentId;

                    if (systemModel.Childs != null && systemModel.Childs.Count() > 0)
                    {
                        var nSystemChilds = new List<SystemModel>();

                        foreach (var systemModelChild in systemModel.Childs)
                        {
                            var nSystemModelChild = SetRoleLevel(systemModelChild, roleLevel, topParentId);
                            nSystemChilds.Add(nSystemModelChild);
                        }

                        systemModel.Childs = nSystemChilds;
                    }

                    dSystemModels.Add(systemModel);
                }
            }

            return dSystemModels;
        }

        public Task<IEnumerable<ConfigurationObjectModel>> GetConfigurationObjectModelsAsync(string configurationNamePattern, Guid moduleId)
        {
            return _configurationObjectService.GetConfigurationObjectAsync(configurationNamePattern, moduleId);
        }

        private SystemModel SetRoleLevel(SystemModel systemModel, RoleLevel roleLevel, Guid topParentId)
        {
            var nSystemModel = new SystemModel();

            nSystemModel.Id = systemModel.Id;
            nSystemModel.Name = systemModel.Name;
            nSystemModel.RoleLevel = roleLevel;
            nSystemModel.TopParentId = topParentId;

            if (systemModel.Childs != null && systemModel.Childs.Count() > 0)
            {
                var nSystemChilds = new List<SystemModel>();

                foreach (var systemModelChild in systemModel.Childs)
                {
                    var nSystemModelChild = SetRoleLevel(systemModelChild, roleLevel, topParentId);
                    nSystemChilds.Add(nSystemModelChild);
                }

                nSystemModel.Childs = nSystemChilds;
            }

            return nSystemModel;
        }
    }
}