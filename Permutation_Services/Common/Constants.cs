using System;
namespace Permutation_services
{
    public class Constants
    {
        public Constants()
        {

        }

        public class UserInput
        {
            public const string EMPTY_INPUTWORD = "-'inputWord' is empty.";
        }
        public class DB
        {
            public const string FOLDER_NAME = "words_clean";
            public const string TABLE_NAME = "words_clean.txt";
            public const string DB_NO_RESULTS = "The DB didn't return any results.";

        }
        public class Logs
        {
            public const string LOGS_FOLDER = "Logs";
            public const string LOG_FILENAME = "Log.txt";
        }
    }
}