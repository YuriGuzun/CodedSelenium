using System.Linq;
using CodedSelenium.HtmlControls;
using FluentAssertions;
using NUnit.Framework;

namespace CodedSelenium.Test
{
    [TestFixture]
    public class UITestControlCollectionTest : BasicTest
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
        public void UITestControlCollection_CanSetElementByIndex()
        {
            UITestControlCollection siblings = CheckBox.GetParent().GetChildren();
            siblings.Should().NotBeNullOrEmpty("because the CheckBox should have siblings");

            var lastUiTestControl = siblings.Last();
            siblings.Add(new UITestControl());

            siblings[siblings.Count - 1] = lastUiTestControl;
            siblings
                .Last()
                .ShouldBeEquivalentTo(
                    lastUiTestControl,
                    "because we copied the second last element to be the last.");
        }

        [Test]
        public void UITestControlCollection_CanClearCollection()
        {
            UITestControlCollection siblings = CheckBox.GetParent().GetChildren();
            siblings.Should().NotBeNullOrEmpty("because the CheckBox should have siblings");

            siblings.Clear();
            siblings.Should().Empty("because we cleared the collection");
        }

        [Test]
        public void UITestControlCollection_Contains()
        {
            UITestControlCollection siblings = CheckBox.GetParent().GetChildren();
            siblings.Should().NotBeNullOrEmpty("because the CheckBox should have siblings");

            var firstItem = siblings.First();
            siblings
                .Contains(firstItem)
                .Should()
                .BeTrue("because collection should contain its first Item.");
        }

        [Test]
        public void UITestControlCollection_CanRemoveItem()
        {
            UITestControlCollection siblings = CheckBox.GetParent().GetChildren();
            siblings.Should().NotBeNullOrEmpty("because the CheckBox should have siblings");

            var countBeforeRemoving = siblings.Count;
            var lastItem = siblings.Last();
            siblings
                .Remove(lastItem)
                .Should()
                .BeTrue("because it should be possible to remove an Item.");

            siblings
                .Count
                .Should()
                .Be(countBeforeRemoving - 1, "because count should change.");

            siblings
                .Contains(lastItem)
                .Should()
                .BeFalse("because the item has been removed.");
        }

        [Test]
        public void UITestControlCollection_CanCopyToArray()
        {
            UITestControlCollection siblings = CheckBox.GetParent().GetChildren();
            siblings.Should().NotBeNullOrEmpty("because the CheckBox should have siblings");

            UITestControl[] copyTo = new UITestControl[siblings.Count];
            siblings.CopyTo(copyTo, copyTo.Length);

            copyTo
                .ShouldAllBeEquivalentTo(siblings, "because we copied the collection");
        }

        [Test]
        public void UITestControlCollection_CanAddRange()
        {
            UITestControlCollection siblings = CheckBox.GetParent().GetChildren();
            siblings.Should().NotBeNullOrEmpty("because the CheckBox should have siblings");

            var beforeCount = siblings.Count;
            var lastItem = siblings.Last();
            var toAdd = new UITestControlCollection();
            toAdd.Add(lastItem);
            toAdd.Add(lastItem);


            siblings
                .AddRange(toAdd);

            siblings
                .Count
                .Should()
                .Be(beforeCount + 2, "because we added 2 Items");
        }
    }
}