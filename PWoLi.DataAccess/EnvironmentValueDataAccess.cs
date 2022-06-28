using GI.MedUW.SqlServer;
using PWoLi.DataAccess.Contracts;
using PWoLi.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace PWoLi.DataAccess
{
    public class EnvironmentValueDataAccess : IEnvironmentValueDataAccess
    {
        private readonly ISqlServerDataAccess _sqlServerDataAccess;

        public EnvironmentValueDataAccess(ISqlServerDataAccess sqlServerDataAccess)
        {
            if (sqlServerDataAccess == null)
            {
                throw new ArgumentNullException(nameof(sqlServerDataAccess));
            }

            _sqlServerDataAccess = sqlServerDataAccess;
        }

        public Task DeleteEnvironmentValuesAsync(Guid objectId, string updatedBy, DateTime updatedOn)
        {
            var sqlParameters = new[]
            {
                new SqlParameter("@ObjectId",objectId),
                new SqlParameter("@UpdatedBy",updatedBy),
                new SqlParameter("UpdatedOn",updatedOn)
            };

            return _sqlServerDataAccess.ExecuteAsync("DeleteEnviornmentValues", sqlParameters);
        }

        public Task<IEnumerable<ModuleEnvironmentValue>> GetModuleEnvironmentValuesAsync(Guid objectId, Guid topParentModuleId)
        {
            var sqlParameters = new[]
            {
                new SqlParameter("@ObjectId",objectId),
                new SqlParameter("@TopParentId",topParentModuleId),
            };

            return _sqlServerDataAccess.GetAllAsync("GetEnvironmentValuesByObjectIdAndTopParentId", MapToModuleEnvironmentValue, sqlParameters);
        }

        public Task InsertIntoEnvironmentValueAsync(Guid valueId, Guid moduleEnvironmentId, Guid objectId, string value, string createdBy, DateTime createdOn)
        {
            var sqlParameters = new[]
            {
                new SqlParameter("@ValueId",valueId),
                new SqlParameter("@ModuleEnvironmentId",moduleEnvironmentId),
                new SqlParameter("@ObjectId",objectId),
                new SqlParameter("@Value",value),
                new SqlParameter("@CreatedBy",createdBy),
                new SqlParameter("@CreatedOn",createdOn)
            };

            return _sqlServerDataAccess.ExecuteAsync("InsertIntoEnvironmentValue", sqlParameters);
        }

        public Task UpdateEnvironmentValueAsync(Guid valueId, string value, string updatedBy, DateTime updatedOn)
        {
            var sqlParameters = new[]
            {
                new SqlParameter("@ValueId",valueId),
                new SqlParameter("@Value",value),
                new SqlParameter("@UpdatedBy",updatedBy),
                new SqlParameter("@UpdatedOn",updatedOn)
            };

            return _sqlServerDataAccess.ExecuteAsync("UpdateEnvironmentValue", sqlParameters);
        }

        private ModuleEnvironmentValue MapToModuleEnvironmentValue(IDataRecord dataRecord)
        {
            return new ModuleEnvironmentValue
            {
                ModuleEnvironmentId = dataRecord.GetValue<Guid>("ModuleEnvironmentId"),
                ModuleId = dataRecord.GetValue<Guid>("ModuleEnvironmentId"),
                EnvironmentName = dataRecord.GetValue<string>("EnvironmentName"),
                ValueId = dataRecord.GetValue<Guid?>("ValueId"),
                Name = dataRecord.GetValue<string>("Name"),
                Value = dataRecord.GetValue<string>("Value"),
                IsSecret = dataRecord.GetValue<bool>("IsSecret"),
                EnvironmentSecretEnabled = dataRecord.GetValue<bool>("EnvironmentSecretEnabled"),
            };
        }
    }
}
