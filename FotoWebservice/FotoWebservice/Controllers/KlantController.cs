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
            SqlKlantRepository repo = new SqlKlantRepository();
            Klant k = repo.Get(id);
            return k;
        }

        [HttpGet]
        [Route("api/klant/{klantkey}")]
        public Klant Get(string klantkey)
        {
            SqlKlantRepository repo = new SqlKlantRepository();
            Klant k = repo.GetByKey(klantkey);
            return k;
        }

        // POST: api/Klant
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Klant/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Klant/5
        public void Delete(int id)
        {
        }
    }
}
