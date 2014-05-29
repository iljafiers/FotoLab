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
        public SqlFotoserieRepository(MSSqlDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public IEnumerable<Fotoserie> GetAll()
        {
            List<Fotoserie> fotoseries = new List<Fotoserie>();
            
            SqlCommand cmd = new SqlCommand("SELECT * FROM fotoserie");

            DataSet ds = dataProvider.Query(cmd);

            foreach (DataRow r in ds.Tables[0].Rows)
            {
                fotoseries.Add(DataRowToObject(r));
            }

            return fotoseries;
        }

        public Fotoserie Get(int id)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM fotoserie WHERE id = @Id");
            SqlParameter parameter = new SqlParameter("Id", id);

            DataSet ds = dataProvider.Query(cmd, parameter);

            return DataRowToObject(ds.Tables[0].Rows[0]);
        }

        public Fotoserie Add(Fotoserie fotoserie)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO fotoserie SET id = @Id, serie_key = @Key");
            List<SqlParameter> parameters = new List<SqlParameter> { 
                new SqlParameter("Id", fotoserie.Id),
                new SqlParameter("Key", fotoserie.Key)
            };

            dataProvider.Query(cmd, parameters);

            return fotoserie;
        }

        public void Remove(int id)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM fotoserie WHERE id = @Id");
            SqlParameter parameter = new SqlParameter("Id", id);

            dataProvider.Query(cmd, parameter);
        }

        public bool Update(Fotoserie fotoserie)
        {
            SqlCommand cmd = new SqlCommand("UPDATE fotoserie SET serie_key = @Key WHERE id = @Id");
            List<SqlParameter> parameters = new List<SqlParameter> { 
                new SqlParameter("Id", fotoserie.Id),
                new SqlParameter("Key", fotoserie.Key)
            };

            dataProvider.Query(cmd, parameters);

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