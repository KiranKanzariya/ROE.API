using Microsoft.Data.SqlClient;

namespace ROE.DataAccess.Utility
{
    public class DBClient
    {
        public static SqlParameter[] GetSqlParameters(List<Tuple<string, object>>? paramList = null)
        {
            List<SqlParameter> paramSqlList = new List<SqlParameter>();
            if (paramList != null && paramList.Count > 0)
            {
                foreach (var item in paramList)
                {
                    paramSqlList.Add(new SqlParameter(item.Item1, item.Item2));
                }
            }
            return paramSqlList.ToArray();
        }
    }
}
