using System.Web.Http;

namespace Permutation_services
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();


            //config.Routes.MapHttpRoute(name: "similar", routeTemplate: "api/v1/");

            config.Routes.MapHttpRoute(name: "DefaultApi",routeTemplate: "api/{controller}/{id}",defaults: new { id = RouteParameter.Optional }); 

        }
    }
}
