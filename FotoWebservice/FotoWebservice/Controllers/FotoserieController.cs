using FotoWebservice.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace FotoWebservice.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class FotoserieController : ApiController
    {
        IFotoserieRepository repository = new SqlFotoserieRepository();
        // GET: api/Fotoserie
        public IEnumerable<Fotoserie> GetAll()
        {
            IEnumerable<Fotoserie> fotoseries = repository.GetAll();
            Debug.WriteLine("fotoseries.count: " + fotoseries.Count().ToString());

            if (fotoseries == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("Er is iets mis gegaan."),
                    ReasonPhrase = "Interne server fout"
                };
                //throw new HttpResponseException(resp);
            }

            return fotoseries;
        }

        [HttpGet]
        [Route("api/klant/{klant_key}/fotoseries")]
        public IEnumerable<Fotoserie> FindAllForKlant(string klant_key)
        {
            IEnumerable<Fotoserie> fotoseries = repository.FindAllForKlant();
            Debug.WriteLine("fotoseries.count: " + fotoseries.Count().ToString());

            if (fotoseries == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("Er is iets mis gegaan."),
                    ReasonPhrase = "Interne server fout"
                };
                //throw new HttpResponseException(resp);
            }

            return fotoseries;
        }


        // GET: api/Fotoserie/5
        [HttpGet]
        [Route("api/klant/fotoserie/{id:int}")]
        public Fotoserie Get(int id)
        {
            Fotoserie fotoserie = repository.Get(id);
            if (fotoserie == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Er bestaat geen fotoserie met Id = {0}", id)),
                    ReasonPhrase = "Fotoserie Id niet gevonden"
                };
                throw new HttpResponseException(resp);
            }
            return fotoserie;
        }

        // POST: api/Fotoserie
        [HttpPost]
        [Route("api/Fotoserie/add")]
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
