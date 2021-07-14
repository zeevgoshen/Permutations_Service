using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Xml.Serialization;
using Permutation_Services.Common;

namespace Permutation_Services
{
    [WebService(Namespace = "http://tempuri.org/")]
    public class similar : WebService
    {
        Utils           utils;
        List<string>    finalWordList;
        string          log_path = Path.Combine(Environment.CurrentDirectory, Constants.Logs.LOGS_FOLDER, Constants.Logs.LOG_FILENAME);

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void CheckWordAsync(string inputWord)
        {
            try
            {
                utils = Utils.GetInstance();

                if (utils.InitLogging().Equals(1))
                {
                    File.WriteAllText(Path.Combine(Environment.CurrentDirectory, Constants.Logs.LOGS_FOLDER, Constants.Logs.LOG_FILENAME), "Log init failed.");
                }

                if (inputWord == string.Empty)
                {
                    utils.WriteLog(log_path, "DEBUG", "Input word is empty.");
                    return;
                }

                inputWord = inputWord.ToLower();
                utils.WriteLog(log_path, "INFO", "Searching for: " + inputWord);
                //List<string> allPermResults = await Task.Run(() => utils.GetPermutationsWithDuplicatesAsync(inputWord));

                // 1 calc perms
                List<string> allPermResults = utils.GetPermutationsWithDuplicates(inputWord);

                // 2 read db
                List<string> dbResults = utils.OpenDBFileAndReturnList();
                finalWordList = new List<string>();

                // 3 cross results
                var watchPerformSearch = System.Diagnostics.Stopwatch.StartNew();

                Task.WaitAll(PerformSearch(allPermResults, dbResults));

                //PerformSearchSync(allPermResults, dbResults);

                watchPerformSearch.Stop();

                var elapsedMsPerformSearch = watchPerformSearch.ElapsedMilliseconds;
                utils.WriteLog(log_path, "INFO", "PerformSearch of " + inputWord + " took - [ms]:" + elapsedMsPerformSearch.ToString());

                finalWordList.Remove(inputWord);

                SerializeWorldList ser = SerializeWorldList.Create(finalWordList);
                string jsonString = ser.Convert();
                Context.Response.Write(jsonString);

            }
            catch (Exception ex)
            {
                File.WriteAllText(log_path, ex.Message);
                return;
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
