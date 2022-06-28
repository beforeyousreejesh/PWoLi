using PWoLi.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PWoLi.DataAccess.Contracts
{
    public interface IEnvironmentValueDataAccess
    {
        Task DeleteEnvironmentValuesAsync(Guid objectId, string updatedBy, DateTime updatedOn);

        Task<IEnumerable<ModuleEnvironmentValue>> GetModuleEnvironmentValuesAsync(Guid objectId, Guid topParentModuleId);

        Task InsertIntoEnvironmentValueAsync(
            Guid valueId, 
            Guid moduleEnvironmentId, 
            Guid objectId, 
            string value, 
            string createdBy, 
            DateTime createdOn);

        Task UpdateEnvironmentValueAsync(
            Guid valueId,
            string value,
            string updatedBy,
            DateTime updatedOn);
    }
}
