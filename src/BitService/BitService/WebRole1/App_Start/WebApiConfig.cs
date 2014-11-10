using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using WebRole1.Filters;

namespace WebRole1
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "NamedBit",
                routeTemplate: "api/{controller}/{name}",
                defaults: new { name = RouteParameter.Optional } );

            GlobalConfiguration.Configuration.Filters.Add(new ValidateHttpAntiForgeryTokenAttribute());

        }
    }
}
