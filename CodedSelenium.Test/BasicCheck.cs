using CodedSelenium.HtmlControls;
using FluentAssert;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace CodedSelenium.Test
{
    [TestClass]
    public class BasicCheck
    {
        [TestMethod]
        public void BasicCheck_HtmlEdit()
        {
            string pathToPage = Directory.GetCurrentDirectory() + "\\TestPages\\TestPage.html";
            BrowserWindow bw = BrowserWindow.Launch(pathToPage);

            HtmlDiv div = new HtmlDiv(bw);
            div.SearchProperties.Add(HtmlControl.PropertyNames.Id, "loginFields");
            HtmlEdit edit = new HtmlEdit(div);

            string value = "banana";
            edit.Text = value;
            edit.Text.ShouldBeEqualTo(value);

            HtmlButton button = new HtmlButton(div);
            button.SearchProperties.Add(HtmlButton.PropertyNames.InnerText, "Second Button");
            button.Click();

            HtmlControl id = new HtmlControl(bw);
            id.SearchProperties.Add(HtmlControl.PropertyNames.TagName, "p");
            id.SearchProperties.Add(HtmlControl.PropertyNames.Id, "id");

            HtmlControl action = new HtmlControl(bw);
            action.SearchProperties.Add(HtmlControl.PropertyNames.TagName, "p");
            action.SearchProperties.Add(HtmlControl.PropertyNames.Id, "action");

            id.InnerText.ShouldBeEqualTo("secondButton");
            action.InnerText.ShouldBeEqualTo("click");
        }
    }
}