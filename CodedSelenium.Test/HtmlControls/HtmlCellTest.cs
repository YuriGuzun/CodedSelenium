using CodedSelenium.HtmlControls;
using FluentAssertions;
using NUnit.Framework;

namespace CodedSelenium.Test.HtmlControls
{
    [TestFixture]
    public class HtmlCellTest : BasicTest
    {
        [Test]
        public void HtmlCellTest_ById()
        {
            HtmlCell cell = new HtmlCell(BasicTestPage);
            cell.SearchProperties.Add(HtmlCell.PropertyNames.Id, "cellWithId");

            cell.InnerText.Should().Be("Item 113");
        }

        [Test]
        public void HtmlCellTest_ByInnerText()
        {
            HtmlCell cell = new HtmlCell(BasicTestPage);
            cell.SearchProperties.Add(HtmlCell.PropertyNames.InnerText, "113", PropertyExpressionOperator.Contains);

            cell.Id.Should().Be("cellWithId");
        }

        [Test]
        public void HtmlCellTest_ByColumnIndex()
        {
            HtmlCell cell = new HtmlCell(BasicTestPage);
            cell.SearchProperties.Add(HtmlCell.PropertyNames.ColumnIndex, "2");

            cell.InnerText.Should().Be("Item 011");
        }

        [Test]
        public void HtmlCellTest_ByRowIndex()
        {
            HtmlCell cell = new HtmlCell(BasicTestPage);
            cell.SearchProperties.Add(HtmlCell.PropertyNames.RowIndex, "2");

            cell.InnerText.Should().Be("Item 001");
        }

        [Test]
        public void HtmlCellTest_ByRowAndColumnIndex()
        {
            HtmlCell cell = new HtmlCell(BasicTestPage);
            cell.SearchProperties.Add(HtmlCell.PropertyNames.ColumnIndex, "2");
            cell.SearchProperties.Add(HtmlCell.PropertyNames.RowIndex, "2");

            cell.InnerText.Should().Be("Item 011");
        }

        [Test]
        public void HtmlCellTest_ByInstance()
        {
            HtmlCell cell = new HtmlCell(BasicTestPage);
            cell.SearchProperties.Add(HtmlCell.PropertyNames.Instance, "4");

            cell.InnerText.Should().Be("Item 012");
        }

        [Test]
        public void HtmlCellTest_ByTagInstance()
        {
            HtmlRow row = new HtmlRow(BasicTestPage);
            HtmlCell cell = new HtmlCell(row);
            cell.SearchProperties.Add(HtmlCell.PropertyNames.TagInstance, "4");

            cell.InnerText.Should().Be("Item 012");
        }
    }
}
