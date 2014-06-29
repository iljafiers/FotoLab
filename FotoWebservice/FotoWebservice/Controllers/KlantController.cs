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
        public void WijzigKlant(string klantKey, [FromBody]Klant klant)
        {

        }

        [HttpPut]
        [Route("api/klant/{id:int}")]
        public void WijzigKlant(int id, [FromBody]Klant klant)
        {

        }

        // DELETE: api/Klant/5
        public void Delete(int id)
        {
        }
    }
}
