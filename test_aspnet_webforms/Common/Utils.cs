using System;
using System.Collections.Generic;
using System.IO;

namespace test_aspnet_webforms.Common
{
    public static class Utils
    {
        public static string log_path = "";

         
        public static string createAndStartLogging()
        {
            log_path = Path.Combine(Environment.CurrentDirectory, Constants.Logs.LOGS_FOLDER);

            if (!Directory.Exists(log_path))
            {
                DirectoryInfo di = Directory.CreateDirectory(log_path);
            }

            return log_path = Path.Combine(Environment.CurrentDirectory, Constants.Logs.LOGS_FOLDER, Constants.Logs.LOG_FILENAME);
        }

        public static void writeLog(string log_path, string severity, string txt)
        {
            File.AppendAllText(log_path, "\n" + DateTime.Now + " " + severity + " " + txt);
        }

        public static string OpenDBFile()
        {
            try
            {

                Utils.writeLog(log_path, "INFO", "Opening DB for reading.");
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
                return null;
            }
        }
    }
}
