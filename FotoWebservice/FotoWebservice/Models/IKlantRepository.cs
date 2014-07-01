using System;
namespace FotoWebservice.Models
{
    public interface IKlantRepository
    {
        Klant Get(int id);
        Klant GetByKey(string key);

        void SaveKlant(Klant newKlant);
        Klant InsertKlant(Klant newKlant);
    }
}
