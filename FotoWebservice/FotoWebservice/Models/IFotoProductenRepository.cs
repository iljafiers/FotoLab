using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FotoWebservice.Models
{
    public interface IFotoProductenRepository
    {
        List<FotoProduct> GetAll();
    }
}
