using DynamicMapping.Molel;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;

namespace DynamicMapping.Configarations
{
    public static class Helper
    {
        public static bool ColumnExists(SqlDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))    //reader.GetName(i) == columnName
                {
                    return true;
                }
            }
            return false;
        }


        public static dynamic ManualMapping(SqlDataReader sqldatareader)
        {
            try
            {
                PersonalInfo oPersonalInfo = new PersonalInfo();
                oPersonalInfo.PersonalInfoID = Helper.ColumnExists(sqldatareader, "PersonalInfoID") ? ((sqldatareader["PersonalInfoID"] == DBNull.Value) ? 0 : Convert.ToInt64(sqldatareader["PersonalInfoID"])) : 0;
                oPersonalInfo.FirstName = Helper.ColumnExists(sqldatareader, "FirstName") ? sqldatareader["FirstName"].ToString() : "";
                oPersonalInfo.LastName = Helper.ColumnExists(sqldatareader, "LastName") ? sqldatareader["LastName"].ToString() : "";
                oPersonalInfo.DateOfBirth = Helper.ColumnExists(sqldatareader, "DateOfBirth") ? ((sqldatareader["DateOfBirth"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(sqldatareader["DateOfBirth"])) : Convert.ToDateTime("01/01/1900");
                oPersonalInfo.City = Helper.ColumnExists(sqldatareader, "City") ? sqldatareader["City"].ToString() : "";
                oPersonalInfo.Country = Helper.ColumnExists(sqldatareader, "Country") ? sqldatareader["Country"].ToString() : "";
                oPersonalInfo.MobileNo = Helper.ColumnExists(sqldatareader, "MobileNo") ? sqldatareader["MobileNo"].ToString() : "";
                oPersonalInfo.NID = Helper.ColumnExists(sqldatareader, "NID") ? sqldatareader["NID"].ToString() : "";
                oPersonalInfo.Email = Helper.ColumnExists(sqldatareader, "Email") ? sqldatareader["Email"].ToString() : "";
                oPersonalInfo.Status = Helper.ColumnExists(sqldatareader, "Status") ? ((sqldatareader["Status"] == DBNull.Value) ? 0 : Convert.ToInt16(sqldatareader["Status"])) : 0;
                return oPersonalInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public static List<T> DataReaderDynamicMapToList<T>(DbDataReader dr)
        {
            List<T> list = new List<T>();
            while (dr.Read())
            {
                var obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!Equals(dr[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dr[prop.Name], null);
                    }
                }
                list.Add(obj);
            }
            return list;
        }

        public static List<T> DataReaderDynamicMapToList2<T>(SqlDataReader reader)
        {
            try
            {
                var results = new List<T>();
                var properties = typeof(T).GetProperties();

                while (reader.Read())
                {
                    var item = Activator.CreateInstance<T>();
                    foreach (var property in typeof(T).GetProperties())
                    {
                        if (!reader.IsDBNull(reader.GetOrdinal(property.Name)))
                        {
                            Type convertTo = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                            property.SetValue(item, Convert.ChangeType(reader[property.Name], convertTo), null);
                        }
                    }
                    results.Add(item);
                }
                return results;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static T DataReaderDynamicMap<T>(SqlDataReader reader)
        {
            var item = Activator.CreateInstance<T>();
            var properties = typeof(T).GetProperties();

            while (reader.Read())
            {
                foreach (var property in typeof(T).GetProperties())
                {
                    if (!reader.IsDBNull(reader.GetOrdinal(property.Name)))
                    {
                        Type convertTo = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                        property.SetValue(item, Convert.ChangeType(reader[property.Name], convertTo), null);
                    }
                }
            }
            return item;
        }


    }
}
