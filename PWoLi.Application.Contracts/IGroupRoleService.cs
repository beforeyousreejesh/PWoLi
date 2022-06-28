using PWoLi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PWoLi.Application.Contracts
{
    public interface IGroupRoleService
    {
        Task<IEnumerable<SystemGroupRole>> GetSystemGroupRolesAsync();
    }
}