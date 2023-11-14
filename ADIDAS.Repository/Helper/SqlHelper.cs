//-------------------------------------------------------------------------------------
// <copyright file="SqlHelper.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-------------------------------------------------------------------------------------
namespace ADIDAS.Repository.Helper
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data;
    using System.Data.Common;
    using System.Globalization;
    using System.Reflection;

    /// <summary>
    /// SqlHelper.
    /// </summary>
    public static class SqlHelper
    {
        private static string connectionString = string.Empty;

        /// <summary>
        /// SetConnectionString.
        /// </summary>
        /// <param name="value">value.</param>
        public static void SetConnectionString(string value)
        {
            connectionString = value;
        }

        /// <summary>
        /// ExecutionType.
        /// </summary>
        public enum ExecutionType 
        {
            /// <summary>Query</summary>
            Query,

            /// <summary>Query</summary>
            Procedure,
        }

        /// <summary>
        /// ExecuteSelect.
        /// </summary>
        /// <typeparam name="T">T.</typeparam>
        /// <param name="query">query.</param>
        /// <param name="sqlParams">sqlParams.</param>
        /// <param name="executionType">executionType.</param>
        /// <returns>DataTable</returns>
        public static DataTable ExecuteSelect<T>(string query, List<DbParameter> sqlParams, ExecutionType executionType)
            where T : IDbConnection, new()
        {
            using (var sqlConnection = new T())
            {
                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.Connection.ConnectionString = connectionString;
                    sqlCommand.CommandText = query;
                    sqlCommand.CommandTimeout = 500;

                    if (executionType == ExecutionType.Procedure)
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                    }
                    else
                    {
                        sqlCommand.CommandType = CommandType.Text;
                    }

                    if (sqlParams != null)
                    {
                        foreach (DbParameter sqlParameter in sqlParams)
                        {
                            sqlCommand.Parameters.Add(sqlParameter);
                        }
                    }

                    sqlCommand.Connection.Open();
                    var dataTable = new DataTable();
                    dataTable.BeginLoadData();
                    dataTable.Load(sqlCommand.ExecuteReader());
                    dataTable.EndLoadData();
                    sqlCommand.Connection.Close();

                    return dataTable;
                }
            }
        }

        /// <summary>
        /// ExecuteDataSet.
        /// </summary>
        /// <typeparam name="T">T.</typeparam>
        /// <param name="query">query.</param>
        /// <param name="sqlParams">sqlParams.</param>
        /// <param name="executionType">executionType.</param>
        /// <returns>DataSet.</returns>
        public static DataSet ExecuteDataSet<T>(string query, List<DbParameter> sqlParams, ExecutionType executionType)
            where T : IDbConnection, new()
        {
            using (var sqlConnection = new T())
            {
                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.Connection.ConnectionString = connectionString;
                    sqlCommand.CommandText = query;
                    sqlCommand.CommandTimeout = 500;

                    if (executionType == ExecutionType.Procedure)
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                    }
                    else
                    {
                        sqlCommand.CommandType = CommandType.Text;
                    }

                    if (sqlParams != null)
                    {
                        foreach (DbParameter sqlParameter in sqlParams)
                        {
                            sqlCommand.Parameters.Add(sqlParameter);
                        }
                    }

                    sqlCommand.Connection.Open();
                    var dataSet = new DataSet();
                    var dataReader = sqlCommand.ExecuteReader();
                    dataSet.BeginInit();
                    while (!dataReader.IsClosed)
                    {
                        var dataTable = new DataTable();
                        dataTable.BeginLoadData();
                        dataTable.Load(dataReader);
                        dataTable.EndLoadData();
                        dataSet.Tables.Add(dataTable);
                        dataSet.AcceptChanges();
                    }

                    dataSet.EndInit();

                    sqlCommand.Connection.Close();

                    return dataSet;
                }

            }
        }

        /// <summary>
        /// ExecuteNonQuery.
        /// </summary>
        /// <typeparam name="T">T.</typeparam>
        /// <param name="query">query.</param>
        /// <param name="dbParams">dbParams.</param>
        /// <param name="executionType">executionType.</param>
        /// <param name="executionType">executionType.</param>
        /// <returns>timeOut</returns>
        public static Dictionary<string, dynamic> ExecuteNonQuery<T>(string query, List<DbParameter> dbParams, ExecutionType executionType, int timeOut = 0) 
            where T : IDbConnection, new()
        {
            int rowsaffected = 0;
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            using (var sqlConnection = new T())
            {
                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.Connection.ConnectionString = connectionString;
                    sqlCommand.CommandText = query;
                    if (timeOut > 0)
                    {
                        sqlCommand.CommandTimeout = timeOut;
                    }

                    if (executionType == ExecutionType.Procedure)
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                    }
                    else
                    {
                        sqlCommand.CommandType = CommandType.Text;
                    }

                    if (dbParams != null)
                    {
                        foreach (DbParameter dbParameter in dbParams)
                        {
                            sqlCommand.Parameters.Add(dbParameter);
                        }
                    }

                    sqlCommand.Connection.Open();
                    rowsaffected = sqlCommand.ExecuteNonQuery();

                    result.Add("RowsAffected", rowsaffected);
                    if (dbParams != null)
                    {
                        foreach (DbParameter param in dbParams)
                        {
                            if (param.Direction == ParameterDirection.Output)
                            {
                                result.Add(param.ParameterName, param.Value);
                            }
                        }
                    }

                    sqlCommand.Parameters.Clear();
                    sqlCommand.Connection.Close();
                    return result;
                }
            }
        }

        /// <summary>
        /// Convert DataTable ToList.
        /// </summary>
        /// <typeparam name="T">T.</typeparam>
        /// <param name="dt">dt.</param>
        /// <returns>List Values.</returns>
        public static List<T> ConvertDataTableToList<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            Type temp = typeof(T);
            PropertyInfo[] props = temp.GetProperties();
            string[] propsName = new string[props.Length];
            int i = 0;

            foreach (PropertyInfo pro in props)
            {
                var pInfo = typeof(T).GetProperty(pro.Name)
                      .GetCustomAttribute<ColumnAttribute>();
                propsName[i] = (pInfo != null && !string.IsNullOrEmpty(pInfo.Name)) ? pInfo.Name : pro.Name;
                i++;
            }

            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row, props, propsName);
                data.Add(item);
            }

            return data;
        }
        /// <summary>
        /// Convert DataTable ToList.
        /// </summary>
        /// <typeparam name="T">T.</typeparam>
        /// <param name="dt">dt.</param>
        /// <returns>List Values.</returns>
        public static DataTable LinqToDataTable<T>(IEnumerable<T> items)
        {
            //Createa DataTable with the Name of the Class i.e. Customer class.
            DataTable dt = new DataTable(typeof(T).Name);

            //Read all the properties of the Class i.e. Customer class.
            PropertyInfo[] propInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            //Loop through each property of the Class i.e. Customer class.
            foreach (PropertyInfo propInfo in propInfos)
            {
                try
                {
                    //Add Columns in DataTable based on Property Name and Type.
                    dt.Columns.Add(new DataColumn(propInfo.Name, Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType));

                    //dt.Columns.Add(new DataColumn(propInfo.Name, propInfo.PropertyType));
                }
                catch
                {

                }

            }

            //Loop through the items if the Collection.
            foreach (T item in items)
            {
                //Add a new Row to DataTable.
                DataRow dr = dt.Rows.Add();

                //Loop through each property of the Class i.e. Customer class.
                foreach (PropertyInfo propInfo in propInfos)
                {
                    //Add value Column to the DataRow.
                    dr[propInfo.Name] = propInfo.GetValue(item, null);
                }
            }

            return dt;
        }

        /// <summary>
        /// GetItem.
        /// </summary>
        /// <typeparam name="T">T.</typeparam>
        /// <param name="dr">dr.</param>
        /// <param name="props">props.</param>
        /// <param name="propsName">propsName.</param>
        /// <returns>GetItem</returns>
        public static T GetItem<T>(DataRow dr, PropertyInfo[] props, string[] propsName)
        {
            T obj = Activator.CreateInstance<T>();
            foreach (DataColumn column in dr.Table.Columns)
            {
                int i = 0;
                foreach (PropertyInfo pro in props)
                {
                    if (string.Compare(propsName[i], column.ColumnName, true, CultureInfo.CurrentCulture) == 0)
                    {
                        if (dr[column.ColumnName] != DBNull.Value)
                        {
                            pro.SetValue(obj, dr[column.ColumnName], null);
                        }
                        else
                        {
                            pro.SetValue(obj, null, null);
                        }

                        break;
                    }

                    i++;
                }
            }

            return obj;
        }
    }
}
