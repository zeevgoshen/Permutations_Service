using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Services;
using System.Web.Services.Protocols;
using Permutation_Services.Common;

namespace Permutation_Services.api.v1
{
    [WebService]
    public class stats : WebService
    {
        Utils utils;

        [WebMethod]
        [HttpGet]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string countWordsInDictionary()
        {
            try
            {

                utils = Utils.GetInstance();


                //utils.OpenDBFile(); // <- needs refactoring to not return json

                string log_path = Path.Combine(Environment.CurrentDirectory, Constants.Logs.LOGS_FOLDER, Constants.Logs.LOG_FILENAME);
                string db_path = Path.Combine(Environment.CurrentDirectory, Constants.DB.FOLDER_NAME, Constants.DB.TABLE_NAME);

                if (!File.Exists(log_path) || !File.Exists(db_path))
                {
                    return null;//"No log file found.";
                }

                var wordFile = File.ReadAllLines(db_path);
                List<string> wordList = new List<string>(wordFile);



                // Reading the log file
                // By default, ReadAllLines(*) closes the file after reading
                var logFile = File.ReadAllLines(log_path);
                List<string> logList = new List<string>(logFile);

                // info from logs
                IEnumerable<string> items = logList;
                int count = items.Count(x => x.Contains("Searching for"));



                int numberOfRequests = items.Count(x => x.Contains("PerformSearch of"));

                IEnumerable<string> uitems = items.Where(x => x.Contains("PerformSearch of"));

                StatsAverageRequest averageRequest = new StatsAverageRequest(uitems);

                SerializeStats ser = SerializeStats.Create(wordList.Count, count, averageRequest.ParseString());
                string jsonString = ser.Convert();
                return jsonString;
            }
            catch (Exception ex)
            {
                return "";
            }

        }
    }
}
