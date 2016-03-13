using CodedSelenium.Test.ObjectMap;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodedSelenium.Test
{
    [TestFixture]
    public class MouseTest : BasicTest
    {
        private MouseTestPage mouseTestPage;

        private MouseTestPage MouseTestPage
        {
            get
            {
                if (mouseTestPage == null)
                {
                    mouseTestPage = new MouseTestPage(BasicTest.BrowserWindow);
                }

                mouseTestPage.Launch();
                return mouseTestPage;
            }
        }

        [Test]
        public void MouseTest_Click()
        {
            Mouse.Click(MouseTestPage.FirstDiv);
        }
    }
}
