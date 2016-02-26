using CodedSelenium.HtmlControls;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace CodedSelenium.Test
{
    public class BasicTest
    {
        public const string PageName = "TestPage.html";
        private static BrowserWindow browserWindow;
        private static string pathToPage;

        protected static string PathToPage
        {
            get
            {
                if (pathToPage == null)
                {
                    pathToPage = Directory.GetCurrentDirectory() + "\\TestPages\\" + PageName;
                }

                return pathToPage;
            }
        }

        protected static CodedSelenium.BrowserWindow BrowserWindow
        {
            get
            {
                if (browserWindow == null)
                {
                    browserWindow = CodedSelenium.BrowserWindow.Launch(PathToPage);
                }

                return browserWindow;
            }
        }

        protected void AssertResult(string elementId, string action)
        {
            HtmlDiv parent = new HtmlDiv(BrowserWindow);
            parent.SearchProperties.Add(HtmlDiv.PropertyNames.Id, "log");

            HtmlControl idControl = new HtmlControl(parent);
            idControl.SearchProperties.Add(HtmlControl.PropertyNames.TagName, "p");
            idControl.SearchProperties.Add(HtmlControl.PropertyNames.Id, "logId");

            HtmlControl actionControl = new HtmlControl(parent);
            actionControl.SearchProperties.Add(HtmlControl.PropertyNames.TagName, "p");
            actionControl.SearchProperties.Add(HtmlControl.PropertyNames.Id, "logAction");

            idControl.InnerText.Should().Be(elementId, "Because unexpected elementId {0}-ed", action);
            actionControl.InnerText.Should().Be(action, "Because unexpected action");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            BrowserWindow.ExecuteScript("document.getElementById(\"logId\").innerHTML = ''");
            BrowserWindow.ExecuteScript("document.getElementById(\"logAction\").innerHTML = ''");
            this.AssertResult(string.Empty, string.Empty);
        }
    }
}