using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Web;
using System.Web.Script.Serialization;

namespace FotoProducent
{
    public partial class FormFotoProducent : Form
    {
        public FormFotoProducent()
        {
            InitializeComponent();
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
                Klant kl = serializer.Deserialize<Klant>(JSON);

                if (kl != null)
                {
                    this.KlantNaam.Text = kl.Naam;
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

                Fotoserie fs = new Fotoserie();
                fs.Naam = textBoxFotoSerieNaam.Text;
                fs.KlantId = 0;
                fs.FotoproducentId = 0;
                fs.Datum = DateTime.Now;

                // Maak JSON text van de fotoserie
                var serializer = new JavaScriptSerializer();
                string JSON = serializer.Serialize(fs);

                // stuur deze naar de webapi, we krijgen een JSON string
                // terug die de volledige Fotoserie wergeeft.
                // Dat is nodig om de Id te kunnen bepalen, we gaan er zo direct fotos
                // aan hangen.
                string url = "http://localhost:2372/api/Fotoserie/add";
                var cli = new WebClient();
                cli.Headers[HttpRequestHeader.ContentType] = "application/json";
                string responseJSON = cli.UploadString(url, JSON);

                // response JSON vertalen naar een instantie van Klasse Fotoserie.
                Fotoserie rspFS = serializer.Deserialize<Fotoserie>(responseJSON);
                MessageBox.Show("Fotoserie aangemaakt met ID " + rspFS.Id);
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

                    // fotodata is the bytes in memory, now upload.

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
