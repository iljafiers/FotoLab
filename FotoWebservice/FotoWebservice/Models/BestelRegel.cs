using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FotoWebservice.Models
{
    public class BestelRegel
    {
        public Foto Foto { get; set; }
        public FotoProduct FotoProduct { get; set; }
    }
}
