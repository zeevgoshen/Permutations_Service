using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Services;
using System.Web.UI;
using test_aspnet_webforms.Common;

namespace test_aspnet_webforms
{
    [WebService]
    public class similar : System.Web.Services.WebService
    {
        private const string V = "http://127.0.0.1:8000/api/v1/similar";

        public similar()
        {
            //this.Url = V;
            //this.Context.AcceptWebSocketRequest
        }
        string log_path = String.Empty;


        public int initLogging()
        {
            try
            {
                log_path = Utils.createAndStartLogging();
                Utils.writeLog(log_path, "INFO", "Logging started.");

                return 0;
            }
            catch (Exception ex)
            {
                File.WriteAllText(Path.Combine(Environment.CurrentDirectory, Constants.Logs.LOG_FILENAME), ex.StackTrace + " " + ex.Message);
                return 1;
            }
        }

        [WebMethod]
        [Route("/api/v1/similar")]
        [HttpGet]
        public void CheckWord(string inputWord)
        {
            try
            {
                if (initLogging().Equals(1))
                {
                    File.WriteAllText(Path.Combine(Environment.CurrentDirectory, Constants.Logs.LOGS_FOLDER, Constants.Logs.LOG_FILENAME), "Log init failed.");
                }

                Utils.writeLog(log_path, "INFO", "Main flow started.");

                string values = Utils.OpenDBFile();

                if (values == null)
                {
                    return;
                }


                // routing to the exact address


                // algorithem part of the search.


                similar sim = new similar();

                HttpContext httpContext = sim.Context;

                httpContext.Request.ContentType = "text/xml";
                httpContext.Request.ContentType = "text/xml; charset=UTF-8";

                if (HttpContext.Current.Request.ContentType == "text/xml")
                {
                    HttpContext.Current.Request.ContentType = "text/xml; charset=UTF-8";
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText(log_path, ex.Message);
            }
        }
    }
}
