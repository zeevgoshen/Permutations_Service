using System.Web;
using System.Web.Http;

namespace test_aspnet_webforms
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            similar se = new similar();

            //se.CheckWordAsync("hh");



            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        protected void Application_End()
        {
            //GlobalConfiguration.Configure(WebApiConfig.Register);
        }

    }
}
