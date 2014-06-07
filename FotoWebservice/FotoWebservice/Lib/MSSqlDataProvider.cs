using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;

namespace FotoWebservice.Lib
{
    public class MSSqlDataProvider
    {
        private string connectionstring;

        public MSSqlDataProvider()
        {
            this.connectionstring = WebConfigurationManager.ConnectionStrings["fotolabdatabase"].ConnectionString;
        }


        public DataSet Query(SqlCommand cmd)
        {
            DataSet ds = new DataSet();

            using (SqlConnection connection = new SqlConnection(this.connectionstring))
            {
                connection.Open();
                ds = BuildDataSet(cmd);
                connection.Close();
            }

            return ds;
        }
        public DataSet Query(string sql, List<SqlParameter> parameters, bool storedProcedure = false)
        {
            DataSet ds = new DataSet();

            using (SqlConnection connection = new SqlConnection(this.connectionstring))
            {
                connection.Open();
                SqlCommand cmd = BuildSqlCommand(sql, parameters, connection, storedProcedure);
                ds = BuildDataSet(cmd);
                connection.Close();
            }

            return ds;
        }
        public DataSet Query(string sql, SqlParameter parameter, bool storedProcedure = false)
        {
            return Query(sql, new List<SqlParameter> { parameter }, storedProcedure);
        }
        public DataSet Query(string sql, bool storedProcedure = false)
        {
            return Query(sql, new List<SqlParameter>(), storedProcedure);
        }

        public void Execute(SqlCommand cmd)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionstring))
            {
                connection.Open();               
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void Execute(string sql, List<SqlParameter> parameters, bool storedProcedure = false)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionstring))
            {
                connection.Open();
                SqlCommand cmd = BuildSqlCommand(sql, parameters, connection, storedProcedure);

                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void Execute(string sql, SqlParameter parameter, bool storedProcedure = false)
        {
            Execute(sql, new List<SqlParameter> { parameter }, storedProcedure);
        }

        public void Execute(string sql, bool storedProcedure = false)
        {
            Execute(sql, new List<SqlParameter>(), storedProcedure);
        }

       /* public SqlDataReader QueryReader(string sql, List<SqlParameter> parameters)
        {
            SqlCommand cmd = BuildSqlCommand(sql, parameters);
            return cmd.ExecuteReader();
        }
        public SqlDataReader QueryReader(string sql, SqlParameter parameter)
        {
            SqlCommand cmd = BuildSqlCommand(sql, new List<SqlParameter> { parameter });
            return cmd.ExecuteReader();
        }
        public SqlDataReader QueryReader(string sql)
        {
            SqlCommand cmd = BuildSqlCommand(sql, new List<SqlParameter>());
            return cmd.ExecuteReader();
        }*/

        private SqlCommand BuildSqlCommand(string sql, List<SqlParameter> parameters, SqlConnection connection, bool storedProcedure)
        {
            SqlCommand cmd = new SqlCommand(sql, connection);

            if (storedProcedure)
            {
                cmd.CommandType = CommandType.StoredProcedure;
            }

            foreach (SqlParameter param in parameters)
            {
                cmd.Parameters.Add(param);
            }
            return cmd;
        }

        private DataSet BuildDataSet(SqlCommand sql)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = sql;
            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;
        }
    }
}