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
        // GET: api/Fotoserie
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Fotoserie/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Fotoserie
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Fotoserie/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Fotoserie/5
        public void Delete(int id)
        {
        }
    }
}
