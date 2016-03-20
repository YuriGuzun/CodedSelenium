using CodedSelenium.HtmlControls;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace CodedSelenium.Test
{
    [TestFixture]
    public class UITestControlNotFoundExceptionTest : BasicTest
    {
        [Test]
        public void UITestControlNotFoundExceptionTest_BasicCheck()
        {
            HtmlRow row = new HtmlRow(BasicTestPage);
            row.SearchProperties.Add(HtmlRow.PropertyNames.Id, "invalidId");
            row.SearchProperties.Add(HtmlRow.PropertyNames.InnerText, "invalidInnerText");
            row.FilterProperties.Add(HtmlRow.PropertyNames.Class, "invalidClass", PropertyExpressionOperator.Contains);
            row.Exists.Should().BeFalse();

            string expectedMessage =
                "Unable to find ui control control matching search criteria:\r\n" +
                "\tSearchProperties: tagname.EqualTo(tr) and id.EqualTo(invalidId) and innertext.EqualTo(invalidInnerText)" +
                "\tFilterProperties: class.Contains(invalidClass)";

            Action action = () => row.InnerText.ToString();

            action.ShouldThrow<UITestControlNotFoundException>()
                .WithMessage(expectedMessage);
        }
    }
}
