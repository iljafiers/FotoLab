using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace FotoWebservice
{
    public static class WebApiConfig
    {
        // http://aspnetwebstack.codeplex.com/wikipage?title=Attribute%20routing%20in%20Web%20API
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            // Foto all
            config.Routes.MapHttpRoute(
                name: "FotoApiAll",
                routeTemplate: "api/fotoserie/{fotoserie_key}/foto/all",
                defaults: new { controller = "Foto", action = "GetAll" }
            );
            // Foto
            config.Routes.MapHttpRoute(
                name: "FotoApi",
                routeTemplate: "api/fotoserie/{fotoserie_key}/foto/{id}",
                defaults: new { controller = "Foto", id = RouteParameter.Optional }
            );

            // Fotoserie
            config.Routes.MapHttpRoute(
                name: "FotoserieApi",
                routeTemplate: "api/Fotoserie/{fotoserie_id}",
                defaults: new { controller = "Fotoserie", fotoserie_id = RouteParameter.Optional }
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
