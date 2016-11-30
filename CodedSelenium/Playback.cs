using System;
using System.Threading;

namespace CodedSelenium
{
    public static class Playback
    {
        public static int Wait(int thinkTimeMilliseconds)
        {
            Thread.Sleep(thinkTimeMilliseconds);
            return thinkTimeMilliseconds;
        }
    }
}
