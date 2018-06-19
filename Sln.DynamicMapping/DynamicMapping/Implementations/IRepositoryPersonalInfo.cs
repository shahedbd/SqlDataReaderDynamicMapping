using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DynamicMapping.Implementations
{
    interface IRepositoryPersonalInfo<Type> where Type : class
    {
        Task<IEnumerable<Type>> GetAll();
        Task<Type> GetByID(long ID);
        Task<string> Insert(Type Entity);
        Task<string> Delete(long ID);
        Task<string> Update(Type Entity);
    }
}
