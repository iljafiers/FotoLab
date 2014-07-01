using FotoWebservice.Models;
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
    public class FotoProductenController : ApiController
    {
        IFotoProductenRepository repository;
        public FotoProductenController()
        {
            repository = new SqlFotoProductenRepository();
        }

        public FotoProductenController(IFotoProductenRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [Route("api/fotoproducten")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                List<FotoProduct> fotoseries = repository.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, fotoseries);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

    }
}
