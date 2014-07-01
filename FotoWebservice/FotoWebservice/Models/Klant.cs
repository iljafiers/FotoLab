using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FotoWebservice.Models
{
    public class Klant
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Klant_key { get; set; }
        public string Straat { get; set; }
        public string Huisnummer { get; set; }
        public string Postcode { get; set; }
        public string Woonplaats { get; set; }
        public List<Bestelling> Bestellingen { get; set; }
    }
}