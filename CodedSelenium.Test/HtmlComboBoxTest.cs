using CodedSelenium.HtmlControls;
using FluentAssertions;
using NUnit.Framework;

namespace CodedSelenium.Test
{
    [TestFixture]
    public class HtmlComboBoxTest : BasicTest
    {
        [Test]
        public void HtmlComboBoxTest_SelectedItem()
        {
            HtmlComboBox comboBox = new HtmlComboBox(BrowserWindow);
            string coffee = "Coffee";
            string tea = "Tea";
            comboBox.SelectedItem = coffee;
            comboBox.SelectedItem.Should().Be(coffee);
            this.AssertResult("comboBox", "click");

            comboBox.SelectedItem = tea;
            comboBox.SelectedItem.Should().Be(tea);
            this.AssertResult("comboBox", "click");
        }

        [Test]
        public void HtmlComboBoxTest_SelectedIndex()
        {
            HtmlComboBox comboBox = new HtmlComboBox(BrowserWindow);
            int expectedValue = 1;
            comboBox.SelectedIndex = expectedValue;
            comboBox.SelectedIndex.Should().Be(expectedValue);
            comboBox.SelectedItem.Should().Be("Coffee");
            this.AssertResult("comboBox", "click");

            expectedValue = 2;
            comboBox.SelectedIndex = expectedValue;
            comboBox.SelectedIndex.Should().Be(expectedValue);
            comboBox.SelectedItem.Should().Be("Tea");
            this.AssertResult("comboBox", "click");
        }
    }
}
