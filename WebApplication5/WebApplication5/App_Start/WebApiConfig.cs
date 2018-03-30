using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace newsapi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
         
            config.MapHttpAttributeRoutes();

        
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );



            config.Routes.MapHttpRoute(
                name: "ActionApi",
                routeTemplate: "actionapi/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            
            config.Routes.MapHttpRoute(
                name: "TestApi",
                routeTemplate: "testapi/{controller}/{ordertype}/{id}",
                defaults: new { ordertype = "aa", id = RouteParameter.Optional }
            );
        }
    }
}
