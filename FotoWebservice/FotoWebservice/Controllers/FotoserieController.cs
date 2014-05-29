using FotoWebservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FotoWebservice.Controllers
{
    public class FotoserieController : ApiController
    {
        IFotoserieRepository repository = new SqlFotoserieRepository();
        // GET: api/Fotoserie
        public IEnumerable<Fotoserie> GetAll()
        {
            return repository.GetAll();
        }

        // GET: api/Fotoserie/5
        public Fotoserie Get(int id)
        {
            return repository.Get(id);
        }

        // POST: api/Fotoserie
        public HttpResponseMessage Post(Fotoserie fotoserie)
        {
            fotoserie = repository.Add(fotoserie);

            var response = Request.CreateResponse<Fotoserie>(HttpStatusCode.Created, fotoserie);

            string uri = Url.Link("FotoserieApi", new { id = fotoserie.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        // PUT: api/Fotoserie/5
        public void Put(int id, Fotoserie fotoserie)
        {
            fotoserie.Id = id;
            if (!repository.Update(fotoserie))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        // DELETE: api/Fotoserie/5
        public void Delete(int id)
        {
            repository.Remove(id);
        }
    }
}
