using CodedSelenium.HtmlControls;
using FluentAssert;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace CodedSelenium.Test
{
    [TestClass]
    public class BasicCheck
    {
        private static string PathToPage = Directory.GetCurrentDirectory() + "\\TestPages\\TestPage.html";
        private static BrowserWindow BrowserWindow = BrowserWindow.Launch(PathToPage);

        [TestMethod]
        public void BasicCheck_HtmlEdit()
        {
            HtmlDiv div = new HtmlDiv(BrowserWindow);
            div.SearchProperties.Add(HtmlControl.PropertyNames.Id, "loginFields");
            HtmlEdit edit = new HtmlEdit(div);

            string value = "banana";
            edit.Text = value;
            edit.Text.ShouldBeEqualTo(value);
        }

        [TestMethod]
        public void BasicCheck_HtmlButton_ByInnerText()
        {
            HtmlButton button = new HtmlButton(BrowserWindow);
            button.SearchProperties.Add(HtmlButton.PropertyNames.InnerText, "Second Button");
            button.Click();

            HtmlControl id = new HtmlControl(BrowserWindow);
            id.SearchProperties.Add(HtmlControl.PropertyNames.TagName, "p");
            id.SearchProperties.Add(HtmlControl.PropertyNames.Id, "id");

            HtmlControl action = new HtmlControl(BrowserWindow);
            action.SearchProperties.Add(HtmlControl.PropertyNames.TagName, "p");
            action.SearchProperties.Add(HtmlControl.PropertyNames.Id, "action");

            id.InnerText.ShouldBeEqualTo("secondButton");
            action.InnerText.ShouldBeEqualTo("click");
        }
    }
}