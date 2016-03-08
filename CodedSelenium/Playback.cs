using CodedSelenium.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodedSelenium
{
    [Obsolete("For backwards compatibility only.")]
    public static class Playback
    {
        public static int Wait(int thinkTimeMilliseconds)
        {
            Thread.Sleep(thinkTimeMilliseconds);
            return thinkTimeMilliseconds;
        }
    }
}
