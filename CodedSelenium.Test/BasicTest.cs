using CodedSelenium.HtmlControls;
using FluentAssertions;
using NUnit.Framework;
using System;
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

        [TearDown]
        public void TestCleanup()
        {
            BrowserWindow.ExecuteScript("document.getElementById(\"logId\").innerHTML = ''");
            BrowserWindow.ExecuteScript("document.getElementById(\"logAction\").innerHTML = ''");
            BrowserWindow.ExecuteScript("document.getElementById(\"logDetails\").innerHTML = ''");
            this.AssertResult(string.Empty, string.Empty);
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
            detailsControl.InnerText.Should().Be(details, "Because unexpected details");
        }
    }
}
