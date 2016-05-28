using CodedSelenium.HtmlControls;
using FluentAssertions;
using NUnit.Framework;

namespace CodedSelenium.Test
{
    [TestFixture]
    public class UITestControlTest : BasicTest
    {
        private HtmlCheckBox _checkBox;

        private HtmlCheckBox CheckBox
        {
            get
            {
                if (_checkBox == null)
                {
                    _checkBox = new HtmlCheckBox(BasicTestPage);
                    _checkBox.SearchProperties.Add(HtmlCheckBox.PropertyNames.Id, "htmlControlTest_Properties");
                }

                return _checkBox;
            }
        }

        [Test]
        public void UITestControlTest_Properties_TagName()
        {
            CheckBox.TagName.Should().Be("input");
        }

        [Test]
        public void UITestControlTest_Properties_ValueAttribute()
        {
            CheckBox.ValueAttribute.Should().Be("buttonValue");
        }

        [Test]
        public void UITestControlTest_Properties_Type()
        {
            CheckBox.Type.Should().Be("checkbox");
        }

        [Test]
        public void UITestControlTest_Properties_Title()
        {
            CheckBox.Title.Should().Be("titleValue");
        }

        [Test]
        public void UITestControlTest_Properties_Id()
        {
            CheckBox.Id.Should().Be("htmlControlTest_Properties");
        }

        [Test]
        public void UITestControlTest_Properties_Class()
        {
            CheckBox.Class.Should().Be("simpleButtons");
        }

        [Test]
        public void UITestControlTest_Properties_HelpText()
        {
            CheckBox.HelpText.Should().Be("titleValue");
        }

        [Test]
        public void UITestControlTest_TryFind_ShouldBeTrue()
        {
            CheckBox
                .TryFind()
                .Should()
                .BeTrue("because the checkbox should exists, and TryFind should return true");
        }

        [Test]
        public void UITestControlTest_DrawHighlight_ShouldNotThrow()
        {
            CheckBox
                .Invoking(x => x.DrawHighlight())
                .ShouldNotThrow("because calling DrawHighlight should not throw.");
        }

        [Test]
        public void UITestControlTest_Find_ShouldNotThrow()
        {
            CheckBox
                .Invoking(x => x.Find())
                .ShouldNotThrow("because calling Find should not throw.");
        }
    }
}
