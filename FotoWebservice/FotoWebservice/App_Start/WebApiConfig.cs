using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace FotoWebservice
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            // Fotoserie
            config.Routes.MapHttpRoute(
                name: "FotoserieApi",
                routeTemplate: "api/Fotoserie/{fotoserie_id}",
                defaults: new { controller = "Fotoserie", fotoserie_id = RouteParameter.Optional }
            );
            // Foto
            config.Routes.MapHttpRoute(
                name: "FotoApi",
                routeTemplate: "api/fotoserie/{fotoserie_key}/foto/{foto_id}",
                defaults: new { controller = "Foto", foto_id = RouteParameter.Optional }
            );

            // DefaultApi
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
