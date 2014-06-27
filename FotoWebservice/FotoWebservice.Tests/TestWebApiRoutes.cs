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
    // https://github.com/filipw/webapi.inmemory/tree/master/webapi.tests

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
        public void GetAllFotoseriesTest()
        {
            this.config.ShouldMap("/api/fotoserie")
                .To<FotoserieController>(HttpMethod.Get, x => x.GetAll());
        }

        [TestMethod]
        public void FindFotoseriesForKlantTest()
        {
            this.config.ShouldMap("/api/klant/DitIsEenKlantKey/fotoserie")
                .To<FotoserieController>(HttpMethod.Get, x => x.FindAllForKlant("DitIsEenKlantKey"));
        }

        [TestMethod]
        public void GetFotoserieTest()
        {
            this.config.ShouldMap("/api/klant/fotoserie/2")
                .To<FotoserieController>(HttpMethod.Get, x => x.Get(2));
        }

        [TestMethod]
        public void PostFotoserieTest()
        {
            this.config.ShouldMap("/api/fotoserie/add")
                .To<FotoserieController>(HttpMethod.Post, x => x.Post(new Fotoserie()));
        }

        [TestMethod]
        public void PutFotoserieTest()
        {
            this.config.ShouldMap("/api/Fotoserie/5")
                .To<FotoserieController>(HttpMethod.Put, x => x.Put(5, new Fotoserie()));
        }

        [TestMethod]
        public void DeleteFotoserieTest()
        {
            this.config.ShouldMap("/api/fotoserie/2")
                .To<FotoserieController>(HttpMethod.Delete, x => x.Delete(2));
        }
    }

    [TestClass]
    public class TestFotoRoutes : TestWebApiRoutes
    {
        [TestMethod]
        public void GetAllFotosTest()
        {
            this.config.ShouldMap("/api/fotoserie/DitIsEenFotoserieKey/foto/all")
                .To<FotoController>(HttpMethod.Get, x => x.GetAll("DitIsEenFotoserieKey"));
        }

        [TestMethod]
        public void GetFotoTest()
        {
            this.config.ShouldMap("/api/fotoserie/DitIsEenFotoserieKey/foto/22")
                .To<FotoController>(HttpMethod.Get, x => x.Get("DitIsEenFotoserieKey", 22));
        }

        [TestMethod]
        public void PostFotosWithStringTest()
        {
            this.config.ShouldMap("/api/fotoserie/DitIsEenFotoserieKey/foto")
                .To<FotoController>(HttpMethod.Post, x => x.UploadPhoto("DitIsEenFotoserieKey"));
        }

        [TestMethod]
        public void PostFotosWithIdTest()
        {
            this.config.ShouldMap("/api/foto/2/upload")
                .To<FotoController>(HttpMethod.Post, x => x.UploadPhoto(2));
        }

        [TestMethod]
        public void DeleteFotoTest()
        {
            this.config.ShouldMap("/api/fotoserie/DitIsEenFotoserieKey/foto/22")
                .To<FotoController>(HttpMethod.Delete, x => x.Delete("DitIsEenFotoserieKey", 22));
        }
    }
}
