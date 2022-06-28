using PWoLi.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PWoLi.DataAccess.Contracts
{
    public interface ISystemDataAccess
    {
        Task<IEnumerable<Systems>> GetSystemsAsync(string systemName);

        Task<Systems> GetApplicationSystemAsync(string applicationSystemName);

        Task InsertIntoSystemsAsync(Guid systemId, string name, Guid? parentSystemId, string createdBy, DateTime createdOn);

        Task UpdateSystemsAsync(Guid systemId, string name, string updatedBy, DateTime updatedOn);

        Task DeleteSystemsAsync(Guid systemId, string updatedBy, DateTime updatedOn);
    }
}