using PWoLi.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PWoLi.Application.Contracts
{
    public interface IEnvironmentValueService
    {
        Task<IEnumerable<ModuleEnvironmentValue>> GetConfigurationObjectValueAsync(Guid objectId, Guid topParentId);

        Task SaveEnvironmentValuesAsync(
            Guid objectId, 
            IEnumerable<ModuleEnvironmentValue> moduleEnvironmentValues, 
            string updatedBy);
    }
}
