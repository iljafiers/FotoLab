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
    public class SqlFotoRepository
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

        public int Add(int fotoserieId)
        {
            try
            {
                string sql = "INSERT INTO foto (fotoserie_id) OUTPUT INSERTED.ID AS Id VALUES (@FotoserieId)";
                List<SqlParameter> parameters = new List<SqlParameter> { 
                    new SqlParameter("FotoserieId", fotoserieId)
                };

                DataSet ds = dataProvider.Query(sql, parameters);
                int id = Convert.ToInt32(ds.Tables[0].Rows[0]["Id"]);

                return id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public Foto AddPath(int id, string fotoPath)
        {
            try
            {
                string sql = "UPDATE foto (foto_path) VALUES (@FotoPath) WHERE id = @Id";
                List<SqlParameter> parameters = new List<SqlParameter> { 
                    new SqlParameter("Id", id),
                    new SqlParameter("FotoPath", fotoPath)
                };

                dataProvider.Query(sql, parameters);

                Foto foto = new Foto();
                foto.Id = id;
                foto.Path = fotoPath;

                return foto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
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