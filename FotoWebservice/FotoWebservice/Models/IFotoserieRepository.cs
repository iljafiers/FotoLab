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
        Fotoserie Add(string naam, int fotoProducentId, int klantId);

        void Remove(int id);
        bool Update(Fotoserie item);

        IEnumerable<Fotoserie> FindAllForKlant();
    }
}
