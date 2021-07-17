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
    [WebService(Namespace = "http://localhost:8000/api/v1/similar.asmx/Find_Permutations_In_DB?word=")]
    public class similar : WebService
    {
        Utils               utils;
        List<string>        finalWordList;
        string              log_path = Path.Combine(Environment.CurrentDirectory, Constants.Logs.LOGS_FOLDER, Constants.Logs.LOG_FILENAME);
        public string       mJsonString = String.Empty;

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

                // Should not lock Task.WaitAll(WordsResponse(word));
                // since it will delay shorter searches
                // if a long search is already undergoing.
                // Very long searches are not immune to request timeouts.
                Task.WaitAll(WordsResponse(word));

            }
            catch (Exception ex)
            {
                File.WriteAllText(log_path, ex.Message);
                if (Context != null)
                {
                    Context.Response.Write(ex.Message);       
                }
                return;
            }
        }

        private Task WordsResponse(string word)
        {
            try
            {
                word = word.ToLower();
                utils.WriteLog(log_path, "INFO", "Searching for: " + word);

                var watchPerformSearch = System.Diagnostics.Stopwatch.StartNew();

                Task.WaitAll(utils.GetPermutationsWithDuplicates(word));

                watchPerformSearch.Stop();

                if (utils.mResults == null)
                {
                    Context.Response.Write("Search failed.");
                    return Task.CompletedTask;
                }

                finalWordList = utils.mResults;

                var elapsedNsPerformSearch = watchPerformSearch.Elapsed.TotalMilliseconds * 1000000;

                utils.WriteLog(log_path, "INFO", "PerformSearch of " + word + " took - [ns]:" + elapsedNsPerformSearch.ToString());

                finalWordList.Remove(word);

                SerializeWorldList ser = SerializeWorldList.Create(finalWordList);
                mJsonString = ser.Convert();

                if (!Environment.CurrentDirectory.Contains(Constants.ENVIRONMENT.TEST))
                {
                    Context.Response.Write(mJsonString);
                }

                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                utils.WriteLog(log_path, "ERROR", ex.Message + " " + ex.StackTrace);
                if (utils.mResults == null)
                {
                    if (Context != null)
                    {
                        Context.Response.Write("Search failed.");
                    }
                    return Task.CompletedTask;
                }
                else
                {
                    if (Context != null)
                    {
                        Context.Response.Write("Thread was being aborted, Timeout.");
                    }

                    return Task.CompletedTask;
                }
            }
        }

        /*private Task IntersectResults(List<string> allPermResults, List<string> dbResults)
        {
            if (dbResults == null)
                return null;
            IEnumerable<string> newFinalWordList = allPermResults.Select(i => i.ToString()).Intersect(dbResults);
            finalWordList = newFinalWordList.ToList();
            return Task.CompletedTask;
        }*/
    }
}
