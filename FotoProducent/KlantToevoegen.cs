using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FotoProducent
{
    public partial class KlantToevoegen : Form
    {
        public Klant m_klant;

        public KlantToevoegen()
        {
            InitializeComponent();
        }

        private void UIToData()
        {
            m_klant.Naam = textBoxNaam.Text;
            m_klant.Klant_key = textBoxKlant_Key.Text;
            m_klant.Straat = textBoxStraat.Text;
            m_klant.Huisnummer = textBoxHuisNr.Text;
            m_klant.Postcode = textBoxPostCode.Text;
            m_klant.Woonplaats = textBoxWoonplaats.Text;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            UIToData();
        }

        private void buttonAnnuleer_Click(object sender, EventArgs e)
        {
            UIToData();
            this.DialogResult = Dialo
        }
    }
}
