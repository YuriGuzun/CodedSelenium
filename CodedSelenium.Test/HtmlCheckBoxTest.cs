using CodedSelenium.HtmlControls;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodedSelenium.Test
{
    [TestClass]
    public class HtmlCheckBoxTest : BasicTest
    {
        [TestMethod]
        public void HtmlComboBoxTest_SelectedItem()
        {
            HtmlCheckBox checkBox = new HtmlCheckBox(BrowserWindow);
            checkBox.Checked.Should().BeFalse();

            checkBox.Checked = true;
            checkBox.Checked.Should().BeTrue();
            this.AssertResult("checkBox", "change");

            checkBox.Checked = false;
            checkBox.Checked.Should().BeFalse();
            this.AssertResult("checkBox", "change");
        }
    }
}
