using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Permutation_Services.Common
{
    public class SerializeStats
    {
        public int WordCount
        {
            set;
            get;
        }

        public int RequestCount
        {
            set;
            get;
        }

        public int AvgRequestTime
        {
            set;
            get;
        }

        public static SerializeStats Create(int wordList, int requestCount, int avgTimePerRequest)
        {
            SerializeStats serializedStatsList = new SerializeStats();
            serializedStatsList.WordCount = wordList;// wordList comes from the DB
            serializedStatsList.RequestCount = requestCount;// wordList comes from the DB
            serializedStatsList.AvgRequestTime = avgTimePerRequest;// wordList comes from the DB

            return serializedStatsList;
        }

        public string Convert()
        {
            string serializedStatsList = JsonConvert.SerializeObject(this);
            return serializedStatsList;
        }
    }
}
