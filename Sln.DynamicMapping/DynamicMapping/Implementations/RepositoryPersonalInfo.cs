using DynamicMapping.Configarations;
using DynamicMapping.DBCommunications;
using DynamicMapping.Logs;
using DynamicMapping.Molel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DynamicMapping.Implementations
{
    public class RepositoryPersonalInfo : Repository<PersonalInfo>, IRepositoryPersonalInfo<PersonalInfo>
    {
        protected ILogger Logger { get; set; }
        public RepositoryPersonalInfo()
        {
            Logger = logger;
        }
        public RepositoryPersonalInfo(ILogger logger)
        {
            Logger = logger;
        }


        public async Task<IEnumerable<PersonalInfo>> GetAll()
        {
            try
            {
                var cmd = new SqlCommand("sp_PersonalInfo");
                cmd.Parameters.AddWithValue("@pOptions", 4);
                var result = await GetDataReaderProc(cmd);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw ex;
            }
        }

        public async Task<IEnumerable<PersonalInfo>> GetAllDynamic()
        {
            try
            {
                var cmd = new SqlCommand("sp_PersonalInfo");
                cmd.Parameters.AddWithValue("@pOptions", 4);
                var result = await GetDataReaderProcDynamic(cmd);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw ex;
            }
        }

        public async Task<IEnumerable<PersonalInfo>> GetAllDynamic2()
        {
            try
            {
                var cmd = new SqlCommand("sp_PersonalInfo");
                cmd.Parameters.AddWithValue("@pOptions", 4);
                var result = await GetDataReaderProcDynamic2(cmd);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw ex;
            }
        }


        public async Task<PersonalInfo> GetByID(long PersonalInfoID)
        {
            try
            {
                var cmd = new SqlCommand("sp_PersonalInfo");
                cmd.Parameters.AddWithValue("@PersonalInfoID", PersonalInfoID);
                cmd.Parameters.AddWithValue("@pOptions", 5);
                var result = await GetByDataReaderProc(cmd);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw ex;
            }
        }

        public async Task<string> Insert(PersonalInfo entity)
        {
            try
            {
                var cmd = new SqlCommand("sp_PersonalInfo");
                cmd.Parameters.AddWithValue("@PersonalInfoID", entity.PersonalInfoID);
                cmd.Parameters.AddWithValue("@FirstName", entity.FirstName);
                cmd.Parameters.AddWithValue("@LastName", entity.LastName);
                cmd.Parameters.AddWithValue("@DateOfBirth", entity.DateOfBirth);
                cmd.Parameters.AddWithValue("@City", entity.City);
                cmd.Parameters.AddWithValue("@Country", entity.Country);
                cmd.Parameters.AddWithValue("@MobileNo", entity.MobileNo);
                cmd.Parameters.AddWithValue("@NID", entity.NID);
                cmd.Parameters.AddWithValue("@Email", entity.Email);
                cmd.Parameters.AddWithValue("@Status", entity.Status);

                cmd.Parameters.Add("@Msg", SqlDbType.NChar, 500);
                cmd.Parameters["@Msg"].Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("@pOptions", 1);

                var result = await ExecuteNonQueryProc(cmd);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw ex;
            }
        }

        public async Task<string> Update(PersonalInfo entity)
        {
            try
            {
                var cmd = new SqlCommand("sp_PersonalInfo");
                cmd.Parameters.AddWithValue("@PersonalInfoID", entity.PersonalInfoID);
                cmd.Parameters.AddWithValue("@FirstName", entity.FirstName);
                cmd.Parameters.AddWithValue("@LastName", entity.LastName);
                cmd.Parameters.AddWithValue("@DateOfBirth", entity.DateOfBirth);
                cmd.Parameters.AddWithValue("@City", entity.City);
                cmd.Parameters.AddWithValue("@Country", entity.Country);
                cmd.Parameters.AddWithValue("@MobileNo", entity.MobileNo);
                cmd.Parameters.AddWithValue("@NID", entity.NID);
                cmd.Parameters.AddWithValue("@Email", entity.Email);
                cmd.Parameters.AddWithValue("@Status", entity.Status);

                cmd.Parameters.Add("@Msg", SqlDbType.NChar, 500);
                cmd.Parameters["@Msg"].Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("@pOptions", 2);

                var result = await ExecuteNonQueryProc(cmd);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw ex;
            }
        }


        public async Task<string> Delete(long PersonalInfoID)
        {
            try
            {
                var cmd = new SqlCommand("sp_PersonalInfo");
                cmd.Parameters.AddWithValue("@PersonalInfoID", PersonalInfoID);
                cmd.Parameters.Add("@Msg", SqlDbType.NChar, 500);


                cmd.Parameters["@Msg"].Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("@pOptions", 3);


                var result = await ExecuteNonQueryProc(cmd);
                if (Convert.ToString(result).Trim().Contains("Data Deleted Successfully"))
                {
                    var message = PersonalInfoID.ToString() + " has been Deleted.";
                }
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw ex;
            }
        }      
    }
}
