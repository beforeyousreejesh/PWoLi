using PWoLi.Application.Contracts;
using PWoLi.DataAccess.Contracts;
using PWoLi.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PWoLi.Application
{
    public class EnvironmentValueService : IEnvironmentValueService
    {
        private readonly IEnvironmentValueDataAccess _environmentValueDataAccess;

        public EnvironmentValueService(IEnvironmentValueDataAccess environmentValueDataAccess)
        {
            if (environmentValueDataAccess == null)
            {
                throw new ArgumentNullException(nameof(environmentValueDataAccess));
            }

            _environmentValueDataAccess = environmentValueDataAccess;
        }

        public async Task<IEnumerable<ModuleEnvironmentValue>> GetConfigurationObjectValueAsync(Guid objectId, Guid topParentId)
        {
            var environmentValues = await _environmentValueDataAccess.GetModuleEnvironmentValuesAsync(objectId, topParentId).ConfigureAwait(false);

            return environmentValues;
        }

        public async Task SaveEnvironmentValuesAsync(Guid objectId, IEnumerable<ModuleEnvironmentValue> moduleEnvironmentValues, string updatedBy)
        {
            foreach (var moduleEnvironmentValue in moduleEnvironmentValues)
            {
                if (moduleEnvironmentValue.ValueId == null)
                {
                    await _environmentValueDataAccess.InsertIntoEnvironmentValueAsync(
                        Guid.NewGuid(),
                        moduleEnvironmentValue.ModuleEnvironmentId,
                        objectId,
                        moduleEnvironmentValue.Value,
                        updatedBy,
                        DateTime.Now).ConfigureAwait(false);

                    continue;
                }

                await _environmentValueDataAccess.UpdateEnvironmentValueAsync(
                            moduleEnvironmentValue.ValueId.Value,
                            moduleEnvironmentValue.Value,
                            updatedBy,
                            DateTime.Now).ConfigureAwait(false);
            }
        }
    }
}
