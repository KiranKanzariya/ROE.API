using Microsoft.Data.SqlClient;
using System.Data;

namespace ROE.DataAccess.Context
{
    public static class DBContext
    {

        #region Stored Procedure | Methods

        public static DataSet ExecuteProcedure(string connectionString, string procName, SqlParameter[] paramList)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 120;
                    command.CommandText = procName;

                    if (paramList != null && paramList.Count() > 0)
                    {
                        command.Parameters.AddRange(paramList);
                    }

                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                    DataSet dataSet = new DataSet();

                    sqlDataAdapter.SelectCommand = command;
                    sqlDataAdapter.Fill(dataSet);

                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    return dataSet;
                }
            }
        }

        public static DataSet ExecuteProcedureWithOutputParam(string connectionString, string procName, SqlParameter[] paramList, List<Tuple<string, SqlDbType, int>> outParamList, out Dictionary<string, string> outParamKeyValuePairs)
        {
            outParamKeyValuePairs = new Dictionary<string, string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 120;
                    command.CommandText = procName;

                    if (paramList != null && paramList.Count() > 0)
                    {
                        command.Parameters.AddRange(paramList);
                    }

                    if (outParamList != null && outParamList.Count > 0)
                    {
                        foreach (var outParam in outParamList)
                        {
                            command.Parameters.Add(outParam.Item1, outParam.Item2, outParam.Item3);
                            command.Parameters[outParam.Item1].Direction = ParameterDirection.Output;
                        }
                    }

                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                    DataSet dataSet = new DataSet();

                    sqlDataAdapter.SelectCommand = command;
                    sqlDataAdapter.Fill(dataSet);

                    if (outParamList != null && outParamList.Count > 0)
                    {
                        foreach (var outParam in outParamList)
                        {
                            outParamKeyValuePairs.Add(
                                outParam.Item1,
                                Convert.ToString(command.Parameters[outParam.Item1].Value) ?? ""
                            );
                        }
                    }

                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    return dataSet;
                }
            }
        }

        public static dynamic ExecuteUIDProcedure(string connectionString, string procName, SqlParameter[] paramList, Tuple<string, SqlDbType, int>? outParam = null)
        {
            dynamic? outParamValue = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 120;
                    command.CommandText = procName;

                    if (paramList != null && paramList.Count() > 0)
                    {
                        command.Parameters.AddRange(paramList);
                    }

                    if (outParam != null)
                    {
                        command.Parameters.Add(outParam.Item1, outParam.Item2, outParam.Item3);
                        command.Parameters[outParam.Item1].Direction = ParameterDirection.Output;
                    }

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    outParamValue = command.ExecuteNonQuery();

                    if (outParam != null)
                        outParamValue = command.Parameters[outParam.Item1].Value;


                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }

                    return outParamValue;
                }
            }
        }

        public static int ExecuteUIDProcedureWithOutputParam(string connectionString, string procName, SqlParameter[] paramList, List<Tuple<string, SqlDbType, int>> outParamList, out Dictionary<string, string> outParamKeyValuePairs)
        {
            int returnValue = 0;
            outParamKeyValuePairs = new Dictionary<string, string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 120;
                    command.CommandText = procName;

                    if (paramList != null && paramList.Count() > 0)
                    {
                        command.Parameters.AddRange(paramList);
                    }

                    if (outParamList != null && outParamList.Count > 0)
                    {
                        foreach (var outParam in outParamList)
                        {
                            command.Parameters.Add(outParam.Item1, outParam.Item2, outParam.Item3);
                            command.Parameters[outParam.Item1].Direction = ParameterDirection.Output;
                        }
                    }

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    returnValue = command.ExecuteNonQuery();

                    if (outParamList != null && outParamList.Count > 0)
                    {
                        foreach (var outParam in outParamList)
                        {
                            outParamKeyValuePairs.Add(
                                outParam.Item1,
                                Convert.ToString(command.Parameters[outParam.Item1].Value) ?? ""
                            );
                        }
                    }

                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }

                    return returnValue;
                }
            }
        }

        #endregion
    }
}
