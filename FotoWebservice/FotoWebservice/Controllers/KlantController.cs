using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FotoWebservice.Models;
using System.Web.Http.Cors;

namespace FotoWebservice.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class KlantController : ApiController
    {
        private IKlantRepository repo;
        public KlantController()
        {
            this.repo = new SqlKlantRepository();
            // KlantController heeft een Repository Interface nodig omdat ik dan een Mock-Repository kan maken voor de unit-tests
            // Ik gebruik expres 'this' omdat dan heel duidelijk is dat het een instance variabele is en geen lokale variabele
        }

        public KlantController(IKlantRepository repo)
        {
            this.repo = repo;
        }

        // GET: api/Klant
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Klant/5
        [HttpGet]
        [Route("api/klant/{id:int}")]
        public Klant Get(int id)
        {
            Klant k = this.repo.Get(id);
            return k;
        }

        [HttpGet]
        [Route("api/klant/{klantkey}")]
        public Klant Get(string klantkey)
        {
            Klant k = this.repo.GetByKey(klantkey);
            return k;
        }

        // POST: api/Klant
        public void Post([FromBody]string value)
        {
        }

        [HttpPut]
        [Route("api/klant/{klantkey}")]
        public HttpResponseMessage UpdateKlant(string klantKey, [FromBody]Klant newKlant)
        {
            try
            {
                Klant klant = repo.GetByKey(klantKey);
                if (klant.Klant_key == newKlant.Klant_key)
                {
                    newKlant.Id = klant.Id;
                    repo.SaveKlant(newKlant);
                    return Request.CreateResponse(HttpStatusCode.Created);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
            
        }

        [HttpPut]
        [Route("api/klant/{id:int}")]
        public void UpdateKlant(int id, [FromBody]Klant newKlant)
        {
            newKlant.Id = id;
            repo.SaveKlant(newKlant);
        }

        // DELETE: api/Klant/5
        public void Delete(int id)
        {
        }
    }
}
