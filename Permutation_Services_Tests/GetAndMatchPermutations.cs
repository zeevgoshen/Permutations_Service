using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Permutation_Services;
using Permutation_Services.Common;

namespace Permutation_Services_Tests
{
    public class GetAndMatchPermutations
    {
        Utils utils;
        List<string> finalWordList;
        public string mJsonString = String.Empty;
        public List<string> mResults;
        private static readonly object syncLock = new object();

        public GetAndMatchPermutations()
        {
        }

        public List<string> OpenDBFileAndReturnList()
        {
            try
            {
                //mInstance.WriteLog(log_path, "INFO", "Opening DB for reading.");
                string db_path = Path.Combine(Environment.CurrentDirectory, Constants.DB.FOLDER_NAME, Constants.DB.TABLE_NAME);

                if (Environment.CurrentDirectory.Contains(Constants.ENVIRONMENT.TEST))
                {
                    DirectoryInfo d = Directory.GetParent(Environment.CurrentDirectory).Parent;
                    db_path = Path.Combine(d.ToString(), Constants.DB.FOLDER_NAME, Constants.DB.TABLE_NAME);
                }

                if (!File.Exists(db_path))
                {
                    return null;
                }

                // By default, ReadAllLines(*) closes the file after reading
                var wordFile = File.ReadAllLines(db_path);
                var wordList = new List<string>(wordFile);

                return wordList;
            }
            catch (Exception ex)
            {
                //mInstance.WriteLog(log_path, "ERROR", ex.Message + " " + ex.StackTrace);
                return null;
            }
        }

        private Task WordsResponse(string word)
        {
            try
            {
                word = word.ToLower();

                //List<string> allPermResults = await Task.Run(() => utils.GetPermutationsWithDuplicatesAsync(inputWord));

                // 1 calc perms
                Task.WaitAll(GetPermutationsWithDuplicates(word));

                if (mResults == null)
                {
                    //Context.Response.Write("Search failed.");
                    return Task.CompletedTask;
                }

                List<string> allPermResults = mResults;

                // 2 read db
                List<string> dbResults = OpenDBFileAndReturnList();
                finalWordList = new List<string>();

                // 3 cross results
                var watchPerformSearch = System.Diagnostics.Stopwatch.StartNew();

                Task.WaitAll(IntersectResults(allPermResults, dbResults));

                watchPerformSearch.Stop();

                var elapsedMsPerformSearch = watchPerformSearch.ElapsedMilliseconds;
                //utils.WriteLog(log_path, "INFO", "PerformSearch of " + word + " took - [ms]:" + elapsedMsPerformSearch.ToString());

                finalWordList.Remove(word);

                SerializeWorldList ser = SerializeWorldList.Create(finalWordList);
                mJsonString = ser.Convert();

                if (!Environment.CurrentDirectory.Contains(Constants.ENVIRONMENT.TEST))
                {
                    //Context.Response.Write(mJsonString);
                }

                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                //utils.WriteLog(log_path, "ERROR", ex.Message + " " + ex.StackTrace);
                if (utils.mResults == null)
                {
                    //if (Context != null)
                    //{
                    //Context.Response.Write("Search failed.");
                    //}
                    return Task.CompletedTask;
                }
                else
                {
                    //if (Context != null)
                    //{
                    //    Context.Response.Write("Thread was being aborted, Timeout.");
                    //}

                    return Task.CompletedTask;
                }
            }

        }

        private Task IntersectResults(List<string> allPermResults, List<string> dbResults)
        {
            if (dbResults == null)
                return null;
            IEnumerable<string> newFinalWordList = allPermResults.Select(i => i.ToString()).Intersect(dbResults);
            finalWordList = newFinalWordList.ToList();
            return Task.CompletedTask;
        }

        public void Find_Permutations_In_DB_Test(string word)
        {
            try
            {
                utils = Utils.GetInstance();


                if (word == string.Empty)
                {
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


                return;
            }
        }

        public Task GetPermutationsWithDuplicates(string s)
        {
            try
            {
                lock (syncLock)
                {
                    //mResponse = response;
                    mResults = new List<string>();
                    Dictionary<char, int> map = BuildFreqTable(s);
                    GetPermutationsWithDuplicates(map, "", s.Length, mResults);
                    return Task.CompletedTask;
                }

            }
            catch (Exception ex)
            {
                //mInstance.WriteLog(log_path, "ERROR", ex.Message + " " + ex.StackTrace);
                //response.Write("Query too big.");
                return null;
            }
        }

        public Dictionary<char, int> BuildFreqTable(string s)
        {
            Dictionary<char, int> map = new Dictionary<char, int>();
            foreach (char c in s.ToCharArray())
            {
                if (!map.ContainsKey(c))
                {
                    map.Add(c, 0);
                }
                map[c]++; //(c, map[c] + 1); // [c] instead of .get(c) which gets value by key
            }
            return map;
        }

        void GetPermutationsWithDuplicates(Dictionary<char, int> map, String prefix, int remaining, List<string> result)
        {
            try
            {
                /* Base case. Permutation has been completed. */
                if (remaining == 0)
                {
                    result.Add(prefix);
                    return;
                }

                foreach (char c in new List<char>(map.Keys)) // or map.Keys
                {
                    //mResponse.Write("Working on - " + prefix); //("");
                    int count = map[c]; // [c] instead of .get(c) which gets value by key

                    if (count > 0)
                    {
                        map[c]--;
                        GetPermutationsWithDuplicates(map, prefix + c, remaining - 1, result);
                        map[c] = count;
                    }
                }
            }
            catch (Exception ex)
            {
                //HttpContext.Current.Response.Write("Thread was killed by timeout.");
                //mInstance.WriteLog(log_path, "ERROR", ex.Message + " " + ex.StackTrace);
            }
        }

        public string new_algo(string word)
        {
            var input = new string(word.ToLower().OrderBy(c => c).ToArray());
            //string input = word.ToLower().OrderBy(c => c).ToArray();

            string db_path = Path.Combine(Environment.CurrentDirectory, Constants.DB.FOLDER_NAME, Constants.DB.TABLE_NAME);

            if (Environment.CurrentDirectory.Contains(Constants.ENVIRONMENT.TEST))
            {
                DirectoryInfo d = Directory.GetParent(Environment.CurrentDirectory).Parent;
                db_path = Path.Combine(d.ToString(), Constants.DB.FOLDER_NAME, Constants.DB.TABLE_NAME);
            }

            if (!File.Exists(db_path))
            {
                return null;
            }

            string dictFile = db_path; //@"c:\temp\dict.txt";

            var dict = new HashSet<string>(File.ReadAllLines(dictFile).Where(s => s.Length == input.Length).Select(s => s.ToLower()));
            var words = from d in dict where d.Length == input.Length && new string(d.OrderBy(c => c).ToArray()) == input select d;

            return string.Join("\n", words);
        }
    }
}
