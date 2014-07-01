using System;
using System.Collections.Generic;
namespace FotoWebservice.Models
{
    public interface IFotoRepository
    {
        int Add(int fotoserieId, string md5, decimal bedrag);
        IEnumerable<int> GetAll(int fotoserieId);
        void GetAllForFotoserieList(System.Collections.Generic.List<Fotoserie> fotoseries);
        void Remove(int fotoserieId, int id);
    }
}
