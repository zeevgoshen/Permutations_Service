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
    [WebService(Namespace = "http://localhost:8000/api/v1/similar.asmx/Show_Stats")]
    public class stats : WebService
    {
        Utils utils;
        string log_path = Path.Combine(Environment.CurrentDirectory, Constants.Logs.LOGS_FOLDER, Constants.Logs.LOG_FILENAME);
        string db_path = Path.Combine(Environment.CurrentDirectory, Constants.DB.FOLDER_NAME, Constants.DB.TABLE_NAME);

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void Show_Stats()
        {
            try
            {
                utils = Utils.GetInstance();
                var                 wordFile = File.ReadAllLines(db_path);
                List<string>        wordList = new List<string>(wordFile);
                var                 logFile = File.ReadAllLines(log_path);
                List<string>        logList = new List<string>(logFile);
                IEnumerable<string> items = logList;
                int                 count = 0;
                int                 numberOfRequests = 0;
                IEnumerable<string> uitems;
                StatsAverageRequest averageRequest;
                string              jsonString = string.Empty;

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

                if (items.Any())
                {
                    count = items.Count(x => x.Contains("Searching for"));
                    numberOfRequests = items.Count(x => x.Contains("PerformSearch of"));
                    uitems = items.Where(x => x.Contains("PerformSearch of"));
                    averageRequest = new StatsAverageRequest(uitems);
                    SerializeStats ser = SerializeStats.Create(wordList.Count, count, averageRequest.ParseString());
                    jsonString = ser.Convert();
                }
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
