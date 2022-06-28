using GI.MedUW.SqlServer;
using PWoLi.DataAccess.Contracts;
using PWoLi.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PWoLi.DataAccess
{
    public class GroupRoleDataAccess : IGroupRoleDataAccess
    {
        private readonly ISqlServerDataAccess _sqlServerDataAccess;

        public GroupRoleDataAccess(ISqlServerDataAccess sqlServerDataAccess)
        {
            if (sqlServerDataAccess == null)
            {
                throw new ArgumentNullException(nameof(sqlServerDataAccess));
            }

            _sqlServerDataAccess = sqlServerDataAccess;
        }

        public Task DeleteSystemsGroupRolesAsync(Guid systemId, string updatedBy, DateTime updatedOn)
        {
            var sqlParameters = new[]
            {
                new SqlParameter("@SystemId",systemId),
                new SqlParameter("@UpdatedBy",updatedBy),
                new SqlParameter("UpdatedOn",updatedOn)
            };

            return _sqlServerDataAccess.ExecuteAsync("DeleteGroupRolesBySystemId", sqlParameters);
        }

        public Task<IEnumerable<GroupRole>> GetGroupRolesAsync()
        {
            return _sqlServerDataAccess.GetAllAsync("GetSystemGroupRole", MapToGroupRole);
        }

        private GroupRole MapToGroupRole(IDataRecord dataRecord)
        {
            return new GroupRole
            {
                SystemId = dataRecord.GetValue<Guid>("SystemId"),
                Name = dataRecord.GetValue<string>("GroupName"),
                Role = dataRecord.GetValue<string>("RoleName")
            };
        }
    }
}