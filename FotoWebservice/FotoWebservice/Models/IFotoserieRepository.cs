using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FotoWebservice.Models
{
    public interface IFotoserieRepository
    {
        IEnumerable<Fotoserie> GetAll();
        Fotoserie Get(int id);
        Fotoserie Add(Fotoserie fs);

        void Remove(int id);
        bool Update(Fotoserie item);

        IEnumerable<Fotoserie> FindAllForKlant();
    }
}
