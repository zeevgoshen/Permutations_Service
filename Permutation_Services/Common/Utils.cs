﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Permutation_Services;

namespace Permutation_Services.Common
{
    public class Utils
    {
        public static string log_path = Path.Combine(Environment.CurrentDirectory, Constants.Logs.LOGS_FOLDER, Constants.Logs.LOG_FILENAME);
        private static readonly object syncLock = new object();
        private static Utils mInstance;
        public List<string> mResults;

        private Utils()
        {

        }

        public static Utils GetInstance()
        {
            if (mInstance == null) // first check
            {
                lock (syncLock)
                {
                    if (mInstance == null) // second check
                    {
                        mInstance = new Utils();
                    }

                }
            }
            return mInstance;
        }

        public int InitLogging()
        {
            try
            {
                //utils = Utils.GetInstance();
                log_path = CreateAndStartLogging();
                mInstance.WriteLog(log_path, "INFO", "Logging started.");
                //utils.WriteLog(log_path, "INFO", "Logging started.");

                return 0;
            }
            catch (Exception ex)
            {
                File.WriteAllText(Path.Combine(Environment.CurrentDirectory, Constants.Logs.LOG_FILENAME), ex.StackTrace + " " + ex.Message);
                return 1;
            }
        }

        public string CreateAndStartLogging()
        {
            log_path = Path.Combine(Environment.CurrentDirectory, Constants.Logs.LOGS_FOLDER);

            if (!Directory.Exists(log_path))
            {
                DirectoryInfo di = Directory.CreateDirectory(log_path);
            }

            return log_path = Path.Combine(Environment.CurrentDirectory, Constants.Logs.LOGS_FOLDER, Constants.Logs.LOG_FILENAME);
        }

        public void WriteLog(string log_path, string severity, string txt)
        {
            File.AppendAllText(log_path, "\n" + DateTime.Now + " " + severity + " " + txt);
        }

        public string SerializeDBResultsList(List<string> wordsFromDB)
        {
            SerializeWorldList serializedWords = SerializeWorldList.Create(wordsFromDB);
            return serializedWords.Convert();
        }

        public Task GetPermutationsWithDuplicates(string s)
        {
            try
            {
                mResults = new List<string>();

                string res = new_algo(s);

                string[] lines = res.Split(
                    new[] { "\r\n", "\r", "\n" },
                    StringSplitOptions.None
                );

                mResults = lines.ToList();

                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                mInstance.WriteLog(log_path, "ERROR", ex.Message + " " + ex.StackTrace);
                //response.Write("Query too big.");
                return null;
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
