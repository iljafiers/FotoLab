using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FotoWebservice.Models;

namespace FotoWebservice.Controllers
{
    public class KlantController : ApiController
    {
        // GET: api/Klant
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Klant/5
        public Klant Get(int id)
        {
            SqlKlantRepository repo = new SqlKlantRepository();
            Klant k = repo.Get(id);
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
