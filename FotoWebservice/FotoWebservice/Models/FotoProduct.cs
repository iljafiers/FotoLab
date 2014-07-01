using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FotoWebservice.Models
{
    public class FotoProduct
    {
        public int Id { get;set;}
        public string Naam { get; set; }
        public decimal Meerprijs { get; set; }
    }
}