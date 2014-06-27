using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FotoWebservice.Models
{
    public class Fotoserie
    {
        private List<Foto> fotos;

        public int Id { get; set; } 
        public string Naam { get; set;}
        public int KlantId { get; set; }
        public int FotoproducentId { get; set; }
        public DateTime Datum { get; set; }
        public string Key { get; set; }
        public List<Foto> Fotos { get { return fotos; } set { fotos = value; } }
    }
}