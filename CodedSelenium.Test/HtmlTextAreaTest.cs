using CodedSelenium.HtmlControls;
using FluentAssertions;
using NUnit.Framework;

namespace CodedSelenium.Test
{
    [TestFixture]
    public class HtmlTextAreaTest : BasicTest
    {
        [Test]
        public void HtmlTextAreaTest_ByLabledBy_Equals()
        {
            HtmlTextArea textArea = new HtmlTextArea(BrowserWindow);
            textArea.SearchProperties.Add(HtmlTextArea.PropertyNames.LabeledBy, "Description2");

            textArea.Exists.Should().BeTrue();

            textArea.Text = "banana";
            textArea.Text.Should().Be("banana");

            this.AssertResult("anotherTextArea", "keypress");
        }

        [Test]
        public void HtmlTextAreaTest_ByLabledBy_Contains()
        {
            HtmlTextArea textArea = new HtmlTextArea(BrowserWindow);
            textArea.SearchProperties.Add(HtmlTextArea.PropertyNames.LabeledBy, "tion2", PropertyExpressionOperator.Contains);

            textArea.Exists.Should().BeTrue();

            textArea.Text = "banana";
            textArea.Text.Should().Be("banana");

            this.AssertResult("anotherTextArea", "keypress");
        }
    }
}
