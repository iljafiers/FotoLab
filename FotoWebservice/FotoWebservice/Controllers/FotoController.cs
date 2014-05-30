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
        // GET: api/Fotoserie/{fotoserie_id}/Foto
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Fotoserie/{fotoserie_id}/Foto/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Fotoserie/{fotoserie_id}/Foto
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Fotoserie/{fotoserie_id}/Foto/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Fotoserie/{fotoserie_id}/Foto/5
        public void Delete(int id)
        {
        }
    }
}
