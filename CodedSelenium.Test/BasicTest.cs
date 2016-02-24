using CodedSelenium.HtmlControls;
using FluentAssertions;
using System.IO;

namespace CodedSelenium.Test
{
    public class BasicTest
    {
        protected static string PathToPage = Directory.GetCurrentDirectory() + "\\TestPages\\TestPage.html";
        protected static BrowserWindow BrowserWindow = BrowserWindow.Launch(PathToPage);

        protected void AssertResult(string elementId, string action)
        {
            HtmlControl idControl = new HtmlControl(BrowserWindow);
            idControl.SearchProperties.Add(HtmlControl.PropertyNames.TagName, "p");
            idControl.SearchProperties.Add(HtmlControl.PropertyNames.Id, "id");

            HtmlControl actionControl = new HtmlControl(BrowserWindow);
            actionControl.SearchProperties.Add(HtmlControl.PropertyNames.TagName, "p");
            actionControl.SearchProperties.Add(HtmlControl.PropertyNames.Id, "action");

            idControl.InnerText.Should().Be(elementId, "Because unexpected elementId {0}-ed", action);
            actionControl.InnerText.Should().Be(action, "Because unexpected action");
        }
    }
}