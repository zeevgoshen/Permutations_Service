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
        int sum, currentValue = 0;
        Utils utils;

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
                    numberOfDigits = s.Length -1 - s.LastIndexOf(":", StringComparison.OrdinalIgnoreCase);

                    bool res = int.TryParse( s.Substring(s.LastIndexOf(":", StringComparison.OrdinalIgnoreCase) + 1,
                     numberOfDigits), out currentValue);
                    sum += currentValue;
                }

                if (sum > 0 && requestItems.Any())
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
