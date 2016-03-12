using CodedSelenium.HtmlControls;
using FluentAssertions;
using NUnit.Framework;

namespace CodedSelenium.Test
{
    [TestFixture]
    public class HtmlCheckBoxTest : BasicTest
    {
        [Test]
        public void HtmlCheckBoxTest_SelectedItem()
        {
            HtmlCheckBox checkBox = new HtmlCheckBox(BrowserWindow);
            checkBox.SearchProperties.Add(HtmlCheckBox.PropertyNames.Id, "checkBox");
            checkBox.Checked.Should().BeFalse();

            checkBox.Checked = true;
            checkBox.Checked.Should().BeTrue();
            AssertResult("checkBox", "change");

            checkBox.Checked = false;
            checkBox.Checked.Should().BeFalse();
            AssertResult("checkBox", "change");
        }
    }
}
