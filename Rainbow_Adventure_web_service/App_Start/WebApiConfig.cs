using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using static Rainbow_Adventure_web_service.Controllers.RainbowController;

namespace Rainbow_Adventure_web_service
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes

          //  var ef = new MyExceptionFilterAttribute();
           // config.Filters.Add(ef);

                config.MapHttpAttributeRoutes();
                config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
