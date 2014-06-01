using FotoWebservice.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace FotoWebservice.Controllers
{
    public class FotoController : ApiController
    {
        SqlFotoRepository repository = new SqlFotoRepository();
        FileFotoRepository filerepo = new FileFotoRepository();

        // GET: api/fotoserie/{fotoserie_id}/foto
        public IEnumerable<int> Get(int fotoserieId)
        {
            IEnumerable<int> fotoIds = repository.GetAll(fotoserieId);

            if (fotoIds == null)
            {
                HttpResponseMessage resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("Er is iets mis gegaan."),
                    ReasonPhrase = "Interne server fout"
                };
                throw new HttpResponseException(resp);
            }

            return fotoIds;
        }

        // GET: api/fotoserie/{fotoserie_id}/foto/5
        public Byte[] Get(int fotoserieId, int id)
        {
            return null;
          //  return repository.Get(fotoserieId, id);
        }

        // POST: api/fotoserie/{fotoserie_id}/foto
       /* public void Post(int fotoserieId, Byte[] fotoByteArray)
        {
            repository.Add(fotoserieId, fotoByteArray);
        }*/

        // DELETE: api/fotoserie/{fotoserie_id}/foto/5
        public void Delete(int fotoserieId, int id)
        {
            repository.Remove(fotoserieId, id);
        }

       /* [Route("~/fotoserie/{fotoserieId:int}/foto")]
        [HttpPost]*/
        public async Task<HttpResponseMessage> Post()
        {
            // http://www.asp.net/web-api/overview/working-with-http/sending-html-form-data,-part-2

            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            try
            {                
                int fotoserieId = FotoserieId();

                var provider = new MultipartFormDataStreamProvider(filerepo.TempPath);

                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                // This illustrates how to get the file names.
                foreach (MultipartFileData file in provider.FileData)
                {
                    int id = repository.Add(fotoserieId);

                    string extension = Path.GetExtension(file.Headers.ContentDisposition.FileName);


                    string fotoPath = filerepo.Add(file.LocalFileName, fotoserieId, id, extension);

                    repository.AddPath(id, fotoPath);

                    Debug.WriteLine(file.Headers.ContentDisposition.FileName);
                    Debug.WriteLine("Server file path: " + file.LocalFileName);
                }
                
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        private int FotoserieId()
        {
            // Het lukt niet om de url-parameter fotoserieId meteen mee te sturen en als parameter voor een functie te gebruiken.
            // Daarom op deze manier handmatig de parameter ophalen uit de request
            return Convert.ToInt32(Request.GetRouteData().Values["fotoserie_id"]);
        }
    }
}
