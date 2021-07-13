using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Permutation_Services;

namespace Permutation_Services.Common
{
    public class Utils
    {
        public static string log_path = Path.Combine(Environment.CurrentDirectory, Constants.Logs.LOGS_FOLDER, Constants.Logs.LOG_FILENAME);
        private static readonly object syncLock = new object();
        private static Utils mInstance;

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


        // remove:
        public string OpenDBFile()
        {
            try
            {
                mInstance.WriteLog(log_path, "INFO", "Opening DB for reading.");
                string db_path = Path.Combine(Environment.CurrentDirectory, Constants.DB.FOLDER_NAME, Constants.DB.TABLE_NAME);

                if (!File.Exists(db_path))
                {
                    return null;
                }

                // By default, ReadAllLines(*) closes the file after reading
                var wordFile = File.ReadAllLines(db_path);
                var wordList = new List<string>(wordFile);

                SerializeWorldList ser = SerializeWorldList.Create(wordList);

                return ser.Convert();
            }
            catch (Exception ex)
            {
                mInstance.WriteLog(log_path, "ERROR", ex.Message + " " + ex.StackTrace);
                return null;
            }
        }

        public List<string> OpenDBFileAndReturnList()
        {
            try
            {
                mInstance.WriteLog(log_path, "INFO", "Opening DB for reading.");
                string db_path = Path.Combine(Environment.CurrentDirectory, Constants.DB.FOLDER_NAME, Constants.DB.TABLE_NAME);

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
                mInstance.WriteLog(log_path, "ERROR", ex.Message + " " + ex.StackTrace);
                return null;
            }
        }

        public string SerializeDBResultsList(List<string> wordsFromDB)
        {
            SerializeWorldList serializedWords = SerializeWorldList.Create(wordsFromDB);
            return serializedWords.Convert();
        }

        public List<string> GetPermutationsWithDuplicatesAsync(string s)
        {
            List<string> result = new List<string>();
            Dictionary<char, int> map = BuildFreqTable(s);
            GetPermutationsWithDuplicates(map, "", s.Length, result);
            return result;
        }


        public List<string> GetPermutationsWithDuplicates(string s)
        {
            List<string> result = new List<string>();
            Dictionary<char, int> map = BuildFreqTable(s);
            GetPermutationsWithDuplicates(map, "", s.Length, result);
            return result;
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
                mInstance.WriteLog(log_path, "ERROR", ex.Message + " " + ex.StackTrace);
            }

        }

        // assumptions:
        // case-incensitive
        // whitespace is insignificant
        //
        // check character count
        // pg. 194

    }
}
