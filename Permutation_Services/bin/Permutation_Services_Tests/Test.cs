using NUnit.Framework;
using Permutation_Services;
using Permutation_Services.Common;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Permutation_Services_Tests
{
    [TestFixture()]
    public class Test
    {
        Utils utils;

        Permutation_Services.similar similarInst1 = null;
        Permutation_Services.similar similarInst2 = null;
        Permutation_Services.similar similarInst3 = null;
        private static readonly object syncLock = new object();

        public Test()
        {
            utils = Utils.GetInstance();

            if (utils.InitLogging().Equals(1))
            {
                File.WriteAllText(Path.Combine(Environment.CurrentDirectory, Constants.Logs.LOGS_FOLDER, Constants.Logs.LOG_FILENAME), "Log init failed.");
            }
        }

        [Test()]
        public void TestShortData()
        {
            Permutation_Services.similar similarInst = new Permutation_Services.similar();
            similarInst.Find_Permutations_In_DB("apple");
            Assert.IsNotEmpty(similarInst.mJsonString);
        }

        [Test()]
        public void TestBigData()
        {
            Permutation_Services.similar similarInst = new Permutation_Services.similar();
            similarInst.Find_Permutations_In_DB("appleappleapple");
            Assert.IsNotEmpty(similarInst.mJsonString);
        }

        [Test()]
        public void TestThreadingPermutations()
        {
            Thread t1 = new Thread(HandleThreadStart);
            Thread t2 = new Thread(HandleThreadStart);
            Thread t3 = new Thread(HandleThreadStart);

            t1.Start();
            t2.Start();
            t3.Start();

        }

        void HandleThreadStart()
        {
            Permutation_Services.similar similarInst = new Permutation_Services.similar();


            //similarInst.Find_Permutations_In_DB("atmosphere");
            GetAndMatchPermutations gp = new GetAndMatchPermutations();

            gp.Find_Permutations_In_DB_Test("cat");

            lock (syncLock)
            {

                if (similarInst1 == null)
                {
                    similarInst1 = new Permutation_Services.similar();
                    similarInst1.mJsonString = similarInst.mJsonString;
                    Console.Write(similarInst1.mJsonString);
                }
                else if (similarInst2 == null)
                {
                    similarInst2 = new Permutation_Services.similar();
                    similarInst2.mJsonString = similarInst.mJsonString;
                    Console.Write(similarInst2.mJsonString);
                }
                else if (similarInst3 == null)
                {
                    similarInst3 = new Permutation_Services.similar();
                    similarInst3.mJsonString = similarInst.mJsonString;
                    Console.Write(similarInst3.mJsonString);
                    System.Diagnostics.Trace.WriteLine(similarInst3.mJsonString);

                    string interesting = TestContext.CurrentContext.ToString();
                    //TestContext.Out.WriteLine("Message to write to log");
                }
            }

            Assert.IsTrue(similarInst1.mJsonString == similarInst2.mJsonString);//== similarInst3.mJsonString);
        }

        [Test()]
        public void Test_new_algo()
        {
            GetAndMatchPermutations gp = new GetAndMatchPermutations();

            string response = gp.new_algo("cat");
        }

    }
}
