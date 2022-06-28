using GI.MedUW.SqlServer;
using PWoLi.DataAccess.Contracts;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace PWoLi.DataAccess
{
    public class SystemEnvironmentDataAccess : ISystemEnvironmentDataAccess
    {
        private readonly ISqlServerDataAccess _sqlServerDataAccess;

        public SystemEnvironmentDataAccess(ISqlServerDataAccess sqlServerDataAccess)
        {
            if (sqlServerDataAccess == null)
            {
                throw new ArgumentNullException(nameof(sqlServerDataAccess));
            }

            _sqlServerDataAccess = sqlServerDataAccess;
        }

        public Task DeleteSystemEnvironmentRolesAsync(Guid systemId, string updatedBy, DateTime updatedOn)
        {
            var sqlParameters = new[]
            {
                new SqlParameter("@SystemId",systemId),
                new SqlParameter("@UpdatedBy",updatedBy),
                new SqlParameter("UpdatedOn",updatedOn)
            };

            return _sqlServerDataAccess.ExecuteAsync("DeleteModuleEnvironmentsBySystemId", sqlParameters);
        }
    }
}
