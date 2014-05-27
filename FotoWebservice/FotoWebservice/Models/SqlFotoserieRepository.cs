using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FotoWebservice.Models
{
    public class SqlFotoserieRepository : IFotoserieRepository
    {
        private SqlConnection conn;
        public SqlFotoserieRepository()
        {
            this.conn = new SqlConnection("Server=localhost\myInstanceName;Database=fotolabdatabase;User Id=myUsername;Password=myPassword;");
        }

        public IEnumerable<Fotoserie> GetAll()
        {
            throw new NotImplementedException();
        }

        public Fotoserie Get(int id)
        {
            throw new NotImplementedException();
        }

        public Fotoserie Add(Fotoserie item)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Fotoserie item)
        {
            throw new NotImplementedException();
        }
    }
}