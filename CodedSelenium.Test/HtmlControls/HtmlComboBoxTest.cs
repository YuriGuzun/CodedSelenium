using CodedSelenium.HtmlControls;
using FluentAssertions;
using NUnit.Framework;

namespace CodedSelenium.Test.HtmlControls
{
    [TestFixture]
    public class HtmlComboBoxTest : BasicTest
    {
        [Test]
        public void HtmlComboBoxTest_SelectedItem()
        {
            HtmlComboBox comboBox = new HtmlComboBox(BasicTestPage);
            string coffee = "Coffee";
            string tea = "Tea";
            comboBox.SelectedItem = coffee;
            comboBox.SelectedItem.Should().Be(coffee);
            AssertResult("comboBox", "click");

            comboBox.SelectedItem = tea;
            comboBox.SelectedItem.Should().Be(tea);
            AssertResult("comboBox", "click");
        }

        [Test]
        public void HtmlComboBoxTest_SelectedIndex()
        {
            HtmlComboBox comboBox = new HtmlComboBox(BasicTestPage);
            int expectedValue = 1;
            comboBox.SelectedIndex = expectedValue;
            comboBox.SelectedIndex.Should().Be(expectedValue);
            comboBox.SelectedItem.Should().Be("Coffee");
            AssertResult("comboBox", "click");

            expectedValue = 2;
            comboBox.SelectedIndex = expectedValue;
            comboBox.SelectedIndex.Should().Be(expectedValue);
            comboBox.SelectedItem.Should().Be("Tea");
            AssertResult("comboBox", "click");
        }
    }
}
