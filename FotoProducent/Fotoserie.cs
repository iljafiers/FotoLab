using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FotoProducent
{
    public class Fotoserie
    {
        public int Id { get; set; } 
        public string Naam { get; set;}
        public int FotoproducentId { get; set;}
        public int KlantId { get; set; }
        public DateTime Datum { get; set; }
    }
}
