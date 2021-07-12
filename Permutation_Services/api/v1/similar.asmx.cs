using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        List<string> finalWordList;

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
        //public async Task CheckWordAsync(string inputWord)
        public string CheckWordAsync(string inputWord)
        {
            try
            {
                if (InitLogging().Equals(1))
                {
                    File.WriteAllText(Path.Combine(Environment.CurrentDirectory, Constants.Logs.LOGS_FOLDER, Constants.Logs.LOG_FILENAME), "Log init failed.");
                }

                if (inputWord == string.Empty)
                {
                    return Constants.UserInput.EMPTY_INPUTWORD;
                }

                inputWord = inputWord.ToLower();

                utils.WriteLog(log_path, "INFO", "Searching for: + " + inputWord);


                string values = utils.OpenDBFile();

                if (values == null)
                {
                    return Constants.DB.DB_NO_RESULTS;
                }

                // algorithem part of the search.


                //similar sim = new similar();

                //HttpContext httpContext = sim.Context;

                //httpContext.Request.ContentType = "text/xml";
                //httpContext.Request.ContentType = "text/xml; charset=UTF-8";

                /*if (HttpContext.Current.Request.ContentType == "text/xml")
                {
                    HttpContext.Current.Request.ContentType = "text/xml; charset=UTF-8";
                }*/



                // results is all possible permutations
                // 1) cross with db data:
                // 2) tests
                // 3) ui of index.html
                // 4) async work
                // 5) stats - count specific files in log
                // 6) explain the algorithem of permutations.

                utils = Utils.GetInstance();

                //"stressed", "apple"
                List<string> allPermResults = utils.PrintPerms(inputWord);
                List<string> dbResults = utils.OpenDBFileAndReturnList();
                finalWordList = new List<string>();


                Task.WaitAll(PerformSearch(allPermResults, dbResults));

                //await PerformSearch(allPermResults, dbResults);
                //await PerformSearch(allPermResults, dbResults);
                //Task t = await PerformSearch(allPermResults, dbResults);

                /*foreach (string s in allPermResults)
                {
                    if (dbResults.Contains(s))
                    {
                        finalWordList.Add(s);

                    }
                }*/

                SerializeWorldList ser = SerializeWorldList.Create(finalWordList);
                string jsonString = ser.Convert();
                return jsonString;

            }
            catch (Exception ex)
            {
                File.WriteAllText(log_path, ex.Message);
                return "";
            }
        }

        //private async Task PerformSearch(List<string> allPermResults, List<string> dbResults)

        private Task PerformSearch(List<string> allPermResults, List<string> dbResults)
        {
            finalWordList = new List<string>();


            /*if (dbResults.Contains(x => dbResults.Where = x))
            {

            }*/
            foreach (string s in allPermResults)
            {
                if (dbResults.Contains(s))
                {
                    finalWordList.Add(s);

                }
            };

            return Task.CompletedTask;


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