using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Permutation_Services.Common
{
    public class SerializeWorldList
    {
        public List<string> Words
        {
            set;
            get;
        }

        public static SerializeWorldList Create(List<string> wordList)
        {
            SerializeWorldList serializedWordList = new SerializeWorldList();
            serializedWordList.Words = wordList;// wordList comes from the DB

            return serializedWordList; 
        }

        public string Convert()
        {
            string serializedWorldList = JsonConvert.SerializeObject(this);
            return serializedWorldList;
        }
    }
}
