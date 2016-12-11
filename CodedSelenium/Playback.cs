using System;
using System.Threading;

namespace CodedSelenium
{
    public static class Playback
    {
        /// <summary>
        /// Property added for Compatibility Purposes Only
        /// Doesn't work
        /// </summary>
        public static event EventHandler<PlaybackErrorEventArgs> PlaybackError
        {
            add
            {
            }

            remove
            {
            }
        }

        public static int Wait(int thinkTimeMilliseconds)
        {
            Thread.Sleep(thinkTimeMilliseconds);
            return thinkTimeMilliseconds;
        }
    }
}
