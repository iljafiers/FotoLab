using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FotoWebservice.Models
{
    interface IFotoRepository
    {
        IEnumerable<int> GetAll(int fotoserieId);
        Foto Get(int fotoserieId, int id);
        Foto Add(int fotoserieId, Byte[] fotoByteArray);
        void Remove(int fotoserieId, int id);
    }
}
