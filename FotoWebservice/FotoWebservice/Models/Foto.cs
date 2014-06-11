using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FotoWebservice.Models
{
    public class Foto
    {
        private int id;
        private string md5;
        public int Id { get { return id; } set { id = value; } }
        public string Md5 { get { return md5; } set { md5 = value; } }
    }
}