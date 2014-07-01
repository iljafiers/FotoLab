using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FotoWebservice.Lib;
using System.Data;

namespace FotoWebservice.Models
{
    public class SqlFotoProductenRepository : IFotoProductenRepository
    {
        MSSqlDataProvider dataProvider;

        public SqlFotoProductenRepository()
        {
            dataProvider = new MSSqlDataProvider();
        }
        public List<FotoProduct> GetAll()
        {
            List<FotoProduct> fotoproducten = new List<FotoProduct>();

            try
            {
                string sql = "SELECT * FROM fotoproducten";

                DataSet ds = dataProvider.Query(sql);

                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    fotoproducten.Add(DataRowToObject(r));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return fotoproducten;
        }

        private FotoProduct DataRowToObject(DataRow r)
        {
            return new FotoProduct
            {
                Id = Convert.ToInt32(r["id"]),
                Naam = Convert.ToString(r["naam"]),
                Meerprijs = Convert.ToDecimal(r["meerprijs"])
            };
        }
    }
}