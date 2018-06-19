using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DynamicMapping.DBCommunications
{
    public interface IRepository<Type> where Type : class
    {
        //Execute Inline Query
        Task<SqlDataReader> GetExecuteReader(string strText);
        Task<int> GetExecuteNonQuery(string strText);
        Task<dynamic> GetByGetExecuteReader(string strText);




        //Execute Stored Procedure
        Task<string> ExecuteNonQueryProc(SqlCommand cmd);
        Task<IEnumerable<Type>> GetDataReaderProc(SqlCommand cmd);
        Task<Type> GetByDataReaderProc(SqlCommand cmd);

    }
}
