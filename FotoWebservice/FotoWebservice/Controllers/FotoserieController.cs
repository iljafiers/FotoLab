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
        IFotoserieRepository repository;
        IFotoRepository fotoRepo;

        public FotoserieController()
        {
            this.repository = new SqlFotoserieRepository();
            this.fotoRepo = new SqlFotoRepository();
        }

        public FotoserieController(IFotoserieRepository repository, IFotoRepository fotoRepo)
        {
            this.repository = repository;
            this.fotoRepo = fotoRepo;
        }

        // GET: api/Fotoserie
        [HttpGet]
        [Route("api/fotoserie")]
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
        [Route("api/klant/{klantKey}/fotoserie")]
        public IEnumerable<Fotoserie> FindAllForKlant(string klantKey)
        {
            IEnumerable<Fotoserie> fotoseries = repository.FindAllForKlant(klantKey);
            Debug.WriteLine("fotoseries.count: " + fotoseries.Count().ToString());
            fotoRepo.GetAllForFotoserieList(fotoseries.ToList());

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
        [Route("api/fotoserie/{id:int}")]
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
        [Route("api/fotoserie/add")]
        public Fotoserie Post(Fotoserie fotoserie)
        {
            return repository.Add(fotoserie);
        }

        // PUT: api/Fotoserie/5
        [HttpPut]
        [Route("api/fotoserie/{id:int}")]
        public void Put(int id, Fotoserie fotoserie)
        {
            fotoserie.Id = id;
            if (!repository.Update(fotoserie))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        // DELETE: api/Fotoserie/5
        [HttpDelete]
        [Route("api/fotoserie/{id:int}")]
        public void Delete(int id)
        {
            repository.Remove(id);
        }
    }
}
