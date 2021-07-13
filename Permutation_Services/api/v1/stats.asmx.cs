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
            utils = Utils.GetInstance();


            //utils.OpenDBFile(); // <- needs refactoring to not return json

            string log_path = Path.Combine(Environment.CurrentDirectory, Constants.Logs.LOGS_FOLDER, Constants.Logs.LOG_FILENAME);

            if (!File.Exists(log_path))
            {
                return null;//"No log file found.";
            }

            // By default, ReadAllLines(*) closes the file after reading
            var wordFile = File.ReadAllLines(log_path);
            var wordList = new List<string>(wordFile);

            // info from logs
            IEnumerable<string> items = wordList;
            int count = items.Count(x => x.Contains("Searching for"));


            SerializeStats ser = SerializeStats.Create(wordList.Count, count, 0);
            string jsonString = ser.Convert();
            return jsonString;
        }
    }
}
