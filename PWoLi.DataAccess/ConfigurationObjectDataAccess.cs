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
    public class ConfigurationObjectDataAccess : IConfigurationObjectDataAccess
    {
        private readonly ISqlServerDataAccess _sqlServerDataAccess;

        public ConfigurationObjectDataAccess(ISqlServerDataAccess sqlServerDataAccess)
        {
            if (sqlServerDataAccess == null)
            {
                throw new ArgumentNullException(nameof(sqlServerDataAccess));
            }

            _sqlServerDataAccess = sqlServerDataAccess;
        }

        public Task DeleteConfigurationObjectAsync(
            Guid objectId, 
            string updatedBy,
            DateTime updatedOn)
        {
            var sqlParameters = new[]
            {
                new SqlParameter("@ObjectId",objectId),
                new SqlParameter("@UpdatedBy",updatedBy),
                new SqlParameter("UpdatedOn",updatedOn)
            };

            return _sqlServerDataAccess.ExecuteAsync("DeleteConfigurationObjects", sqlParameters);
        }

        public Task DeleteSystemsConfigurationObjectAsync(Guid systemId, string updatedBy, DateTime updatedOn)
        {
            var sqlParameters = new[]
             {
                new SqlParameter("@SystemId",systemId),
                new SqlParameter("@UpdatedBy",updatedBy),
                new SqlParameter("UpdatedOn",updatedOn)
            };

            return _sqlServerDataAccess.ExecuteAsync("DeleteConfigurationObjectBySystemId", sqlParameters);
        }

        public Task<IEnumerable<ConfigurationObject>> GetConfigurationObjectsAsync(string configurationName, Guid moduleId)
        {
            var sqlParameters = new[]
            {
                new SqlParameter("@ConfigurationName",configurationName),
                new SqlParameter("@ModuleId",moduleId)
            };

            return _sqlServerDataAccess.GetAllAsync("GetConfigurationObjectsByNameAndModuleId", MapToConfigurationObject, sqlParameters);
        }

        public Task InsertIntoConfigurationObjectAsync
            (Guid objectId,
            Guid moduleId,
            string name,
            Guid objectType,
            Guid? parentObjectId,
            bool isSecret,
            string createdBy,
            DateTime createdOn)
        {
            var sqlParameters = new[]
            {
                new SqlParameter("@ObjectId",objectId),
                new SqlParameter("@ModuleId",moduleId),
                new SqlParameter("@Name",name),
                new SqlParameter("@ObjectType",objectType),
                new SqlParameter("@ParentObjectId",parentObjectId),
                new SqlParameter("@IsSecret",isSecret),
                new SqlParameter("@CreatedBy",createdBy),
                new SqlParameter("@CreatedOn",createdOn)
            };

            return _sqlServerDataAccess.ExecuteAsync("InsertIntoConfigurationObjects", sqlParameters);
        }

        public Task UpdateConfigurationObjectAsync(
            Guid objectId,
            string name,
            Guid objectType,
            bool isSecret,
            string updatedBy,
            DateTime updatedOn)
        {
            var sqlParameters = new[]
            {
                new SqlParameter("@ObjectId",objectId),
                new SqlParameter("@Name",name),
                new SqlParameter("@ObjectType",objectType),
                new SqlParameter("@IsSecret",isSecret),
                new SqlParameter("@UpdatedBy",updatedBy),
                new SqlParameter("UpdatedOn",updatedOn)
            };

            return _sqlServerDataAccess.ExecuteAsync("UpdateConfigurationObject", sqlParameters);
        }

        private ConfigurationObject MapToConfigurationObject(IDataRecord dataRecord)
        {
            return new ConfigurationObject
            {
                ObjectId = dataRecord.GetValue<Guid>("ObjectId"),
                Name = dataRecord.GetValue<string>("Name"),
                ParentObjectId = dataRecord.GetValue<Guid?>("ParentObjectId"),
                IsSecret=dataRecord.GetValue<bool>("IsSecret"),
                ObjectType = new ObjectType
                {
                    Id = dataRecord.GetValue<Guid>("ObjectTypeId"),
                    Name = dataRecord.GetValue<string>("ObjectType")
                }
            };
        }
    }
}