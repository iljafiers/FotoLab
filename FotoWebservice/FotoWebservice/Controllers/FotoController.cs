using FotoWebservice.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace FotoWebservice.Controllers
{
    public class FotoController : ApiController
    {
        SqlFotoRepository repository = new SqlFotoRepository();
        FileFotoRepository filerepo = new FileFotoRepository();

        // GET: api/fotoserie/{fotoserie_id}/foto/all
        public IEnumerable<int> GetAll()
        {
            string fotoserieKey = this.FotoserieKey();

            int fotoserieId = new SqlFotoserieRepository().FindIdForKey(fotoserieKey);
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
/*        [HttpGet]
        [Route("~/fotoserie/{fotoserie_key}/foto/{id}")]*/
        public HttpResponseMessage Get()
        {
            string fotoserieKey = this.FotoserieKey();
            int fotoId = this.Id();

            int fotoserieId = new SqlFotoserieRepository().FindIdForKey(fotoserieKey);
            string fullPath = filerepo.Get(fotoserieId, fotoId);

            Debug.WriteLine("fullPath: " + fullPath);
            Debug.WriteLine("fotoserieKey: " + fotoserieKey);
            Debug.WriteLine("fotoserieId: " + fotoserieId);
            Debug.WriteLine("fotoId: " + fotoId.ToString()); 

            if (fullPath != string.Empty)
            {
                HttpResponseMessage response = new HttpResponseMessage();
                response.Content = new StreamContent(new FileStream(fullPath, FileMode.Open));
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");

                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        // POST: api/fotoserie/{fotoserie_id}/foto
       /* public void Post(int fotoserieId, Byte[] fotoByteArray)
        {
            repository.Add(fotoserieId, fotoByteArray);
        }*/

        // DELETE: api/fotoserie/{fotoserie_id}/foto/5
        public void Delete(string fotoserieKey, int id)
        {
            int fotoserieId = new SqlFotoserieRepository().FindIdForKey(fotoserieKey);

            repository.Remove(fotoserieId, id);
        }

       /* [Route("~/fotoserie/{fotoserieId:int}/foto")]
        [HttpPost]*/
        public async Task<HttpResponseMessage> Post() // parameter: fotoserie_key
        {
            // http://www.asp.net/web-api/overview/working-with-http/sending-html-form-data,-part-2

            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            try
            {
                string fotoserieKey = FotoserieKey();
                int fotoserieId = new SqlFotoserieRepository().FindIdForKey(fotoserieKey);

                var provider = new MultipartFormDataStreamProvider(filerepo.TempPath);

                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                // This illustrates how to get the file names.
                foreach (MultipartFileData file in provider.FileData)
                {
                    string md5 = FileFotoRepository.CalculateMD5Hash(File.ReadAllBytes(file.LocalFileName));
                    int id = repository.Add(fotoserieId, md5);

                    if (id != null && id > 0) // id > 0 dus het plaatje is toegevoegd aan de database
                    {
                        string originalFilename = file.Headers.ContentDisposition.FileName.ToString().ToLower();
                        originalFilename = FileFotoRepository.RemoveBadPathChars(originalFilename);

                        string[] pointParts = originalFilename.Split('.'); //CommonUtils.GetFileExtension(originalFilename); //  Path.GetExtension(originalFilename);//info.Extension; // 
                        string extension = "." + pointParts.Last();

                        string fotoPath = filerepo.Add(file.LocalFileName, fotoserieId, id, extension);

                        return Request.CreateResponse(HttpStatusCode.Created);
                    }
                    else // Niet kunnen toevoegen aan de database, dus plaatje bestond al, dus temp file verwijderen
                    {
                        File.Delete(file.LocalFileName);
                        return Request.CreateResponse(HttpStatusCode.Conflict);
                    }
                }

                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        private int Id()
        {
            // Het lukt niet om de url-parameter fotoserieId meteen mee te sturen en als parameter voor een functie te gebruiken.
            // Daarom op deze manier handmatig de parameter ophalen uit de request
            return Convert.ToInt32(Request.GetRouteData().Values["id"]);
        }

        private string FotoserieKey()
        {
            return Convert.ToString(Request.GetRouteData().Values["fotoserie_key"]);
        }
    }
}
