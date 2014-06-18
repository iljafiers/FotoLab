using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FotoWebservice.Models
{
    public class Klant
    {
        private int id;
        private string naam;
        private string klant_key;
        private string straat;
        private string huisnummer;
        private string postcode;
        private string woonplaats;

        /* Opmerking Pieter 18-06-2014: Beter om geen constructor te doen. Dan kun je ook een object initialiseren zonder alle parameters */
        /*public Klant(int id, string naam, string klant_key)
        {
            this.id = id;
            this.naam = naam;
            this.klant_key = klant_key;
        }*/

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
        public string Straat
        {
            get { return straat; }
            set { straat = value; }
        }
        public string Huisnummer
        {
            get { return huisnummer; }
            set { huisnummer = value; }
        }
        public string Postcode
        {
            get { return postcode; }
            set { postcode = value; }
        }
        public string Woonplaats
        {
            get { return woonplaats; }
            set { woonplaats = value; }
        }
    }
}