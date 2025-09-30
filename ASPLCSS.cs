using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace ASPL
{
    class ASPLCSS
    {       
        public static string connstr =ConfigurationManager.ConnectionStrings["ASPLDBCoonection"].ToString();      
        static SqlConnection con = new SqlConnection(connstr);
        public static SqlConnection connect()
        {
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
            }
            finally
            {
                //if (con.State != ConnectionState.Closed)
            }
            return con;
        }
}
}
