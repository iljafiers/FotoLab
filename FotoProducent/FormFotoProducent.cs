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
