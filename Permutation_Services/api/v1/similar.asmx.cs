using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
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
        public void Find_Permutations_In_DB(string word)
        {
            try
            {
                utils = Utils.GetInstance();

                if (utils.InitLogging().Equals(1))
                {
                    File.WriteAllText(Path.Combine(Environment.CurrentDirectory, Constants.Logs.LOGS_FOLDER, Constants.Logs.LOG_FILENAME), "Log init failed.");
                }

                if (word == string.Empty)
                {
                    utils.WriteLog(log_path, "DEBUG", "Input word is empty.");
                    return;
                }

                word = WordsResponse(word);
            }
            catch (Exception ex)
            {
                File.WriteAllText(log_path, ex.Message);
                return;
            }
        }

        private string WordsResponse(string word)
        {
            word = word.ToLower();
            utils.WriteLog(log_path, "INFO", "Searching for: " + word);
            //List<string> allPermResults = await Task.Run(() => utils.GetPermutationsWithDuplicatesAsync(inputWord));

            // 1 calc perms
            List<string> allPermResults = utils.GetPermutationsWithDuplicates(word);

            // 2 read db
            List<string> dbResults = utils.OpenDBFileAndReturnList();
            finalWordList = new List<string>();

            // 3 cross results
            var watchPerformSearch = System.Diagnostics.Stopwatch.StartNew();

            Task.WaitAll(IntersectResults(allPermResults, dbResults));

            watchPerformSearch.Stop();

            var elapsedMsPerformSearch = watchPerformSearch.ElapsedMilliseconds;
            utils.WriteLog(log_path, "INFO", "PerformSearch of " + word + " took - [ms]:" + elapsedMsPerformSearch.ToString());

            finalWordList.Remove(word);

            SerializeWorldList ser = SerializeWorldList.Create(finalWordList);
            string jsonString = ser.Convert();
            Context.Response.Write(jsonString);
            return word;
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


        private Task IntersectResults(List<string> allPermResults, List<string> dbResults)
        {
            //Semaphore semaphore = new Semaphore(0,1);
            //semaphore.Close();

            if (dbResults == null)
                return null; //Task.FromException(throw new EmptyDBResultsException);
            IEnumerable<string> newFinalWordList = allPermResults.Select(i => i.ToString()).Intersect(dbResults);
            //Thread.Sleep(2000);
            finalWordList = newFinalWordList.ToList();
            //semaphore.Release();
            return Task.CompletedTask;
        }
    }
}
