using CodedSelenium.HtmlControls;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodedSelenium.Test
{
    [TestClass]
    public class BasicCheck : BasicTest
    {
        [TestMethod]
        public void BasicCheck_HtmlEdit()
        {
            HtmlDiv div = new HtmlDiv(BrowserWindow);
            div.SearchProperties.Add(HtmlControl.PropertyNames.Id, "loginFields");
            HtmlEdit edit = new HtmlEdit(div);

            string value = "banana";
            edit.Text = value;
            edit.Text.Should().Be(value);
        }

        [TestMethod]
        public void BasicCheck_HtmlButton_ByInnerText()
        {
            HtmlButton button = new HtmlButton(BrowserWindow);
            button.SearchProperties.Add(HtmlButton.PropertyNames.InnerText, "Second Button");
            button.Click();

            this.AssertResult("secondButton", "click");
        }

        [TestMethod]
        public void BasicCheck_FilterProperties()
        {
            HtmlButton button = new HtmlButton(BrowserWindow);
            button.FilterProperties.Add(HtmlButton.PropertyNames.InnerText, "Second Button");
            button.Click();

            this.AssertResult("secondButton", "click");
        }

        [TestMethod]
        public void BasicCheck_GetChildren()
        {
            HtmlDiv div = new HtmlDiv(BrowserWindow);
            div.SearchProperties.Add(HtmlControl.PropertyNames.Id, "loginFields");
            div.GetChildren()[2].Click();

            this.AssertResult("thirdButton", "click");
        }

        [TestMethod]
        public void BasicCheck_FindMatchingControls()
        {
            HtmlButton button = new HtmlButton(BrowserWindow);
            button.FindMatchingControls()[2].Click();

            this.AssertResult("thirdButton", "click");
        }

        [TestMethod]
        public void BasicCheck_CopyFrom()
        {
            HtmlButton button = new HtmlButton(BrowserWindow);
            button.CopyFrom(button.FindMatchingControls()[2]);
            button.Click();

            this.AssertResult("thirdButton", "click");
        }

        [TestMethod]
        public void BasicCheck_GetParent()
        {
            HtmlButton button = new HtmlButton(BrowserWindow);
            button.SearchProperties.Add(HtmlButton.PropertyNames.Id, "secondButton");
            HtmlDiv div = new HtmlDiv();
            UITestControl parent = button.GetParent();
            div.CopyFrom(parent);
            div.Id.Should().Be("buttons");
        }
    }
}
