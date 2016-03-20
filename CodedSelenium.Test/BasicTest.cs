using CodedSelenium.HtmlControls;
using CodedSelenium.Test.ObjectMap;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.IO;

namespace CodedSelenium.Test
{
    public class BasicTest
    {
        public const string PageName = "BasicTestPage.html";
        private static BrowserWindow browserWindow;
        private static string pathToPage;
        private static BasicTestPage basicTestPage;

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

        protected static BasicTestPage BasicTestPage
        {
            get
            {
                if (BasicTest.basicTestPage == null)
                {
                    BasicTest.basicTestPage = new BasicTestPage(BasicTest.BrowserWindow);
                }

                BasicTest.basicTestPage.Launch();
                return BasicTest.basicTestPage;
            }
        }

        protected static BrowserWindow BrowserWindow
        {
            get
            {
                if (BasicTest.browserWindow == null)
                {
                    BasicTest.browserWindow = BrowserWindow.Launch(PathToPage);
                }

                return BasicTest.browserWindow;
            }
        }

        [TearDown]
        public void TestCleanup()
        {
            string script =
                "jQuery('.log').each(function(index) {" +
                "   $( this ).text('')" +
                "});";
            BrowserWindow.ExecuteScript(script);
            AssertResult(string.Empty, string.Empty);
        }

        protected void AssertResult(string elementId, string action, string details = "")
        {
            HtmlDiv parent = new HtmlDiv(BrowserWindow);
            parent.SearchProperties.Add(HtmlDiv.PropertyNames.Id, "log");

            HtmlControl idControl = new HtmlControl(parent);
            idControl.SearchProperties.Add(HtmlControl.PropertyNames.TagName, "p");
            idControl.SearchProperties.Add(HtmlControl.PropertyNames.Id, "logId");

            HtmlControl actionControl = new HtmlControl(parent);
            actionControl.SearchProperties.Add(HtmlControl.PropertyNames.TagName, "p");
            actionControl.SearchProperties.Add(HtmlControl.PropertyNames.Id, "logAction");

            HtmlControl detailsControl = new HtmlControl(parent);
            detailsControl.SearchProperties.Add(HtmlControl.PropertyNames.TagName, "p");
            detailsControl.SearchProperties.Add(HtmlControl.PropertyNames.Id, "logDetails");

            idControl.InnerText.Should().Be(elementId, "Because unexpected elementId {0}-ed", action);
            actionControl.InnerText.Should().Be(action, "Because unexpected action");

            if (!string.IsNullOrEmpty(details))
                detailsControl.InnerText.Should().Be(details, "Because unexpected details");
        }
    }
}
