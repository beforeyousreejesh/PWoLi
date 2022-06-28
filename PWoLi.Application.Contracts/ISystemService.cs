using PWoLi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PWoLi.Application.Contracts
{
    public interface ISystemService
    {
        Task<IEnumerable<SystemModel>> GetSystemsAsync(string systemNamePattern);

        Task<Systems> GetApplicationSystemAsync();

        Task InsertIntoSystemsAsync(SystemModel systemModel, string createdBy);

        Task UpdateSystemsAsync(SystemModel systemModel, string updatedBy);

        Task DeleteSystemsAsync(SystemModel systemModel, string updatedBy);
    }
}