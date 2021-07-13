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
    [WebService(Namespace =  "http://tempuri.org/")]

    //[XmlInclude(typeof(similar))]
    public class similar : WebService
    {
        private const string V = "http://127.0.0.1:8000/api/v1/similar";


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
                utils.WriteLog(log_path, "INFO", "Searching for: " + inputWord);

                string values = utils.OpenDBFile();

                if (values == null)
                {
                    return Constants.DB.DB_NO_RESULTS;
                }

                // algorithem part of the search.



                // results is all possible permutations
                // 1) cross with db data:
                // 2) tests
                // 3) ui of index.html
                // 4) async work
                // 5) stats - count specific files in log
                // 6) explain the algorithem of permutations.

                utils = Utils.GetInstance();

                // try: "stressed", "apple"
                //List<string> allPermResults = await Task.Run(() => utils.GetPermutationsWithDuplicatesAsync(inputWord));

                // 1 calc perms
                List<string> allPermResults = utils.GetPermutationsWithDuplicates(inputWord);

                // 2 read db
                List<string> dbResults = utils.OpenDBFileAndReturnList();
                finalWordList = new List<string>();

                // 3 cross results
                var watchPerformSearch = System.Diagnostics.Stopwatch.StartNew();

                //Task.WaitAll(PerformSearch(allPermResults, dbResults));

                PerformSearchSync(allPermResults, dbResults);
                watchPerformSearch.Stop();
                var elapsedMsPerformSearch = watchPerformSearch.ElapsedMilliseconds;
                utils.WriteLog(log_path, "INFO", "PerformSearch of " + inputWord + " took - [ms]:" + elapsedMsPerformSearch.ToString());

                finalWordList.Remove(inputWord);
                 

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

        private void PerformSearchSync(List<string> allPermResults, List<string> dbResults)
        {
            finalWordList = new List<string>();

            foreach (string s in allPermResults)
            {
                if (dbResults.Contains(s))
                {
                    finalWordList.Add(s);

                }
            };
        }


        private Task PerformSearch(List<string> allPermResults, List<string> dbResults)
        {
            finalWordList = new List<string>();

            foreach (string s in allPermResults)
            {
                if (dbResults.Contains(s))
                {
                    finalWordList.Add(s);

                }
            };

            return Task.CompletedTask;
        }
    }
}
