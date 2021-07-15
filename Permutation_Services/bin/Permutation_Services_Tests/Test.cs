using NUnit.Framework;
using System;
using System.Threading;

namespace Permutation_Services_Tests
{
    [TestFixture()]
    public class Test
    {
        [Test()]
        public void TestParallelPermutations()
        {

            // Currently there are no locks in the code
            //
            Thread t1 = new Thread(HandleThreadStart);
            t1.Start();

            Thread t2 = new Thread(HandleThreadStart);
            t2.Start();

            Semaphore sm = new Semaphore(0, 2);
            //sm.Release();
            //sm.Close();



            //Permutation_Services.Common.Utils
            //Permutation_Services.Common.SerializeStats

            // compare to lists
            Assert.AreEqual(5, 5);
        }

        void HandleThreadStart()
        {
            Permutation_Services.similar c = new Permutation_Services.similar();

            c.Find_Permutations_In_DB("apple");

        }

    }
}
