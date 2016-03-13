using CodedSelenium.Extension;
using CodedSelenium.HtmlControls;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace CodedSelenium.Test
{
    [TestFixture]
    public class PropertyExpressionCollectionTest : BasicTest
    {
        [Test]
        public void PropertyExpressionCollectionTest_Add_Replace()
        {
            HtmlButton button = new HtmlButton(BasicTestPage);
            button.SearchProperties.Add(HtmlButton.PropertyNames.Id, "firstButton");
            button.SearchProperties.Add(HtmlButton.PropertyNames.Id, "secondButton");
            Mouse.Click(button);

            AssertResult("secondButton", "click");
        }

        [Test]
        public void PropertyExpressionCollectionTest_AddRange()
        {
            HtmlButton button = new HtmlButton(BasicTestPage);
            button.SearchProperties.Add(HtmlButton.PropertyNames.Id, "secondButton");
            HtmlButton secondButton = new HtmlButton(BasicTestPage);
            secondButton.SearchProperties.AddRange(button.SearchProperties);
            Mouse.Click(secondButton);

            AssertResult("secondButton", "click");
        }

        [Test]
        public void PropertyExpressionCollectionTest_Add_ArrayOfPairs()
        {
            HtmlButton button = new HtmlButton(BasicTestPage);
            button.SearchProperties.Add(HtmlButton.PropertyNames.Id, "secondButton", HtmlButton.PropertyNames.Class, "simpleButtons");
            Mouse.Click(button);

            AssertResult("secondButton", "click");
        }

        [Test]
        public void PropertyExpressionCollectionTest_Add_ArrayOfPairs_IncompletePair()
        {
            HtmlButton button = new HtmlButton();
            Action action = () => button.SearchProperties.Add(HtmlButton.PropertyNames.Id, "secondButton", HtmlButton.PropertyNames.Class);

            action.ShouldThrow<ArgumentOutOfRangeException>()
                .WithMessage("Specified argument was out of the range of valid values.\r\nParameter name: nameValuePairs");
        }

        [Test]
        public void PropertyExpressionCollectionTest_Remove()
        {
            HtmlButton button = new HtmlButton();
            button.SearchProperties.Add(HtmlButton.PropertyNames.Class, "class");
            button.SearchProperties.Add(HtmlButton.PropertyNames.Id, "secondButton");
            button.SearchProperties.Remove(new PropertyExpression(HtmlButton.PropertyNames.Id, "secondButton"));

            button.SearchProperties
                .Should().HaveCount(1)
                .And.OnlyContain(item => item.PropertyName.Equals(HtmlButton.PropertyNames.Class));
        }

        [Test]
        public void PropertyExpressionCollectionTest_MatchingSearchAndFilterProperty_Id()
        {
            HtmlButton button = new HtmlButton(BasicTestPage);
            button.SearchProperties.Add(HtmlButton.PropertyNames.Id, "Button", PropertyExpressionOperator.Contains);
            button.FilterProperties.Add(HtmlButton.PropertyNames.Id, "second", PropertyExpressionOperator.Contains);
            Mouse.Click(button);

            AssertResult("secondButton", "click");
        }

        [Test]
        public void PropertyExpressionCollectionTest_MatchingSearchAndFilterProperty_InnerText()
        {
            HtmlButton button = new HtmlButton(BasicTestPage);
            button.SearchProperties.Add(HtmlButton.PropertyNames.InnerText, "Button", PropertyExpressionOperator.Contains);
            button.FilterProperties.Add(HtmlButton.PropertyNames.InnerText, "Second", PropertyExpressionOperator.Contains);
            Mouse.Click(button);

            AssertResult("secondButton", "click");
        }
    }
}
