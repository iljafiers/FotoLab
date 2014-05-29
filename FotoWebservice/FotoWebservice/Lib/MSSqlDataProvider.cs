using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Configuration;
using System.Data.SqlClient;

namespace FotoWebservice.Lib
{
    public class MSSqlDataProvider
    {
        private SqlConnection conn;

        public MSSqlDataProvider()
        {
            this.conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["fotolabdatabase"].ConnectionString);
            this.Open();
        }
        ~MSSqlDataProvider()
        {
            this.Close();
        }
        public void Open()
        {
            if (!IsOpen())
            {
                this.conn.Open();
            }
        }

        public void Close()
        {
            if (IsOpen())
            {
                this.conn.Close();
            }
        }
        public bool IsOpen()
        {
            return (this.conn != null && this.conn.State != System.Data.ConnectionState.Closed);
        }

        public DataSet Query(SqlCommand sql, List<SqlParameter> parameters)
        {
            InjectParameters(sql, parameters);
            return BuildDataSet(sql);
        }
        public DataSet Query(SqlCommand sql, SqlParameter parameter)
        {
            InjectParameters(sql, new List<SqlParameter> { parameter });
            return BuildDataSet(sql);
        }
        public DataSet Query(SqlCommand sql)
        {
            return BuildDataSet(sql);
        }

        public SqlDataReader QueryReader(SqlCommand sql, List<SqlParameter> parameters)
        {
            InjectParameters(sql, parameters);
            return sql.ExecuteReader();
        }
        public SqlDataReader QueryReader(SqlCommand sql, SqlParameter parameter)
        {
            InjectParameters(sql, new List<SqlParameter> { parameter });
            return sql.ExecuteReader();
        }
        public SqlDataReader QueryReader(SqlCommand sql)
        {
            return sql.ExecuteReader();
        }

        private void InjectParameters(SqlCommand sql, List<SqlParameter> parameters)
        {
            foreach (SqlParameter param in parameters)
            {
                sql.Parameters.Add(param);
            }
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