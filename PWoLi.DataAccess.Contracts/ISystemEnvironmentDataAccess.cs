using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PWoLi.DataAccess.Contracts
{
    public interface ISystemEnvironmentDataAccess
    {
        Task DeleteSystemEnvironmentRolesAsync(
            Guid systemId,
            string updatedBy,
            DateTime updatedOn);
    }
}
