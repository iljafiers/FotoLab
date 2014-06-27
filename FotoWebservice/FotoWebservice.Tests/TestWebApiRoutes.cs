using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using MvcRouteTester;
using FotoWebservice.Controllers;
using FotoWebservice.Models;
using System.Web.Http;
using System.Web.Mvc;
using System.Web;
using System.Net.Http;

namespace FotoWebservice.Tests
{
    [TestClass]
    public abstract class TestWebApiRoutes
    {
        protected HttpConfiguration config;

        [TestInitialize]
        public void MakeRouteTable()
        {
            config = new HttpConfiguration();
            WebApiConfig.Register(config);
            config.EnsureInitialized();
        }
    }

    [TestClass]
    public class TestKlantRoutes : TestWebApiRoutes
    {
        [TestMethod]
        public void GetKlantWithIdTest()
        {
            this.config.ShouldMap("/api/klant/2")
                .To<KlantController>(HttpMethod.Get, x => x.Get(2));
        }

        [TestMethod]
        public void GetKlantWithStringTest()
        {
            this.config.ShouldMap("/api/klant/if")
                .To<KlantController>(HttpMethod.Get, x => x.Get("if"));
        }
    }

    [TestClass]
    public class TestFotoserieRoutes : TestWebApiRoutes
    {
        [TestMethod]
        public void GetIdTest()
        {
            this.config.ShouldMap("/api/fotoserie/2")
                .To<KlantController>(HttpMethod.Get, x => x.Get(2));
        }

        [TestMethod]
        public void GetStringTest()
        {
            this.config.ShouldMap("/api/fotoserie/if")
                .To<KlantController>(HttpMethod.Get, x => x.Get("if"));
        }
    }

    [TestClass]
    public class TestFotoRoutes : TestWebApiRoutes
    {
        [TestMethod]
        public void GetAllFotosTest()
        {
            this.config.ShouldMap("api/fotoserie/DitIsEenFotoserieKey/foto/")
                .To<FotoController>(HttpMethod.Get, x => x.GetAll("DitIsEenFotoserieKey"));
        }

        [TestMethod]
        public void GetFotoTest()
        {
            this.config.ShouldMap("api/fotoserie/DitIsEenFotoserieKey/foto/22")
                .To<KlantController>(HttpMethod.Get, x => x.Get("DitIsEenFotoserieKey", 22));
        }

        [TestMethod]
        public void PostFotosTest()
        {
            this.config.ShouldMap("api/foto/2/upload")
                .To<KlantController>(HttpMethod.Post, x => x.UploadPhoto(2));
        }

        [TestMethod]
        public void DeleteFotoTest()
        {
            this.config.ShouldMap("api/fotoserie/DitIsEenFotoserieKey/foto/22")
                .To<KlantController>(HttpMethod.Delete, x => x.Delete("DitIsEenFotoserieKey", 22));
        }
    }
}
