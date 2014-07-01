using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace KlantenWebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PaypalEnd()
        {
            return View();
        }
    }
}
