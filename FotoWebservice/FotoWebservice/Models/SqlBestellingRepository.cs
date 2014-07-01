using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FotoWebservice.Lib;

namespace FotoWebservice.Models
{
    public class SqlBestellingRepository : IBestellingRepository
    {
        MSSqlDataProvider dataProvider;
        public SqlBestellingRepository()
        {
            dataProvider = new MSSqlDataProvider();
        }


    }
}