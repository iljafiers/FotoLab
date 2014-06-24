using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FotoWebservice.Models
{
    public class MockKlantRepository : IKlantRepository
    {
        private List<Klant> klanten;

        public MockKlantRepository(List<Klant> klanten)
        {
            this.klanten = klanten;
        }
        public Klant Get(int id)
        {
            foreach (Klant klant in this.klanten)
            {
                if (klant.Id == id)
                {
                    return klant;
                }
            }
            return null;
        }

        public Klant GetByKey(string key)
        {
            foreach (Klant klant in this.klanten)
            {
                if (klant.Klant_key == key)
                {
                    return klant;
                }
            }
            return null;
        }
    }
}