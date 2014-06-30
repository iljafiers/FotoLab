using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using FotoWebservice.Lib;
using System.Diagnostics;
using System.Reflection;

namespace FotoWebservice.Models
{
    public class SqlKlantRepository : IKlantRepository
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
                 string sql = "SELECT * FROM klanten WHERE id = @Id";
                 List<SqlParameter> parameters = new List<SqlParameter> {
                     new SqlParameter("id", id)
                 };

                 DataSet ds = dataProvider.Query(sql, parameters);

                 return DataRowToObject(ds.Tables[0].Rows[0]);
             }
             catch (Exception ex)
             {
                 Debug.WriteLine(ex.Message + " | Line: " + new StackTrace(ex, true).GetFrame(0).GetFileLineNumber().ToString());
                 return null;
             }
        }

        public Klant GetByKey(string key) 
        {
            try
            {
                string sql = "SELECT * FROM klanten WHERE klant_key LIKE @Klant_key";
                List<SqlParameter> parameters = new List<SqlParameter> {
                     new SqlParameter("Klant_key", key)
                 };

                DataSet ds = dataProvider.Query(sql, parameters);

                return DataRowToObject(ds.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message + " | Line: " + new StackTrace(ex, true).GetFrame(0).GetFileLineNumber().ToString());
                return null;
            }
        }

        public void SaveKlant(Klant klant)
        {      
            try
            {
                string sql = 
                    "UPDATE klanten SET " + 
                    "naam = @Naam, " + 
                    "klant_key = @KlantKey, " +
                    "straat = @Straat, " +
                    "huisnummer = @Huisnummer, " +
                    "postcode = @Postcode, " +
                    "woonplaats = @Woonplaats " +
                    "WHERE id = @Id";

                List<SqlParameter> parameters = new List<SqlParameter> {
                    new SqlParameter("Naam", klant.Naam),
                    new SqlParameter("KlantKey", klant.Klant_key),
                    new SqlParameter("Straat", klant.Straat),
                    new SqlParameter("Huisnummer", klant.Huisnummer),
                    new SqlParameter("Postcode", klant.Postcode),
                    new SqlParameter("Woonplaats", klant.Woonplaats),
                    new SqlParameter("Id", klant.Id)
                };

                dataProvider.Execute(sql, parameters);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message + " | Line: " + new StackTrace(ex, true).GetFrame(0).GetFileLineNumber().ToString());
            }
        }

        public void InsertKlant(Klant newKlant)
        {
            throw new NotImplementedException();
        }

        private Klant DataRowToObject(DataRow row)
        {
            return new Klant
            {
                Id = Convert.ToInt32(row["id"]),
                Naam = Convert.ToString(row["naam"]),
                Klant_key = Convert.ToString(row["klant_key"]),
                Straat = Convert.ToString(row["straat"]),
                Huisnummer = Convert.ToString(row["huisnummer"]),
                Postcode = Convert.ToString(row["postcode"]),
                Woonplaats = Convert.ToString(row["woonplaats"])
            };
        }


    }
}