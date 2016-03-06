using CodedSelenium.HtmlControls;
using FluentAssertions;
using NUnit.Framework;

namespace CodedSelenium.Test
{
    [TestFixture]
    public class BasicCheck : BasicTest
    {
        [Test]
        public void BasicCheck_HtmlEdit()
        {
            HtmlDiv div = new HtmlDiv(BrowserWindow);
            div.SearchProperties.Add(HtmlControl.PropertyNames.Id, "loginFields");
            HtmlEdit edit = new HtmlEdit(div);

            string value = "banana";
            edit.Text = value;
            edit.Text.Should().Be(value);
        }

        [Test]
        public void BasicCheck_HtmlButton_ByInnerText()
        {
            HtmlButton button = new HtmlButton(BrowserWindow);
            button.SearchProperties.Add(HtmlButton.PropertyNames.InnerText, "Second Button");
            button.Click();

            AssertResult("secondButton", "click");
        }

        [Test]
        public void BasicCheck_FilterProperties()
        {
            HtmlButton button = new HtmlButton(BrowserWindow);
            button.FilterProperties.Add(HtmlButton.PropertyNames.InnerText, "Second Button");
            button.Click();

            AssertResult("secondButton", "click");
        }

        [Test]
        public void BasicCheck_GetChildren()
        {
            HtmlDiv div = new HtmlDiv(BrowserWindow);
            div.SearchProperties.Add(HtmlControl.PropertyNames.Id, "loginFields");
            div.GetChildren()[2].Click();

            AssertResult("thirdButton", "click");
        }

        [Test]
        public void BasicCheck_FindMatchingControls()
        {
            HtmlButton button = new HtmlButton(BrowserWindow);
            button.FindMatchingControls()[2].Click();

            AssertResult("thirdButton", "click");
        }

        [Test]
        public void BasicCheck_CopyFrom()
        {
            HtmlButton button = new HtmlButton(BrowserWindow);
            button.CopyFrom(button.FindMatchingControls()[2]);
            button.Click();

            AssertResult("thirdButton", "click");
        }

        [Test]
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
