using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ASPL.Models.Common
{
    public abstract class DBConnect
    {
        protected SqlConnection sqlConnection;

        public DBConnect()
        {
            sqlConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ASPLDBCoonection"].ConnectionString.ToString());
        }

        protected DataSet GetDataSetByQuery(string sqlQuery)
        {
            using (var da = new SqlDataAdapter(sqlQuery, sqlConnection))
            {
                var ds = new DataSet();

                da.Fill(ds);

                return ds;
            }
        }

        protected DataTable GetDataTableByQuery(string sqlQuery)
        {
            var ds = GetDataSetByQuery(sqlQuery);
            var dt = new DataTable();

            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        protected DataSet GetDataSetByProcedure(string procedureName, Dictionary<string, object> parameters = null)
        {

            using (var cmd = new SqlCommand())
            {
                cmd.Connection = sqlConnection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = procedureName;
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        cmd.Parameters.AddWithValue(param.Key, param.Value);
                    }
                }

                using (var da = new SqlDataAdapter(cmd))
                {
                    var ds = new DataSet();
                    da.Fill(ds);

                    return ds;
                }
            }

        }

        protected DataSet GetDataSetByProcedure(string procedureName, SqlParameter[] parameters = null)
        {

            using (var cmd = new SqlCommand())
            {
                cmd.Connection = sqlConnection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = procedureName;
                cmd.Parameters.AddRange(parameters);

                using (var da = new SqlDataAdapter(cmd))
                {
                    var ds = new DataSet();
                    da.Fill(ds);

                    return ds;
                }
            }

        }



        protected DataTable GetDataTableByProcedure(string procedureName, Dictionary<string, object> parameters = null)
        {
            var ds = GetDataSetByProcedure(procedureName, parameters);
            var dt = new DataTable();

            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        protected DataRow GetDataRowByProcedure(string procedureName, Dictionary<string, object> parameters = null)
        {
            var dt = GetDataTableByProcedure(procedureName, parameters);

            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0];
            }

            return dt.NewRow();
        }

        protected DataRow GetDataRowByQuery(string sqlQuery)
        {
            var dt = GetDataTableByQuery(sqlQuery);

            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0];
            }

            return dt.NewRow();
        }

        protected bool SetDataByProcedure(string procedureName, Dictionary<string, object> parameters = null)
        {
            try
            {
                var ds = GetDataSetByProcedure(procedureName, parameters);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}