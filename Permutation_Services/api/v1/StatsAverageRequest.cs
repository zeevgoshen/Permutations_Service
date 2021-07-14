using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Permutation_Services.Common;

namespace Permutation_Services.api.v1
{
    public class StatsAverageRequest
    {
        IEnumerable<string> requestItems;
        string log_path = Path.Combine(Environment.CurrentDirectory, Constants.Logs.LOGS_FOLDER, Constants.Logs.LOG_FILENAME);

        int numberOfDigits = 0;
        int sum = 0;
        Utils utils;
        int result;

        public StatsAverageRequest(IEnumerable<string> RequestItems)
        {
            requestItems = RequestItems;
            utils = Utils.GetInstance();
        }

        public int ParseString()
        {
            try
            {
                foreach (string s in requestItems)
                {
                    numberOfDigits = s.Length -1 - s.LastIndexOf(":");

                    bool res = int.TryParse( s.Substring(s.LastIndexOf(":") + 1, numberOfDigits), out sum);
                }

                if (sum > 0 && requestItems.Count() > 0)
                {
                    return (int)sum / requestItems.Count();
                }
                return 0;
            }
            catch (Exception ex)
            {
                utils.WriteLog(log_path, "ERROR", ex.Message + " " + ex.StackTrace);
                return 0;
            }
        }
    }
}
