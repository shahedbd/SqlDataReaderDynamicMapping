using DynamicMapping.Configarations;
using DynamicMapping.Logs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicMapping.DBCommunications
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private static SqlConnection con = MSSQLConn.MSSQLConnection();
        public Logger logger { get; set; }
        public Repository()
        {
            logger = new Logger();
        }



        #region Execute Stored Procedure 

        /// <summary>
        /// ExecuteNonQueryProc
        /// </summary>
        /// <param name="cmd"> sql command</param>
        /// <returns>result</returns>
        public Task<string> ExecuteNonQueryProc(SqlCommand cmd)
        {
            try
            {
                if (con.State == ConnectionState.Broken || con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;

                Task.FromResult(cmd.ExecuteNonQuery());
                return Task.FromResult((string)cmd.Parameters["@Msg"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
                //return ex.Message;
            }
            finally
            {
                con.Close();
            }
        }

        /// <summary>
        /// Get By ExecuteStore Procedure
        /// </summary>
        /// <param name="cmd">sql command</param>
        /// <returns>result Tentity</returns>
        public async Task<IEnumerable<TEntity>> GetDataReaderProcDynamic(SqlCommand cmd)
        {
            var list = new List<TEntity>();
            try
            {
                if (con.State == ConnectionState.Broken || con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                var reader = cmd.ExecuteReader();

                try
                {
                    list = Helper.DataReaderDynamicMapToList<TEntity>(reader).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return await Task.FromResult(list);
        }

        public async Task<IEnumerable<TEntity>> GetDataReaderProcDynamic2(SqlCommand cmd)
        {
            var list = new List<TEntity>();
            try
            {
                if (con.State == ConnectionState.Broken || con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                var reader = cmd.ExecuteReader();

                try
                {

                    list = Helper.DataReaderDynamicMapToList2<TEntity>(reader).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return await Task.FromResult(list);
        }

        public async Task<IEnumerable<TEntity>> GetDataReaderProc(SqlCommand cmd)
        {
            var list = new List<TEntity>();
            try
            {
                if (con.State == ConnectionState.Broken || con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                var reader = cmd.ExecuteReader();

                try
                {
                    list = Helper.ManualMapping(reader).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return await Task.FromResult(list);
        }


        public async Task<TEntity> GetByDataReaderProc(SqlCommand cmd)
        {
            //var list = new List<TEntity>();
            TEntity record = null;
            try
            {
                if (con.State == ConnectionState.Broken || con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                var reader = cmd.ExecuteReader();
                try
                {
                    record = Helper.DataReaderDynamicMap<TEntity>(reader);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return await Task.FromResult(record);
        }
        #endregion Execute Stored Procedure


















        #region Execute Inline Query
        /// <summary>
        /// Get All
        /// </summary>
        /// <param name="strText"> sql Query</param>
        /// <returns> result </returns>
        public Task<SqlDataReader> GetExecuteReader(string strText)
        {
            SqlDataReader sqlreader;
            try
            {
                if (con.State == ConnectionState.Broken || con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand(strText);
                cmd.Connection = con;
                sqlreader = cmd.ExecuteReader();
                try
                {
                    while (sqlreader.Read())
                    {
                        return Task.FromResult(sqlreader);
                    }
                }
                finally
                {
                    sqlreader.Close();
                }
            }
            finally
            {
                con.Close();
            }
            return Task.FromResult(sqlreader);
        }

        /// <summary>
        /// ExecuteText (Only execute)
        /// </summary>
        /// <param name="strText">sql query</param>
        /// <returns>result</returns>
        public Task<int> GetExecuteNonQuery(string strText)
        {
            try
            {
                if (con.State == ConnectionState.Broken || con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand(strText);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                return Task.FromResult(cmd.ExecuteNonQuery());
            }
            catch (Exception ex)
            {
                throw ex;
                //return ex.Message;
            }
            finally
            {
                con.Close();
            }
        }
        /// <summary>
        /// GetReult
        /// </summary>
        /// <param name="strText">sql query</param>
        /// <returns>result</returns>
        public Task<dynamic> GetByGetExecuteReader(string strText)
        {
            dynamic record = null;
            try
            {
                if (con.State == ConnectionState.Broken || con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand(strText);
                cmd.Connection = con;
                var reader = cmd.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        record = (reader[0] == DBNull.Value) ? 0 : reader[0];
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            finally
            {
                con.Close();
            }
            return Task.FromResult(record);
        }
        #endregion  Execute Inline Query

    }
}
