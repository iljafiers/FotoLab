using FotoWebservice.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace FotoWebservice.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BestellingController : ApiController
    {
        IBestellingRepository repository;
        public BestellingController()
        {
            repository = new SqlBestellingRepository();
        }

        public BestellingController(IBestellingRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        [Route("api/klant/{klantKey}/bestelling")]
        public HttpResponseMessage PostBestelling(string klantKey, [FromBody]string newBestelling)
        {
            try
            {
                Bestelling bestelling = JsonConvert.DeserializeObject<Bestelling>(newBestelling);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
