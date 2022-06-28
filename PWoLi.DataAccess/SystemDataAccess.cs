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
    public class SystemDataAccess : ISystemDataAccess
    {
        private readonly ISqlServerDataAccess _sqlServerDataAccess;

        public SystemDataAccess(ISqlServerDataAccess sqlServerDataAccess)
        {
            if (sqlServerDataAccess == null)
            {
                throw new ArgumentNullException(nameof(sqlServerDataAccess));
            }

            _sqlServerDataAccess = sqlServerDataAccess;
        }

        public Task<Systems> GetApplicationSystemAsync(string applicationSystemName)
        {
            var sqlParameter = new[]
            {
                new SqlParameter("@SystemName",applicationSystemName)
            };

            return _sqlServerDataAccess.FirstOrDefaultAsync("GetSystemByName", MapToSystem, sqlParameter);
        }

        public Task<IEnumerable<Systems>> GetSystemsAsync(string systemName)
        {
            var sqlParameter = new[]
            {
                new SqlParameter("@SystemName",systemName)
            };

            return _sqlServerDataAccess.GetAllAsync("GetSystemsBySystemName", MapToSystems, sqlParameter);
        }

        public Task InsertIntoSystemsAsync(Guid systemId, string name, Guid? parentSystemId, string createdBy, DateTime createdOn)
        {
            var sqlParameter = new[]
            {
                new SqlParameter("@SystemId",systemId),
                new SqlParameter("@Name",name),
                new SqlParameter("@ParentModule",parentSystemId),
                new SqlParameter("@CreatedBy",createdBy),
                new SqlParameter("@CreatedOn",createdOn),
            };

            return _sqlServerDataAccess.ExecuteAsync("InsertIntoSystems", sqlParameter);
        }

        public Task UpdateSystemsAsync(Guid systemId, string name, string updatedBy, DateTime updatedOn)
        {
            var sqlParameter = new[]
            {
                new SqlParameter("@SystemId",systemId),
                new SqlParameter("@Name",name),
                new SqlParameter("@UpdatedBy",updatedBy),
                new SqlParameter("@UpdatedOn",updatedOn),
            };

            return _sqlServerDataAccess.ExecuteAsync("UpdateSystems", sqlParameter);
        }

        public Task DeleteSystemsAsync(Guid systemId, string updatedBy, DateTime updatedOn)
        {
            var sqlParameter = new[]
            {
                new SqlParameter("@SystemId",systemId),
                new SqlParameter("@UpdatedBy",updatedBy),
                new SqlParameter("@UpdatedOn",updatedOn),
            };

            return _sqlServerDataAccess.ExecuteAsync("DeleteSystems", sqlParameter);
        }

        private Systems MapToSystems(IDataRecord dataRecord)
        {
            return new Systems
            {
                Id = dataRecord.GetValue<Guid>("SystemId"),
                Name = dataRecord.GetValue<string>("SystemName"),
                ParentId = dataRecord.GetValue<Guid?>("ParentSystemId"),
                TopParentId = dataRecord.GetValue<Guid?>("TopParentId"),
            };
        }

        private Systems MapToSystem(IDataRecord dataRecord)
        {
            return new Systems
            {
                Id = dataRecord.GetValue<Guid>("SystemId"),
                Name = dataRecord.GetValue<string>("SystemName"),
                ParentId = dataRecord.GetValue<Guid?>("ParentSystemId")
            };
        }
    }
}