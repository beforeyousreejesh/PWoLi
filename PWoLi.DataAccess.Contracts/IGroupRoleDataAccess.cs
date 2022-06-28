using PWoLi.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PWoLi.DataAccess.Contracts
{
    public interface IGroupRoleDataAccess
    {
        Task<IEnumerable<GroupRole>> GetGroupRolesAsync();

        Task DeleteSystemsGroupRolesAsync(
            Guid systemId,
            string updatedBy,
            DateTime updatedOn);
    }
}