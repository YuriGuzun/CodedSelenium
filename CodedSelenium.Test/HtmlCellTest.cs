using CodedSelenium.HtmlControls;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodedSelenium.Test
{
    [TestClass]
    public class HtmlCellTest : BasicTest
    {
        [TestMethod]
        public void HtmlCellTest_ById()
        {
            HtmlCell checkBox = new HtmlCell(BrowserWindow);
            checkBox.SearchProperties.Add(HtmlCell.PropertyNames.Id, "cellWithId");

            checkBox.InnerText.Should().Be("Item 113");
        }

        [TestMethod]
        public void HtmlCellTest_ByInnerText()
        {
            HtmlCell checkBox = new HtmlCell(BrowserWindow);
            checkBox.SearchProperties.Add(HtmlCell.PropertyNames.InnerText, "Item 113");

            checkBox.Id.Should().Be("cellWithId");
        }

        [TestMethod]
        public void HtmlCellTest_ByColumnIndex()
        {
            HtmlCell checkBox = new HtmlCell(BrowserWindow);
            checkBox.SearchProperties.Add(HtmlCell.PropertyNames.ColumnIndex, "2");

            checkBox.InnerText.Should().Be("Item 011");
        }

        [TestMethod]
        public void HtmlCellTest_ByRowIndex()
        {
            HtmlCell checkBox = new HtmlCell(BrowserWindow);
            checkBox.SearchProperties.Add(HtmlCell.PropertyNames.RowIndex, "2");

            checkBox.InnerText.Should().Be("Item 002");
        }

        [TestMethod]
        public void HtmlCellTest_ByRowAndColumnIndex()
        {
            HtmlCell checkBox = new HtmlCell(BrowserWindow);
            checkBox.SearchProperties.Add(HtmlCell.PropertyNames.ColumnIndex, "2");
            checkBox.SearchProperties.Add(HtmlCell.PropertyNames.RowIndex, "2");

            checkBox.InnerText.Should().Be("Item 012");
        }

        [TestMethod]
        public void HtmlCellTest_ByInstance()
        {
            HtmlCell checkBox = new HtmlCell(BrowserWindow);
            checkBox.SearchProperties.Add(HtmlCell.PropertyNames.Instance, "4");

            checkBox.InnerText.Should().Be("Item 012");
        }

        [TestMethod]
        public void HtmlCellTest_ByTagInstance()
        {
            HtmlCell checkBox = new HtmlCell(BrowserWindow);
            checkBox.SearchProperties.Add(HtmlCell.PropertyNames.Instance, "4");

            checkBox.InnerText.Should().Be("Item 012");
        }
    }
}
