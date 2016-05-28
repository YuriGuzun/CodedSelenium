using CodedSelenium.Test.ObjectMap;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Input;
using MouseAction = CodedSelenium.Test.ObjectMap.MouseClickDiv.MouseAction;

namespace CodedSelenium.Test
{
    [TestFixture]
    public class MouseTest : BasicTest
    {
        private readonly Point _defaultPoint = new Point(113, 106);
        private MouseTestPage _mouseTestPage;

        private MouseTestPage TestPage
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
        public void MouseTest_Click_Simple()
        {
            Mouse.Click(TestPage.FirstDiv);
            AssertClick(TestPage.FirstDiv, _defaultPoint.X, _defaultPoint.Y, MouseAction.Click, MouseButtons.Left, ModifierKeys.None);
        }

        [TestCase(10, 15, MouseAction.Click, MouseButtons.Left, ModifierKeys.Control)]
        [TestCase(20, 25, MouseAction.MouseUp, MouseButtons.Right, ModifierKeys.Control)]
        [TestCase(30, 35, MouseAction.Click, MouseButtons.Left, ModifierKeys.Alt)]
        [TestCase(40, 45, MouseAction.Click, MouseButtons.Left, ModifierKeys.Shift)]
        [Test]
        public void MouseTest_Click_AllParams(
            int x, int y, MouseAction action, MouseButtons mouseButton, ModifierKeys modifierKey)
        {
            Mouse.Click(TestPage.FirstDiv, mouseButton, modifierKey, new Point(x, y));
            AssertClick(TestPage.FirstDiv, x, y, action, mouseButton, modifierKey);
        }

        [TestCase(ModifierKeys.Control)]
        [TestCase(ModifierKeys.Alt)]
        [TestCase(ModifierKeys.Shift)]
        [Test]
        public void MouseTest_Click_ModifierKeys(ModifierKeys modifierKey)
        {
            Mouse.Click(TestPage.FirstDiv, modifierKey);
            AssertClick(TestPage.FirstDiv, _defaultPoint.X, _defaultPoint.Y, MouseAction.Click, MouseButtons.Left, modifierKey);
        }

        [TestCase(MouseAction.Click, MouseButtons.Left)]
        [TestCase(MouseAction.MouseUp, MouseButtons.Right)]
        [Test]
        public void MouseTest_Click_MouseButtons(MouseAction action, MouseButtons mouseButton)
        {
            Mouse.Click(TestPage.FirstDiv, mouseButton);
            AssertClick(TestPage.FirstDiv, _defaultPoint.X, _defaultPoint.Y, action, mouseButton, ModifierKeys.None);
        }

        [TestCase(MouseAction.Click, MouseButtons.Left, ModifierKeys.Control)]
        [TestCase(MouseAction.MouseUp, MouseButtons.Right, ModifierKeys.Control)]
        [TestCase(MouseAction.Click, MouseButtons.Left, ModifierKeys.Shift)]
        [TestCase(MouseAction.MouseUp, MouseButtons.Right, ModifierKeys.Shift)]
        [TestCase(MouseAction.Click, MouseButtons.Left, ModifierKeys.Alt)]
        [TestCase(MouseAction.MouseUp, MouseButtons.Right, ModifierKeys.Alt)]
        [Test]
        public void MouseTest_Click_MouseButtonAndModifiers(MouseAction action, MouseButtons mouseButton, ModifierKeys modifierKey)
        {
            Mouse.Click(TestPage.FirstDiv, mouseButton, modifierKey);
            AssertClick(TestPage.FirstDiv, _defaultPoint.X, _defaultPoint.Y, action, mouseButton, modifierKey);
        }

        [TestCase(10, 10)]
        [TestCase(0, 0)]
        [TestCase(200, 200)]
        [Test]
        public void MouseTest_Click_Point(int x, int y)
        {
            Mouse.Click(TestPage.FirstDiv, new Point(x, y));
            AssertClick(TestPage.FirstDiv, x, y, MouseAction.Click, MouseButtons.Left, ModifierKeys.None);
        }

        [TestCase(-1, -1)]
        [Test]
        public void MouseTest_Click_Point_Invalid(int x, int y)
        {
            Action action = () => Mouse.Click(TestPage.FirstDiv, new Point(x, y));
            action.ShouldThrow<ArgumentOutOfRangeException>("relativeCoordinate has negative values");
            CleanLogs();
        }

        [TestCase(10, 15, ModifierKeys.Control)]
        [TestCase(20, 25, ModifierKeys.Alt)]
        [TestCase(30, 35, ModifierKeys.Shift)]
        [Test]
        public void MouseTest_DoubleClick(
            int x, int y, ModifierKeys modifierKey)
        {
            Mouse.DoubleClick(TestPage.FirstDiv, modifierKey, new Point(x, y));
            AssertClick(TestPage.FirstDiv, x, y, MouseAction.DblClick, MouseButtons.Left, modifierKey);
        }

        private void AssertClick(
            MouseClickDiv div, int x, int y, MouseAction action, MouseButtons mouseButton, ModifierKeys modifierKey)
        {
            Console.WriteLine("{0}, {1}", x, y);
            Wait.Until((d) => { return !string.IsNullOrEmpty(div.X); });
            div.X.Should().Be(x.ToString(), "X");
            div.Y.Should().Be(y.ToString(), "Y");
            div.Action.Should().Be(action, "Action");
            div.MouseButton.Should().Be(mouseButton, "MouseButton");
            div.ModifierKey.Should().Be(modifierKey, "ModifierKey");
            CleanLogs();
            Wait.Until((d) => { return string.IsNullOrEmpty(div.X); });
        }
    }
}
