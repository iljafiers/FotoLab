using FotoWebservice.Lib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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

        public int Add(int fotoserieId, string md5)
        {
            int id = 0;

    //        try
     //       {
                string sql = "sp_InsertFoto"; //"IF NOT EXISTS(SELECT 1 FROM fotos WHERE md5 = '@Md5') BEGIN INSERT INTO fotos (fotoserie_id, md5) OUTPUT INSERTED.ID AS Id VALUES (@FotoserieId, @md5) END";

                SqlParameter param1 = new SqlParameter("@fotoserieId", fotoserieId);
                param1.Direction = ParameterDirection.Input;
                param1.DbType = DbType.Int32;

                SqlParameter param2 = new SqlParameter("@md5", md5);
                param2.Direction = ParameterDirection.Input;
                param2.DbType = DbType.String;
                param2.Size = 32;

                List<SqlParameter> parameters = new List<SqlParameter> { param1, param2 }; 

                DataSet ds = dataProvider.Query(sql, parameters, true);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    id = Convert.ToInt32(ds.Tables[0].Rows[0]["Id"]);
                }               
       /*     }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }*/

            return id;
        }

        public Foto AddPath(int id, string fotoPath)
        {
            try
            {
                string sql = "UPDATE foto SET foto_path = @FotoPath WHERE id = @Id";
                List<SqlParameter> parameters = new List<SqlParameter> { 
                    new SqlParameter("Id", id),
                    new SqlParameter("FotoPath", fotoPath)
                };

                string debugSql = sql;
                foreach (SqlParameter p in parameters)
                {
                    debugSql = debugSql.Replace(p.ParameterName, p.Value.ToString());
                }
                Debug.WriteLine(debugSql);

                dataProvider.Execute(sql, parameters);

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