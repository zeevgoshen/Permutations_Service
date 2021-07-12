using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Services;
using System.Web.UI;
using System.Xml.Serialization;
using Permutation_Services.Common;

namespace Permutation_Services
{
    [WebService]
    //[XmlInclude(typeof(similar))]
    public class similar : WebService
    {
        private const string V = "http://127.0.0.1:8000/api/v1/similar";
        Task<int> longRunningReadTask;

        Utils utils;

        public similar()
        {
            //this.Url = V;
            //this.Context.AcceptWebSocketRequest
        }
        string log_path = String.Empty;


        public int InitLogging()
        {
            try
            {
                utils = Utils.GetInstance();
                log_path = utils.CreateAndStartLogging();
                //utils = Utils.GetInstance();
                utils.WriteLog(log_path, "INFO", "Logging started.");

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
        public async System.Threading.Tasks.Task CheckWordAsync(string inputWord)
        {
            try
            {
                if (InitLogging().Equals(1))
                {
                    File.WriteAllText(Path.Combine(Environment.CurrentDirectory, Constants.Logs.LOGS_FOLDER, Constants.Logs.LOG_FILENAME), "Log init failed.");
                }

                if (inputWord == string.Empty)
                {
                    // show hidden html validation error
                    return;
                }

                utils.WriteLog(log_path, "INFO", "Main flow started.");


                string values = utils.OpenDBFile();

                if (values == null)
                {
                    return;
                }

                // algorithem part of the search.


                similar sim = new similar();

                HttpContext httpContext = sim.Context;

                httpContext.Request.ContentType = "text/xml";
                httpContext.Request.ContentType = "text/xml; charset=UTF-8";

                /*if (HttpContext.Current.Request.ContentType == "text/xml")
                {
                    HttpContext.Current.Request.ContentType = "text/xml; charset=UTF-8";
                }*/




                utils = Utils.GetInstance();

                //"stressed", "apple"
                List<string> allPermResults = utils.PrintPerms(inputWord);
                List<string> dbResults = utils.OpenDBFileAndReturnList();

                // results is all possible permutations
                // 1) cross with db data:
                // 2) tests
                // 3) ui of index.html
                // 4) async work
                // 5) stats
                // 6) explain the algorithem of permutations.

                string final = string.Empty;


                foreach (string s in allPermResults)
                {
                    // this inserts the actual string too,
                    // needs a fix
                    if (dbResults.Contains(s))
                    {
                        final += s;
                        final += ",";
                    }

                }

                //string result = GetSomething();
                int res = await ReadFile();


            }
            catch (Exception ex)
            {
                File.WriteAllText(log_path, ex.Message);
            }
        }


        public async Task<int> ReadFile()
        {
            try
            {
                //mUtil = utilManager.GetInstance();
                longRunningReadTask = TryRead();//mUtil.TryEncrypt(inFile);

                int result = await longRunningReadTask;

                if (result == 1)
                {
                    //lblStatus.Text = mEncryptDoneMsg;
                }
                return result;
            }
            catch (Exception ex)
            {
                utils.WriteLog(log_path, "ERROR", ex.Message + " " + ex.StackTrace);
                return 0;
            }
        }


        //async Task<int> TryRead()
        async Task<int> TryRead()
        {
            try
            {
                utils.WriteLog(log_path, "INFO", "Opening DB for reading.");
                string db_path = Path.Combine(Environment.CurrentDirectory, Constants.DB.FOLDER_NAME, Constants.DB.TABLE_NAME);

                if (!File.Exists(db_path))
                {
                    return 0;
                }

                // By default, ReadAllLines(*) closes the file after reading
                var wordFile = File.ReadAllLines(db_path);
                var wordList = new List<string>(wordFile);

                SerializeWorldList ser = SerializeWorldList.Create(wordList);

                return 1; //ser.Convert();
            }
            catch (Exception ex)
            {
                utils.WriteLog(log_path, "ERROR", ex.Message + " " + ex.StackTrace);
                return 0;
            }
        }
    }
}
