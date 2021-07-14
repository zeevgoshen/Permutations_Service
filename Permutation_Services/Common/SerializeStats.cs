using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Permutation_Services.Common
{
    public class SerializeStats
    {
        public int totalWords
        {
            set;
            get;
        }

        public int totalRequests
        {
            set;
            get;
        }

        public int avgProcessingTimeNs
        {
            set;
            get;
        }

        public static SerializeStats Create(int wordList, int requestCount, int avgTimePerRequest)
        {
            SerializeStats serializedStatsList = new SerializeStats();
            serializedStatsList.totalWords = wordList;
            serializedStatsList.totalRequests = requestCount;
            serializedStatsList.avgProcessingTimeNs = avgTimePerRequest;
            return serializedStatsList;
        }

        public string Convert()
        {
            string serializedStatsList = JsonConvert.SerializeObject(this);
            return serializedStatsList;
        }
    }
}
