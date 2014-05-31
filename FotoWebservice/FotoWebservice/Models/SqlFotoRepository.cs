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
    public class SqlFotoRepository : IFotoRepository
    {
        private MSSqlDataProvider dataProvider;
        public SqlFotoRepository()
        {
            this.dataProvider = new MSSqlDataProvider();
        }

        /* Vraag foto ids voor fotoserie op */
        public IEnumerable<int> GetAll(int fotoserieId)
        {
            List<int> fotoIds = new List<int>();
            try
            {
                string sql = "SELECT id FROM foto WHERE fotoserie_id = @FotoserieId";
                SqlParameter parameter = new SqlParameter("FotoserieId", fotoserieId);

                DataSet ds = dataProvider.Query(sql, parameter);

                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    fotoIds.Add(Convert.ToInt32(r["id"]));
                }

                return fotoIds;
            }
            catch (Exception ex)
            {
                fotoIds = null;
                return fotoIds;
            }
        }

        /* Vraag een foto op */
        //http://www.codeguru.com/csharp/.net/returning-images-from-asp.net-web-api.htm
        public Byte[] Get(int fotoserieId, int id)
        {
            try
            {
                string sql = "SELECT path FROM foto WHERE fotoserie_id = @FotoserieId AND id = @Id";
                List<SqlParameter> parameters = new List<SqlParameter> {
                    new SqlParameter("Id", id),
                    new SqlParameter("FotoserieId", fotoserieId)
                };

                DataSet ds = dataProvider.Query(sql, parameters);

                string path = Convert.ToString(ds.Tables[0].Rows[0]["path"]);

                return null;


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Foto Add(int fotoserieId, Byte[] fotoByteArray)
        {
            return null;

            /*try
            {
                string sql = "INSERT INTO foto (serie_key) OUTPUT INSERTED.ID AS Id VALUES (@Key)";
                List<SqlParameter> parameters = new List<SqlParameter> { 
                    new SqlParameter("fotoserie_id", foto.Key)
                };

                DataSet ds = dataProvider.Query(sql, parameters);
                foto.Id = Convert.ToInt32(ds.Tables[0].Rows[0]["Id"]);

                return foto;
            }
            catch (Exception ex)
            {
                return null;
            }*/
        }

        public void Remove(int fotoserieId, int id)
        {
            try
            {
                string sql = "DELETE FROM fotoserie WHERE id = @Id";
                SqlParameter parameter = new SqlParameter("Id", id);

                dataProvider.Query(sql, parameter);
            }
            catch (Exception ex)
            {
                return;
            }
        }

      /*  public bool Update(int fotoserieId, Foto foto)
        {
            try
            {
                string sql = "UPDATE fotoserie SET serie_key = @Key WHERE id = @Id";
                List<SqlParameter> parameters = new List<SqlParameter> { 
                    new SqlParameter("Id", foto.Id),
                    new SqlParameter("Key", foto.Key)
                };

                dataProvider.Query(sql, parameters);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }*/

       /* private Foto DataRowToObject(DataRow row)
        {
            return new Foto
            {
                Id = Convert.ToInt32(row["id"]),
                Url = Convert.ToString(row[""])
            };
        }*/
    }
}