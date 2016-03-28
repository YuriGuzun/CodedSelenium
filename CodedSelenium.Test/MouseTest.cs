using CodedSelenium.Test.ObjectMap;
using FluentAssertions;
using NUnit.Framework;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Input;

namespace CodedSelenium.Test
{
    [TestFixture]
    public class MouseTest : BasicTest
    {
        private MouseTestPage _mouseTestPage;

        private MouseTestPage MouseTestPage
        {
            get
            {
                if (_mouseTestPage == null)
                {
                    _mouseTestPage = new MouseTestPage(BasicTest.BrowserWindow);
                }

                _mouseTestPage.Launch();
                return _mouseTestPage;
            }
        }

        [Test]
        public void MouseTest_Click_NoControl_NoParameters()
        {
            Mouse.Move(MouseTestPage.FirstDiv, new Point(5, 7));
            Mouse.Click();
            AssertClick(MouseTestPage.FirstDiv, 5, 7, MouseClickDiv.MouseAction.Click, MouseButtons.Left, ModifierKeys.None);
        }

        [Test]
        public void MouseTest_Click_NoControl_ModifierKeys()
        {
            Mouse.Move(MouseTestPage.FirstDiv, new Point(5, 7));
            Mouse.Click(ModifierKeys.Alt);
            AssertClick(MouseTestPage.FirstDiv, 5, 7, MouseClickDiv.MouseAction.Click, MouseButtons.Left, ModifierKeys.Alt);
        }

        [Test]
        public void MouseTest_Click_NoControl_MouseButton()
        {
            Mouse.Move(MouseTestPage.FirstDiv, new Point(5, 7));
            Mouse.Click(MouseButtons.Right);
            AssertClick(MouseTestPage.FirstDiv, 5, 7, MouseClickDiv.MouseAction.MouseUp, MouseButtons.Right, ModifierKeys.None);
        }

        [Test]
        public void MouseTest_Click_NoControl_MouseButton_ModifierKeys()
        {
            Mouse.Move(MouseTestPage.FirstDiv, new Point(5, 7));
            Mouse.Click(MouseButtons.Right, ModifierKeys.Shift);
            AssertClick(MouseTestPage.FirstDiv, 5, 7, MouseClickDiv.MouseAction.MouseUp, MouseButtons.Right, ModifierKeys.Shift);
        }

        [Test]
        public void MouseTest_Click_NoControl_Point()
        {
            Point pointToClick = MouseTestPage.FirstDiv.BoundingRectangle.Location;
            pointToClick.X += 5;
            pointToClick.Y += 7;
            Mouse.Click(pointToClick);
            AssertClick(MouseTestPage.FirstDiv, 5, 7, MouseClickDiv.MouseAction.Click, MouseButtons.Left, ModifierKeys.None);
        }

        [Test]
        public void MouseTest_Click_Simple()
        {
            Mouse.Click(MouseTestPage.FirstDiv);
            AssertClick(MouseTestPage.FirstDiv, 113, 106, MouseClickDiv.MouseAction.Click, MouseButtons.Left, ModifierKeys.None);
        }

        [TestCase(10, 15, MouseClickDiv.MouseAction.Click, MouseButtons.Left, ModifierKeys.Control)]
        [TestCase(20, 25, MouseClickDiv.MouseAction.MouseUp, MouseButtons.Right, ModifierKeys.Control)]
        [TestCase(30, 35, MouseClickDiv.MouseAction.Click, MouseButtons.Left, ModifierKeys.Alt)]
        [TestCase(40, 45, MouseClickDiv.MouseAction.Click, MouseButtons.Left, ModifierKeys.Shift)]
        [Test]
        public void MouseTest_Click(
            int x, int y, MouseClickDiv.MouseAction action, MouseButtons mouseButton, ModifierKeys modifierKey)
        {
            Mouse.Click(MouseTestPage.FirstDiv, mouseButton, modifierKey, new Point(x, y));
            AssertClick(MouseTestPage.FirstDiv, x, y, action, mouseButton, modifierKey);
        }

        [TestCase(10, 15, ModifierKeys.Control)]
        [TestCase(20, 25, ModifierKeys.Alt)]
        [TestCase(30, 35, ModifierKeys.Shift)]
        [Test]
        public void MouseTest_DoubleClick(
            int x, int y, ModifierKeys modifierKey)
        {
            Mouse.DoubleClick(MouseTestPage.FirstDiv, modifierKey, new Point(x, y));
            AssertClick(MouseTestPage.FirstDiv, x, y, MouseClickDiv.MouseAction.DblClick, MouseButtons.Left, modifierKey);
        }

        [Test]
        public void MouseTest_DoubleClick_NoControl_NoParameters()
        {
            Mouse.Move(MouseTestPage.FirstDiv, new Point(5, 7));
            Mouse.DoubleClick();
            AssertClick(MouseTestPage.FirstDiv, 5, 7, MouseClickDiv.MouseAction.DblClick, MouseButtons.Left, ModifierKeys.None);
        }

        [Test]
        public void MouseTest_DoubleClick_NoControl_ModifierKeys()
        {
            Mouse.Move(MouseTestPage.FirstDiv, new Point(5, 7));
            Mouse.DoubleClick(ModifierKeys.Alt);
            AssertClick(MouseTestPage.FirstDiv, 5, 7, MouseClickDiv.MouseAction.DblClick, MouseButtons.Left, ModifierKeys.Alt);
        }

        [Test]
        public void MouseTest_DoubleClick_NoControl_Point()
        {
            Point pointToClick = MouseTestPage.FirstDiv.BoundingRectangle.Location;
            pointToClick.X += 5;
            pointToClick.Y += 7;
            Mouse.DoubleClick(pointToClick);
            AssertClick(MouseTestPage.FirstDiv, 5, 7, MouseClickDiv.MouseAction.DblClick, MouseButtons.Left, ModifierKeys.None);
        }

        private void AssertClick(
            MouseClickDiv div, int x, int y, MouseClickDiv.MouseAction action, MouseButtons mouseButton, ModifierKeys modifierKey)
        {
            Wait.Until((d) => { return div.X.Equals(x.ToString()); });
            div.X.Should().Be(x.ToString(), "X");
            div.Y.Should().Be(y.ToString(), "Y");
            div.Action.Should().Be(action, "Action");
            div.MouseButton.Should().Be(mouseButton, "MouseButton");
            div.ModifierKey.Should().Be(modifierKey, "ModifierKey");
            CleanLogs();
        }
    }
}
