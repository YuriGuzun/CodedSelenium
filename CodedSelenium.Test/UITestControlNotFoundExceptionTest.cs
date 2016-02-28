using CodedSelenium.HtmlControls;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CodedSelenium.Test
{
    [TestClass]
    public class UITestControlNotFoundExceptionTest : BasicTest
    {
        [TestMethod]
        public void UITestControlNotFoundExceptionTest_BasicCheck()
        {
            HtmlRow row = new HtmlRow(BrowserWindow);
            row.SearchProperties.Add(HtmlRow.PropertyNames.Id, "invalidId");
            row.SearchProperties.Add(HtmlRow.PropertyNames.InnerText, "invalidInnerText");
            row.FilterProperties.Add(HtmlRow.PropertyNames.Class, "invalidClass", PropertyExpressionOperator.Contains);
            row.Exists.Should().BeFalse();

            string expectedMessage =
                "Unable to find ui control control matching search criteria:\r\n" +
                "\tSearchProperties: TagName.EqualTo(tr) and id.EqualTo(invalidId) and InnerText.EqualTo(invalidInnerText)" +
                "\tFilterProperties: Class.Contains(invalidClass)";

            Action action = () => row.InnerText.ToString();

            action.ShouldThrow<UITestControlNotFoundException>()
                .WithMessage(expectedMessage);
        }
    }
}
