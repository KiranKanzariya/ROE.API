using System.Data;
using System.Reflection;

namespace ROE.DataAccess.Utility
{
    public static class DataTableExtensions
    {
        /// <summary>
        /// Convert s DataTable to a list with generic objects.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    T obj = new T();
                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo? propertyInfo = obj.GetType().GetProperty(prop.Name);
                            if (propertyInfo != null)
                            {
                                if (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                                {
                                    if (table.Rows[i][prop.Name] == DBNull.Value)
                                    {
                                        propertyInfo.SetValue(obj, null, null);
                                    }
                                    else
                                    {
                                        var targetType = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                                        propertyInfo.SetValue(obj, Convert.ChangeType(table.Rows[i][prop.Name], targetType), null);
                                    }
                                }
                                else
                                {
                                    propertyInfo.SetValue(obj, Convert.ChangeType(table.Rows[i][prop.Name], propertyInfo.PropertyType), null);
                                } 
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    list.Add(obj);
                }
                return list;
            }
            catch
            {
                return new List<T>();
            }
        }

        /// <summary>
        /// Convert s DaaTable to a model.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static T DataTableToModel<T>(this DataTable table) where T : class, new()
        {
            try
            {
                T obj = new T();
                if (table.Rows.Count > 0)
                {
                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo? propertyInfo = obj.GetType().GetProperty(prop.Name);
                            if (propertyInfo != null)
                            {
                                if (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                                {
                                    if (table.Rows[0][prop.Name] == DBNull.Value)
                                    {
                                        propertyInfo.SetValue(obj, null, null);
                                    }
                                    else
                                    {
                                        var targetType = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                                        propertyInfo.SetValue(obj, Convert.ChangeType(table.Rows[0][prop.Name], targetType), null);
                                    }
                                }
                                else
                                {
                                    propertyInfo.SetValue(obj, Convert.ChangeType(table.Rows[0][prop.Name], propertyInfo.PropertyType), null);
                                } 
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
                return obj;
            }
            catch
            {
                return new T();
            }
        }
    }
}
