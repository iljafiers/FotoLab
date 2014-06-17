using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using FotoWebservice.Lib;

namespace FotoWebservice.Models
{
    public class SqlKlantRepository
    {
        private MSSqlDataProvider dataProvider;

        public SqlKlantRepository()
        {
            this.dataProvider = new MSSqlDataProvider();
        }

        // Vraag 1 klant op
        //http://www.codeguru.com/csharp/.net/returning-images-from-asp.net-web-api.htm
        public Klant Get(int id)
         {
             try
             {
                 string sql = "SELECT id,naam, klant_key FROM klanten WHERE id = @Id";
                 List<SqlParameter> parameters = new List<SqlParameter> {
                     new SqlParameter("id", id)
                 };

                 DataSet ds = dataProvider.Query(sql, parameters);

                 return new Klant(
                                Convert.ToInt32(ds.Tables[0].Rows[0]["id"]),
                                Convert.ToString(ds.Tables[0].Rows[0]["naam"]),
                                Convert.ToString(ds.Tables[0].Rows[0]["klant_key"])
                                    );

             }
             catch (Exception ex)
             {
                 return null;
             }
         }
    }
}