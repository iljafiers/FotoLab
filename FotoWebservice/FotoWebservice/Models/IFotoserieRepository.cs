using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FotoWebservice.Models
{
    interface IFotoserieRepository
    {
        IEnumerable<Fotoserie> GetAll();
        Fotoserie Get(int id);
        Fotoserie Add(Fotoserie item);

        void Remove(int id);
        bool Update(Fotoserie item);
    }
}
