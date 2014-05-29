using FotoWebservice.Lib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration; 

namespace FotoWebservice.Models
{
    public class SqlFotoserieRepository : IFotoserieRepository
    {
        private MSSqlDataProvider dataProvider;
        public SqlFotoserieRepository()
        {
            this.dataProvider = new MSSqlDataProvider();
        }

        public IEnumerable<Fotoserie> GetAll()
        {
            List<Fotoserie> fotoseries = new List<Fotoserie>();
            
            string sql ="SELECT * FROM fotoserie";

            DataSet ds = dataProvider.Query(sql);

            foreach (DataRow r in ds.Tables[0].Rows)
            {
                fotoseries.Add(DataRowToObject(r));
            }

            return fotoseries;
        }

        public Fotoserie Get(int id)
        {
            string sql = "SELECT * FROM fotoserie WHERE id = @Id";
            SqlParameter parameter = new SqlParameter("Id", id);

            DataSet ds = dataProvider.Query(sql, parameter);

            return DataRowToObject(ds.Tables[0].Rows[0]);
        }

        public Fotoserie Add(Fotoserie fotoserie)
        {
            string sql = "INSERT INTO fotoserie SET id = @Id, serie_key = @Key";
            List<SqlParameter> parameters = new List<SqlParameter> { 
                new SqlParameter("Id", fotoserie.Id),
                new SqlParameter("Key", fotoserie.Key)
            };

            dataProvider.Query(sql, parameters);

            return fotoserie;
        }

        public void Remove(int id)
        {
            string sql = "DELETE FROM fotoserie WHERE id = @Id";
            SqlParameter parameter = new SqlParameter("Id", id);

            dataProvider.Query(sql, parameter);
        }

        public bool Update(Fotoserie fotoserie)
        {
            string sql = "UPDATE fotoserie SET serie_key = @Key WHERE id = @Id";
            List<SqlParameter> parameters = new List<SqlParameter> { 
                new SqlParameter("Id", fotoserie.Id),
                new SqlParameter("Key", fotoserie.Key)
            };

            dataProvider.Query(sql, parameters);

            return true;
        }

        private Fotoserie DataRowToObject(DataRow row)
        {
            return new Fotoserie { 
                Id  = Convert.ToInt32(row["id"]), 
                Key = Convert.ToString(row["serie_key"]) 
            };
        }
    }
}