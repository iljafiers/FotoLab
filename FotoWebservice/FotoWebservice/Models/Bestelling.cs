using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FotoWebservice.Models
{
    public class Bestelling
    {
        public DateTime Datum { get; set; }
        public List<BestelRegel> BestelRegels { get; set; }
        public PayPalBetaalMethode BetaalMethode { get; set; }
    }
}