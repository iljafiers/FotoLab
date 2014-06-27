using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FotoWebservice.Controllers;
using FotoWebservice.Models;

namespace FotoWebservice.Tests
{
    // http://www.asp.net/web-api/overview/testing-and-debugging/unit-testing-with-aspnet-web-api
    // http://www.asp.net/web-api/overview/testing-and-debugging/unit-testing-controllers-in-web-api

    // http://www.strathweb.com/2012/08/testing-routes-in-asp-net-web-api/
    [TestClass]
    public class TestKlantController
    {
        [TestMethod]
        public void GetWithString_ShouldReturnKlant()
        {
            List<Klant> klanten = GetTestKlanten();
            KlantController controller = new KlantController(new MockKlantRepository(klanten));

            Klant resultKlant = controller.Get("henkieskey");

            Assert.AreEqual(resultKlant.Id, 1);
        }

        [TestMethod]
        public void GetWithId_ShouldReturnKlant()
        {
            List<Klant> klanten = GetTestKlanten();
            KlantController controller = new KlantController(new MockKlantRepository(klanten));

            Klant resultKlant = controller.Get(3);

            Assert.AreEqual(resultKlant.Klant_key, "klaasjeskey");
        }

        [TestMethod]
        public void GetWithWrongString_ShouldReturnNull()
        {
            List<Klant> klanten = GetTestKlanten();
            KlantController controller = new KlantController(new MockKlantRepository(klanten));

            Klant resultKlant = controller.Get("Dezeklantkeybestaatniet");

            Assert.AreEqual(resultKlant, null);
        }

        [TestMethod]
        public void GetWithWrongId_ShouldReturnNull()
        {
            List<Klant> klanten = GetTestKlanten();
            KlantController controller = new KlantController(new MockKlantRepository(klanten));

            Klant resultKlant = controller.Get(9999);

            Assert.AreEqual(resultKlant, null);
        }

        private List<Klant> GetTestKlanten()
        {
            return new List<Klant>
            {
                new Klant{Id = 1, Naam="Henkie", Klant_key="henkieskey", Straat="henkiesstraat", Huisnummer="25A", Postcode="5555 ZZ", Woonplaats="Sexbierum"},
                new Klant{Id = 2, Naam="Keesje", Klant_key="@ll3m##l_RARE_t*k&ns", Straat="keesjesstraat", Huisnummer="2", Postcode="8888 TT", Woonplaats="Groningen"},
                new Klant{Id = 3, Naam="Klaasje", Klant_key="klaasjeskey", Straat="klaasjesstraat", Huisnummer="999", Postcode="7777 SS", Woonplaats="Biervliet"},
                new Klant{Id = 4, Naam="Janus", Klant_key="janussen", Straat="Januskesstraat", Huisnummer="13", Postcode="1313 VV", Woonplaats="Blankenberge"}
            };
        }
    }
}
