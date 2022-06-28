using Microsoft.AspNetCore.Http;
using PWoLi.Application.Contracts;
using PWoLi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWoLi.Web.Services
{
    public class RoleLevelService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IGroupRoleService _groupRoleService;

        public RoleLevelService(IHttpContextAccessor httpContextAccessor, IGroupRoleService groupRoleService)
        {
            if (_httpContextAccessor == httpContextAccessor)
            {
                throw new ArgumentNullException(nameof(httpContextAccessor));
            }

            if (_groupRoleService == groupRoleService)
            {
                throw new ArgumentNullException(nameof(groupRoleService));
            }

            _httpContextAccessor = httpContextAccessor;
            _groupRoleService = groupRoleService;
        }

        private IEnumerable<SystemGroupRole> GroupRoles { get; set; }

        public RoleLevel ApplicationSystemRole { get; set; } = RoleLevel.NoAccess();

        public async Task InitAsync()
        {
            GroupRoles = await _groupRoleService.GetSystemGroupRolesAsync();
        }

        public async Task<RoleLevel> GetRoleLevelAsync(Guid moduleId)
        {
            if (GroupRoles == null)
            {
                await InitAsync();
            }

            var currentUser = _httpContextAccessor.HttpContext.User;

            if (currentUser == null)
            {
                throw new Exception("Invalid user");
            }

            var systemAdminGroups = GroupRoles.FirstOrDefault(x => x.SystemId == moduleId)?.GroupRoles.Where(x => x.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase));

            if (systemAdminGroups != null)
            {
                foreach (var systemAdminGroup in systemAdminGroups)
                {
                    if (currentUser.IsInRole(systemAdminGroup.Name))
                    {
                        return RoleLevel.SystemAdmin();
                    }
                }
            }

            var viewSecretsGroups = GroupRoles.FirstOrDefault(x => x.SystemId == moduleId)?.GroupRoles.Where(x => x.Role.Equals("ViewSecret", StringComparison.OrdinalIgnoreCase));

            if (viewSecretsGroups != null)
            {
                foreach (var viewSecrets in viewSecretsGroups)
                {
                    if (currentUser.IsInRole(viewSecrets.Name))
                    {
                        return RoleLevel.ViewSecrets();
                    }
                }
            }

            var deleteGroups = GroupRoles.FirstOrDefault(x => x.SystemId == moduleId)?.GroupRoles.Where(x => x.Role.Equals("Delete", StringComparison.OrdinalIgnoreCase));

            if (deleteGroups != null)
            {
                foreach (var deleteGroup in deleteGroups)
                {
                    if (currentUser.IsInRole(deleteGroup.Name))
                    {
                        return RoleLevel.Delete();
                    }
                }
            }

            var writeGroups = GroupRoles.FirstOrDefault(x => x.SystemId == moduleId)?.GroupRoles.Where(x => x.Role.Equals("Write", StringComparison.OrdinalIgnoreCase));

            if (writeGroups != null)
            {
                foreach (var writeGroup in writeGroups)
                {
                    if (currentUser.IsInRole(writeGroup.Name))
                    {
                        return RoleLevel.Write();
                    }
                }
            }

            var readGroups = GroupRoles.FirstOrDefault(x => x.SystemId == moduleId)?.GroupRoles.Where(x => x.Role.Equals("Read", StringComparison.OrdinalIgnoreCase));

            if (readGroups != null)
            {
                foreach (var readGroup in readGroups)
                {
                    if (currentUser.IsInRole(readGroup.Name))
                    {
                        return RoleLevel.Read();
                    }
                }
            }

            return RoleLevel.NoAccess();
        }
    }
}