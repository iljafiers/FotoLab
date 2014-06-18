using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FotoProducent
{
    public class Klant
    {
        private int id;
        private string naam;
        private string klant_key;

        public Klant()
        {
            id = -1;
            naam = "uninitialised";
            klant_key = "none";
        }

        public Klant(int id, string naam, string klant_key)
        {
            this.id = id;
            this.naam = naam;
            this.klant_key = klant_key;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Naam
        {
            get { return naam; }
            set { naam = value; }
        }
        public string Klant_key
        {
            get { return klant_key; }
            set { klant_key = value; }
        }
    }
}
