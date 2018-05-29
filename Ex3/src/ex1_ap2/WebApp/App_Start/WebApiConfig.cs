using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();


            config.Routes.MapHttpRoute(
                name: "DefaultApi4",
                routeTemplate: "api/{controller}/{name}/{rows}/{cols}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi5",
                routeTemplate: "api/{controller}/JoinMaze/{name}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

                        config.Routes.MapHttpRoute(
                name: "DefaultApi6",
                routeTemplate: "api/{controller}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi2",
                routeTemplate: "api/{controller}/{name}/{rows}/{cols}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi3",
                routeTemplate: "api/{controller}/{name}/{alg}",
                defaults: new { id = RouteParameter.Optional }
            );

        }
    }
}
