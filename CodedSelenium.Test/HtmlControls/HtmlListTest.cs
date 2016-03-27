using CodedSelenium.HtmlControls;
using FluentAssertions;
using NUnit.Framework;

namespace CodedSelenium.Test.HtmlControls
{
    [TestFixture]
    public class HtmlListTest : BasicTest
    {
        private HtmlList _list;

        private HtmlList List
        {
            get
            {
                if (_list == null)
                {
                    _list = new HtmlList(BasicTestPage);
                    _list.SearchProperties.Add(HtmlList.PropertyNames.Id, "htmlList");
                }

                return _list;
            }
        }

        [TestCase("Option 1", "Option 3")]
        [TestCase("Option 4")]
        [TestCase]
        [Test]
        public void HtmlListTest_SelectedItems(params string[] expectedValues)
        {
            List.SelectedItems = expectedValues;
            List.SelectedItems.Should().Equal(expectedValues);
        }

        [TestCase(0, 2)]
        [TestCase(3)]
        [TestCase]
        [Test]
        public void HtmlListTest_SelectedIndex(params int[] expectedIndices)
        {
            List.SelectedIndices = expectedIndices;
            List.SelectedIndices.Should().Equal(expectedIndices);
        }

        [TestCase("Option 1", "Option 3")]
        [TestCase("Option 4")]
        [TestCase]
        [Test]
        public void HtmlListTest_SelectedItemsAsString(params string[] expectedValues)
        {
            List.SelectedItems = expectedValues;
            List.SelectedItems.Should().Equal(expectedValues);
            List.SelectedItemsAsString.Should().Be(string.Join(string.Empty, expectedValues));
        }

        [Test]
        public void HtmlListTest_IsMultipleSelection()
        {
            List.IsMultipleSelection.Should().BeTrue();

            HtmlList comboBox = new HtmlList(BasicTestPage);
            comboBox.SearchProperties.Add(HtmlList.PropertyNames.Id, "comboBox");
            comboBox.IsMultipleSelection.Should().BeFalse();
        }
    }
}
