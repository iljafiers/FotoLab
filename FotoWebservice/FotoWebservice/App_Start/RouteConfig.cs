using FotoWebservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FotoWebservice
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            /*PayPalBetaalMethode pm = new PayPalBetaalMethode();

            pm.PostPayment("http://www.startpagina.nl", "http://www.google.nl", 12.49m);*/
        }
    }
}
