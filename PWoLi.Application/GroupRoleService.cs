using PWoLi.Application.Contracts;
using PWoLi.DataAccess.Contracts;
using PWoLi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWoLi.Application
{
    public class GroupRoleService : IGroupRoleService
    {
        private readonly IGroupRoleDataAccess _groupRoleDataAccess;

        public GroupRoleService(IGroupRoleDataAccess groupRoleDataAccess)
        {
            if (groupRoleDataAccess == null)
            {
                throw new ArgumentNullException(nameof(groupRoleDataAccess));
            }

            _groupRoleDataAccess = groupRoleDataAccess;
        }

        public async Task<IEnumerable<SystemGroupRole>> GetSystemGroupRolesAsync()
        {
            var groupRoles = await _groupRoleDataAccess.GetGroupRolesAsync();

            var systemModelGroups = groupRoles.GroupBy(x => x.SystemId).Select(x => new SystemGroupRole
            {
                SystemId = x.Key,
                GroupRoles = x
            });

            return systemModelGroups;
        }
    }
}