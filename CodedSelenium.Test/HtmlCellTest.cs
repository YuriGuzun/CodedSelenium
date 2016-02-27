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
            HtmlCell cell = new HtmlCell(BrowserWindow);
            cell.SearchProperties.Add(HtmlCell.PropertyNames.Id, "cellWithId");

            cell.InnerText.Should().Be("Item 113");
        }

        [TestMethod]
        public void HtmlCellTest_ByInnerText()
        {
            HtmlCell cell = new HtmlCell(BrowserWindow);
            cell.SearchProperties.Add(HtmlCell.PropertyNames.InnerText, "113", PropertyExpressionOperator.Contains);

            cell.Id.Should().Be("cellWithId");
        }

        [TestMethod]
        public void HtmlCellTest_ByColumnIndex()
        {
            HtmlCell cell = new HtmlCell(BrowserWindow);
            cell.SearchProperties.Add(HtmlCell.PropertyNames.ColumnIndex, "2");

            cell.InnerText.Should().Be("Item 011");
        }

        [TestMethod]
        public void HtmlCellTest_ByRowIndex()
        {
            HtmlCell cell = new HtmlCell(BrowserWindow);
            cell.SearchProperties.Add(HtmlCell.PropertyNames.RowIndex, "2");

            cell.InnerText.Should().Be("Item 002");
        }

        [TestMethod]
        public void HtmlCellTest_ByRowAndColumnIndex()
        {
            HtmlCell cell = new HtmlCell(BrowserWindow);
            cell.SearchProperties.Add(HtmlCell.PropertyNames.ColumnIndex, "2");
            cell.SearchProperties.Add(HtmlCell.PropertyNames.RowIndex, "2");

            cell.InnerText.Should().Be("Item 012");
        }

        [TestMethod]
        public void HtmlCellTest_ByInstance()
        {
            HtmlCell cell = new HtmlCell(BrowserWindow);
            cell.SearchProperties.Add(HtmlCell.PropertyNames.Instance, "4");

            cell.InnerText.Should().Be("Item 012");
        }

        [TestMethod]
        public void HtmlCellTest_ByTagInstance()
        {
            HtmlCell cell = new HtmlCell(BrowserWindow);
            cell.SearchProperties.Add(HtmlCell.PropertyNames.Instance, "4");

            cell.InnerText.Should().Be("Item 012");
        }
    }
}
