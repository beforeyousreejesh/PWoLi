using PWoLi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PWoLi.DataAccess.Contracts
{
    public interface IObjectTypeDataAccess
    {
        Task<IEnumerable<ObjectType>> GetObjectTypesAsync();
    }
}