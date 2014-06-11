using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FotoWebservice.Models
{
    public class Fotoserie
    {
        private int id;
        private List<Foto> fotos;
        private string key;
        private DateTime datum;

        public int Id { get { return id; } set { id = value; } }
        public List<Foto> Fotos { get { return fotos; } set { fotos = value; } }
        public string Key { get { return key; } set { key = value; } }
        public DateTime Datum { get { return datum; } set { datum = value; } }
    }
}