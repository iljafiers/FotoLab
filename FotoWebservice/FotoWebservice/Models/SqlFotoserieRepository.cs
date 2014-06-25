using FotoWebservice.Lib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Http; 

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
            try
            {
                string sql = "SELECT * FROM fotoseries";

                DataSet ds = dataProvider.Query(sql);

                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    fotoseries.Add(DataRowToObject(r));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return fotoseries;
        }

        public IEnumerable<Fotoserie> FindAllForKlant()
        {
            List<Fotoserie> fotoseries = new List<Fotoserie>();
            try
            {
                string sql = "SELECT * FROM fotoseries";

                DataSet ds = dataProvider.Query(sql);

                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    fotoseries.Add(DataRowToObject(r));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return fotoseries;
        }

        public Fotoserie Get(int id)
        {
            try
            {
                string sql = "SELECT * FROM fotoseries WHERE id = @Id";
                SqlParameter parameter = new SqlParameter("Id", id);

                DataSet ds = dataProvider.Query(sql, parameter);

                return DataRowToObject(ds.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Fotoserie Add([FromBody]Fotoserie fs)
        {
            try
            {
                string sql = "INSERT INTO fotoseries (naam, datum, fotoproducent_id, klant_id) " +
                             "OUTPUT INSERTED.ID AS Id " +
                             "VALUES (@naam, @datum, @fotoproducent_id, @klant_id)";
                List<SqlParameter> parameters = new List<SqlParameter> { 
                    new SqlParameter("naam", fs.Naam),
                    new SqlParameter("datum", fs.Datum),
                    new SqlParameter("fotoproducent_id", fs.FotoproducentId),
                    new SqlParameter("klant_id", fs.KlantId)
                };

                DataSet ds = dataProvider.Query(sql, parameters);
                fs.Id = Convert.ToInt32(ds.Tables[0].Rows[0]["Id"]);

                return fs;
            }
            catch (Exception ex)
            {
                Console.Error.Write( ex.Message );
                return null;
            }
        }

        //public Fotoserie Add([FromBody]string naam, [FromBody]int klantId)
        //{
        //    // relay to Add(FotoSerie)
        //    Fotoserie fs = new Fotoserie();
        //    fs.Naam = naam;
        //    fs.KlantId = klantId;

        //    return Add(fs);
        //}

        public void Remove(int id)
        {
            try
            {
                string sql = "DELETE FROM fotoseries WHERE id = @Id";
                SqlParameter parameter = new SqlParameter("Id", id);

                dataProvider.Query(sql, parameter);
            }
            catch (Exception ex)
            {
                Console.Error.Write(ex.Message);
                return;
            }
        }

        public bool Update(Fotoserie fotoserie)
        {
            try
            {
                string sql = "UPDATE fotoseries SET naam = @Naam WHERE id = @Id";
                List<SqlParameter> parameters = new List<SqlParameter> { 
                    new SqlParameter("Id", fotoserie.Id),
                    new SqlParameter("Naam", fotoserie.Naam)
                };

                dataProvider.Query(sql, parameters);

                return true;
            }
            catch (Exception ex)
            {
                Console.Error.Write(ex.Message);
                return false;
            }
        }

        public int FindIdForKey(string fotoserieKey)
        {
            int id = 0;

            try
            {
                string sql = "SELECT id FROM fotoseries WHERE fotoserie_key = @FotoserieKey";
                SqlParameter parameter = new SqlParameter("FotoserieKey", fotoserieKey);

                DataSet ds = dataProvider.Query(sql, parameter);
                id = Convert.ToInt32(ds.Tables[0].Rows[0]["Id"]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return id;
        }

        private Fotoserie DataRowToObject(DataRow row)
        {
            return new Fotoserie { 
                Id  = Convert.ToInt32(row["id"]),
                Naam = Convert.ToString(row["naam"]),
                Datum = Convert.ToDateTime(row["datum"])
            };
        }

    }
}