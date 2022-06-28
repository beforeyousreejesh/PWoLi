using GI.MedUW.SqlServer;
using PWoLi.DataAccess.Contracts;
using PWoLi.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PWoLi.DataAccess
{
    public class ObjectTypeDataAccess : IObjectTypeDataAccess
    {
        private readonly ISqlServerDataAccess _sqlServerDataAccess;

        public ObjectTypeDataAccess(ISqlServerDataAccess sqlServerDataAccess)
        {
            if (sqlServerDataAccess == null)
            {
                throw new ArgumentNullException(nameof(sqlServerDataAccess));
            }

            _sqlServerDataAccess = sqlServerDataAccess;
        }

        public Task<IEnumerable<ObjectType>> GetObjectTypesAsync()
        {
            return _sqlServerDataAccess.GetAllAsync("GetObjectTypes", MapToObjecType);
        }

        private ObjectType MapToObjecType(IDataRecord dataRecord)
        {
            return new ObjectType
            {
                Id = dataRecord.GetValue<Guid>("ObjectTypeId"),
                Name = dataRecord.GetValue<string>("Name")
            };
        }
    }
}