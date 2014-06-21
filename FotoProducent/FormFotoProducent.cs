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

            int id = Convert.ToInt32(this.textBoxKlantID.Text);

            string url = "http://localhost:2372/api/Klant/" + id;

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

        }

        private void OnClickUpload(object sender, EventArgs e)
        {

        }
    }
}
