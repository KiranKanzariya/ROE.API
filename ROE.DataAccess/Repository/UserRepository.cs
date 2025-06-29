using Microsoft.Data.SqlClient;
using ROE.DataAccess.Context;
using ROE.DataAccess.Entities;
using ROE.DataAccess.DTO;
using ROE.DataAccess.Utility;
using System.Data;
using DataTableExtensions = ROE.DataAccess.Utility.DataTableExtensions;

namespace ROE.DataAccess.Repository
{
    public class UserRepository
    {
        private readonly DatabaseSettings databaseSettings;
        public UserRepository(DatabaseSettings database)
        {
            databaseSettings = database;
        }

        public User AuthenticateUser(string userName, string password, out string returnCode)
        {
            returnCode = string.Empty;
            User model = new User();
            List<Tuple<string, object>> paramList = new List<Tuple<string, object>>();
            paramList.Add(new Tuple<string, object>("userName", userName));
            paramList.Add(new Tuple<string, object>("password", password));
            SqlParameter[] parameters = DBClient.GetSqlParameters(paramList);

            List<Tuple<string, SqlDbType, int>> outParamList = new List<Tuple<string, SqlDbType, int>>()
            {
                new Tuple<string, SqlDbType, int>("ReturnCode", SqlDbType.NVarChar, 20)
            };
            Dictionary<string, string> outParamKeyValuePairs = new Dictionary<string, string>();

            DataSet dataSet = DBContext.ExecuteProcedureWithOutputParam(databaseSettings.ConnectionString, "AuthenticateUser", parameters, outParamList, out outParamKeyValuePairs);
            if (outParamKeyValuePairs != null && outParamKeyValuePairs.Count > 0)
            {
                returnCode = Convert.ToString(outParamKeyValuePairs["ReturnCode"]);
                if (!string.IsNullOrEmpty(returnCode))
                {
                    if (returnCode == "C200")
                    {
                        if (dataSet != null && dataSet.Tables.Count > 0)
                        {
                            DataTable dt = dataSet.Tables[0].Copy();
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                model = DataTableExtensions.DataTableToModel<User>(dt);
                            }
                        }
                    }
                }
            }
            
            return model;
        }

        public List<User> FetchAllUsers(int customerId)
        {
            List<User> model = new List<User>();
            List<Tuple<string, object>> paramList = new List<Tuple<string, object>>();
            paramList.Add(new Tuple<string, object>("customerId", customerId));
            SqlParameter[] parameters = DBClient.GetSqlParameters(paramList);
            DataSet dataSet = DBContext.ExecuteProcedure(databaseSettings.ConnectionString, "FetchAllUsers", parameters);
            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                DataTable dt = dataSet.Tables[0].Copy();
                if (dt != null && dt.Rows.Count > 0)
                {
                    model = DataTableExtensions.DataTableToList<User>(dt);
                }
            }
            return model;
        }

        public UserDTOModel GetUserByUserName(string userName)
        {
            UserDTOModel model = new UserDTOModel();
            List<Tuple<string, object>> paramList = new List<Tuple<string, object>>();
            paramList.Add(new Tuple<string, object>("userName", userName));
            SqlParameter[] parameters = DBClient.GetSqlParameters(paramList);
            DataSet dataSet = DBContext.ExecuteProcedure(databaseSettings.ConnectionString, "GetUserByUserName", parameters);
            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                DataTable dt = dataSet.Tables[0].Copy();
                if (dt != null && dt.Rows.Count > 0)
                {
                    model = DataTableExtensions.DataTableToModel<UserDTOModel>(dt);
                }
            }
            return model;
        }
    }
}
