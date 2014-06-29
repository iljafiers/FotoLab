using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FotoProducent
{
    public class Klant
    {
        public Klant()
        {
            Id = -1;
            Naam = "uninitialised";
            Klant_key = "none";
        }

        public Klant(int id, string naam, string klant_key)
        {
            this.Id = id;
            this.Naam = naam;
            this.Klant_key = klant_key;
        }

        public int Id { get; set; }
        public string Naam { get; set; }
        public string Klant_key { get; set; }
        public string Straat { get; set; }
        public string Huisnummer { get; set; }
        public string Postcode { get; set; }
        public string Woonplaats { get; set; }
    }
}
