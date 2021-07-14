using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Services;
using System.Web.Services.Protocols;
using Permutation_Services.Common;
using System.Web.Script.Serialization;
using System.Web.Script.Services;

namespace Permutation_Services.api.v1
{
    [WebService(Namespace = "http://tempuri.org/")]
    public class stats : WebService
    {
        Utils utils;
        string log_path = Path.Combine(Environment.CurrentDirectory, Constants.Logs.LOGS_FOLDER, Constants.Logs.LOG_FILENAME);
        string db_path = Path.Combine(Environment.CurrentDirectory, Constants.DB.FOLDER_NAME, Constants.DB.TABLE_NAME);

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ShowStats()
        {
            try
            {
                utils = Utils.GetInstance();

                if (utils.InitLogging().Equals(1))
                {
                    File.WriteAllText(Path.Combine(Environment.CurrentDirectory, Constants.Logs.LOGS_FOLDER, Constants.Logs.LOG_FILENAME), "Log init failed.");
                }

                if (!File.Exists(log_path) || !File.Exists(db_path))
                {
                    File.WriteAllText(log_path, "log file or DB were not found.");
                    Context.Response.Write("log file or DB were not found. Perform at least 1 search before viewing statistics.");
                    return;
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

                Context.Response.Write(jsonString);

            }
            catch (Exception ex)
            {
                File.WriteAllText(log_path, "log file or DB were not found. " + ex.Message + " - " + ex.StackTrace);
                return;
            }

        }
    }
}
