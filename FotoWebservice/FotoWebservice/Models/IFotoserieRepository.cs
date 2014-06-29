using System;
using System.Collections.Generic;
namespace FotoWebservice.Models
{
    public interface IFotoserieRepository
    {
        Fotoserie Add(Fotoserie fs);
        IEnumerable<Fotoserie> FindAllForKlant(string klantKey);
        int FindIdForKey(string fotoserieKey);
        Fotoserie Get(int id);
        IEnumerable<Fotoserie> GetAll();
        void Remove(int id);
        bool Update(Fotoserie fotoserie);
    }
}
