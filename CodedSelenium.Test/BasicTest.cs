using CodedSelenium.HtmlControls;
using CodedSelenium.Test.ObjectMap;
using FluentAssertions;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;

namespace CodedSelenium.Test
{
    public class BasicTest
    {
        public const string PageName = "BasicTestPage.html";
        public const string SkipCiCategory = "SkipCi";
        private static BrowserWindow _browserWindow;
        private static string _pathToPage;
        private static BasicTestPage _basicTestPage;
        private static WebDriverWait _webDriverWait;

        protected static string PathToPage
        {
            get
            {
                if (_pathToPage == null)
                {
                    _pathToPage = Directory.GetCurrentDirectory() + "\\TestPages\\" + PageName;
                }

                return _pathToPage;
            }
        }

        protected static BasicTestPage BasicTestPage
        {
            get
            {
                if (BasicTest._basicTestPage == null)
                {
                    BasicTest._basicTestPage = new BasicTestPage(BasicTest.BrowserWindow);
                }

                BasicTest._basicTestPage.Launch();
                return BasicTest._basicTestPage;
            }
        }

        protected static BrowserWindow BrowserWindow
        {
            get
            {
                if (BasicTest._browserWindow == null)
                {
                    BasicTest._browserWindow = BrowserWindow.Launch(PathToPage);
                }

                return BasicTest._browserWindow;
            }
        }

        protected static WebDriverWait Wait
        {
            get
            {
                if (BasicTest._webDriverWait == null)
                {
                    BasicTest._webDriverWait = new WebDriverWait(BrowserWindow.Driver, TimeSpan.FromSeconds(1));
                    BasicTest._webDriverWait.PollingInterval = TimeSpan.FromMilliseconds(0);
                }

                return BasicTest._webDriverWait;
            }
        }

        public void CleanLogs()
        {
            string script =
                "jQuery('.log').each(function(index) {" +
                "   $( this ).text('')" +
                "});";
            BrowserWindow.ExecuteScript(script);
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

            Wait.Until((d) => { return idControl.InnerText.Equals(elementId); });
            idControl.InnerText.Should().Be(elementId, "Because unexpected elementId {0}-ed", action);

            Wait.Until((d) => { return actionControl.InnerText.Equals(action); });
            actionControl.InnerText.Should().Be(action, "Because unexpected action");

            if (!string.IsNullOrEmpty(details))
            {
                Wait.Until((d) => { return detailsControl.InnerText.Equals(details); });
                detailsControl.InnerText.Should().Be(details, "Because unexpected details");
            }

            CleanLogs();
        }
    }
}
