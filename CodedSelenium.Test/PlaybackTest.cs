using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace CodedSelenium.Test
{
    [TestFixture]
    public class PlaybackTest : BasicTest
    {
        [Test]
        public void PlaybackTest_RegisterErrorCallback_DoesntThrow()
        {
            assignPlaybackError.ShouldNotThrow();
        }

        {
            throw new NotImplementedException();
        }
    }
}
