using FotoWebservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FotoWebservice.Controllers
{
    public class FotoController : ApiController
    {
        IFotoRepository repository = new SqlFotoRepository();

        // GET: api/Fotoserie/{fotoserie_id}/Foto
        public IEnumerable<int> Get(int fotoserieId)
        {
            IEnumerable<int> fotoIds = repository.GetAll(fotoserieId);

            if (fotoIds == null)
            {
                HttpResponseMessage resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("Er is iets mis gegaan."),
                    ReasonPhrase = "Interne server fout"
                };
                throw new HttpResponseException(resp);
            }

            return fotoIds;
        }

        // GET: api/Fotoserie/{fotoserie_id}/Foto/5
        public Byte[] Get(int fotoserieId, int id)
        {
            return repository.Get(fotoserieId, id);
        }

        // POST: api/Fotoserie/{fotoserie_id}/Foto
        public void Post(int fotoserieId, Byte[] fotoByteArray)
        {
            repository.Add(fotoserieId, fotoByteArray);
        }

        // DELETE: api/Fotoserie/{fotoserie_id}/Foto/5
        public void Delete(int fotoserieId, int id)
        {
            repository.Remove(fotoserieId, id);
        }
    }
}
