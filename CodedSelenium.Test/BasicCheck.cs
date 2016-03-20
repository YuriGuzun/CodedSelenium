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
            HtmlDiv div = new HtmlDiv(BasicTestPage);
            div.SearchProperties.Add(HtmlControl.PropertyNames.Id, "loginFields");
            HtmlEdit edit = new HtmlEdit(div);

            string value = "banana";
            edit.Text = value;
            edit.Text.Should().Be(value);
        }

        [Test]
        public void BasicCheck_HtmlButton_ByInnerText()
        {
            HtmlButton button = new HtmlButton(BasicTestPage);
            button.SearchProperties.Add(HtmlButton.PropertyNames.InnerText, "Second Button");
            Mouse.Click(button);

            AssertResult("secondButton", "click");
        }

        [Test]
        public void BasicCheck_FilterProperties()
        {
            HtmlButton button = new HtmlButton(BasicTestPage);
            button.FilterProperties.Add(HtmlButton.PropertyNames.InnerText, "Second Button");
            Mouse.Click(button);

            AssertResult("secondButton", "click");
        }

        [Test]
        public void BasicCheck_GetChildren()
        {
            HtmlDiv div = new HtmlDiv(BasicTestPage);
            div.SearchProperties.Add(HtmlControl.PropertyNames.Id, "loginFields");
            Mouse.Click(div.GetChildren()[2]);

            AssertResult("thirdButton", "click");
        }

        [Test]
        public void BasicCheck_FindMatchingControls()
        {
            HtmlButton button = new HtmlButton(BasicTestPage);
            Mouse.Click(button.FindMatchingControls()[2]);

            AssertResult("thirdButton", "click");
        }

        [Test]
        public void BasicCheck_CopyFrom()
        {
            HtmlButton button = new HtmlButton(BasicTestPage);
            button.CopyFrom(button.FindMatchingControls()[2]);
            Mouse.Click(button);

            AssertResult("thirdButton", "click");
        }

        [Test]
        public void BasicCheck_GetParent()
        {
            HtmlButton button = new HtmlButton(BasicTestPage);
            button.SearchProperties.Add(HtmlButton.PropertyNames.Id, "secondButton");
            HtmlDiv div = new HtmlDiv();
            CodedSelenium.UITestControl parent = button.GetParent();
            div.CopyFrom(parent);
            div.Id.Should().Be("buttons");
        }
    }
}
