using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace FotoWebservice
{
    public static class WebApiConfig
    {
        // http://aspnetwebstack.codeplex.com/wikipage?title=Attribute%20routing%20in%20Web%20API
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // CORS Request doorlaten (Ajax-requests van andere websites)
            // http://www.asp.net/web-api/overview/security/enabling-cross-origin-requests-in-web-api#create-webapi-project
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors();

            // Web API routes
            config.MapHttpAttributeRoutes();


            // Foto all
            config.Routes.MapHttpRoute(
                name: "FotoApiAll",
                routeTemplate: "api/fotoserie/{fotoserie_key}/foto/all",
                defaults: new { controller = "Foto", action = "GetAll" }
            );

            // Foto Get
            config.Routes.MapHttpRoute(
                name: "FotoApiGet",
                routeTemplate: "api/fotoserie/{fotoserie_key}/foto/{id}",
                defaults: new { controller = "Foto", action = "Get" }
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

            /* Opmerking Pieter 18-06-2014: 
             * Ik weet denk ik al waar het fout ging in de attribute routing. Ik gebruikte een variabele met een underscore. Dat mag waarschijnlijk niet.
             */
        }
    }
}
