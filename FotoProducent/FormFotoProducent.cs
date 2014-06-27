using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Forms;
using System.Web;
using System.Web.Script.Serialization;
using System.Threading.Tasks;
using System.Threading;

namespace FotoProducent
{
    public partial class FormFotoProducent : Form
    {
        private Fotoserie m_fotoserie;
        private Klant m_klant;

        public FormFotoProducent()
        {
            InitializeComponent();

            m_fotoserie = new Fotoserie();
            m_klant = new Klant();
        }


        private void OnClickOphalenKlant(object sender, EventArgs e)
        {

            string klantKey = this.textBoxKlantID.Text;

            string url = "http://localhost:2372/api/Klant/" + klantKey;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                string JSON;
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    JSON =  reader.ReadToEnd();
                }

                // parse JSON
                var serializer = new JavaScriptSerializer();
                m_klant = serializer.Deserialize<Klant>(JSON);

                if (m_klant != null)
                {
                    this.KlantNaam.Text = m_klant.Naam;
                }

            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.Message, "FotoProducent error", MessageBoxButtons.OK);
            }
        }



        private void OnClickBrowse(object sender, EventArgs e)
        {
            // Displays an OpenFileDialog so the user can select a Cursor.
            OpenFileDialog fotos = new OpenFileDialog();
            fotos.Filter = "Fotos|*.jpg";
            fotos.Title = "Selecteer de fotos";
            fotos.Multiselect = true;

            // Show the Dialog.
            // If the user clicked OK in the dialog and
            // a .CUR file was selected, open it.
            if (fotos.ShowDialog() == DialogResult.OK)
            {
                // voeg alle geselecteerde fotos to aan de listbox
                foreach (string fotoFile in fotos.FileNames)
                {
                    if (listBoxFotos.FindStringExact( fotoFile) == -1)
                        listBoxFotos.Items.Add(fotoFile);
                }
            }
        }

        private void OnClickUpload(object sender, EventArgs e)
        {
            try
            {
                // maak een instantie van de klasse fotoserie zoals we die willen gaan 
                // toevoegen aan de databse via de WebAPI.

                m_fotoserie.Naam = textBoxFotoSerieNaam.Text;
                m_fotoserie.KlantId = m_klant.Id;
                m_fotoserie.FotoproducentId = 0;
                m_fotoserie.Datum = DateTime.Now;
                m_fotoserie.Key = Utility.GenerateRandomString(20);

                // Maak JSON text van de fotoserie
                var serializer = new JavaScriptSerializer();
                string JSON = serializer.Serialize(m_fotoserie);

                // stuur deze naar de webapi, we krijgen een JSON string
                // terug die de volledige Fotoserie wergeeft.
                // Dat is nodig om de Id te kunnen bepalen, we gaan er zo direct fotos
                // aan hangen.
                string url = "http://localhost:2372/api/Fotoserie/add";
                var cli = new WebClient();
                cli.Headers[HttpRequestHeader.ContentType] = "application/json";
                string responseJSON = cli.UploadString(url, JSON);

                // response JSON vertalen naar een instantie van Klasse Fotoserie.
                m_fotoserie = serializer.Deserialize<Fotoserie>(responseJSON);
                MessageBox.Show("Fotoserie aangemaakt met ID " + m_fotoserie.Id);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {
                foreach (string fotoFileName in listBoxFotos.Items)
                {
                    // read the foto into memory

                    // first found out how big it is
                    // get the file information form the selected file
                    FileInfo fotoFileInfo = new FileInfo(fotoFileName);
                    long numBytes = fotoFileInfo.Length;

                    FileStream fotoStream = new FileStream(fotoFileName, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fotoStream);

                    // convert the file to a byte array
                    byte[] fotoData = br.ReadBytes((int)numBytes);
                    br.Close();


                    Uri webService = new Uri("http://localhost:2372/api/foto/" + m_fotoserie.Id + "/upload");
                    HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, webService);
                    requestMessage.Headers.ExpectContinue = false;

                    MultipartFormDataContent multiPartContent = new MultipartFormDataContent("----MyGreatBoundary");
                    ByteArrayContent byteArrayContent = new ByteArrayContent( fotoData );
                    byteArrayContent.Headers.Add("Content-Type", "application/octet-stream");
                    multiPartContent.Add(byteArrayContent, Path.GetFileName(fotoFileName), fotoFileName);
                    requestMessage.Content = multiPartContent;

                    HttpClient httpClient = new HttpClient();
                    Task<HttpResponseMessage> httpRequest = httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseContentRead, CancellationToken.None);
                    HttpResponseMessage httpResponse = httpRequest.Result;

                    HttpStatusCode statusCode = httpResponse.StatusCode;
                    HttpContent responseContent = httpResponse.Content;

                    if (responseContent != null)
                    {
                        Task<String> stringContentsTask = responseContent.ReadAsStringAsync();
                        String stringContents = stringContentsTask.Result;
                    }


                    fotoStream.Close();
                    fotoStream.Dispose();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonVerwijderClicked(object sender, EventArgs e)
        {
            int index=0;
            while (index < listBoxFotos.Items.Count)
            {
                if (listBoxFotos.GetSelected(index))
                {
                    listBoxFotos.Items.RemoveAt(index);
                }
                else
                {
                    index++;
                }
            }
        }
    }
}
