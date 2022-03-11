using System;
using System.Threading;

namespace CarFactory.Utilities
{
    /*
    *  This class is off limits
    *  
    *  It's primary purpose is to hide dummy waiting times
    *  inside function that looks useful from the outside.
    */
    public static class SlowWorker
    {
        public static void FakeWorkingForMillis(int millis)
        {
            Thread.Sleep(millis);
        }
    }
}
