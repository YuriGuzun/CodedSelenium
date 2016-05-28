using CodedSelenium.Test.ObjectMap;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Input;

namespace CodedSelenium.Test
{
    [TestFixture]
    public class MouseTest : BasicTest
    {
        private static int _divDimension = 200;
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
            AssertClick(TestPage.FirstDiv, 113, 106, MouseClickDiv.MouseAction.Click, MouseButtons.Left, ModifierKeys.None);
        }

        [TestCase(10, 15, MouseClickDiv.MouseAction.Click, MouseButtons.Left, ModifierKeys.Control)]
        [TestCase(20, 25, MouseClickDiv.MouseAction.MouseUp, MouseButtons.Right, ModifierKeys.Control)]
        [TestCase(30, 35, MouseClickDiv.MouseAction.Click, MouseButtons.Left, ModifierKeys.Alt)]
        [TestCase(40, 45, MouseClickDiv.MouseAction.Click, MouseButtons.Left, ModifierKeys.Shift)]
        [Test]
        public void MouseTest_Click(
            int x, int y, MouseClickDiv.MouseAction action, MouseButtons mouseButton, ModifierKeys modifierKey)
        {
            Mouse.Click(TestPage.FirstDiv, mouseButton, modifierKey, new Point(x, y));
            AssertClick(TestPage.FirstDiv, x, y, action, mouseButton, modifierKey);
        }

        [TestCase(10, 15, ModifierKeys.Control)]
        [TestCase(20, 25, ModifierKeys.Alt)]
        [TestCase(30, 35, ModifierKeys.Shift)]
        [Test]
        public void MouseTest_DoubleClick(
            int x, int y, ModifierKeys modifierKey)
        {
            Mouse.DoubleClick(TestPage.FirstDiv, modifierKey, new Point(x, y));
            AssertClick(TestPage.FirstDiv, x, y, MouseClickDiv.MouseAction.DblClick, MouseButtons.Left, modifierKey);
        }

        private static Point GetPoint()
        {
            _divDimension--;
            return new Point(_divDimension, _divDimension);
        }

        private void AssertClick(
            MouseClickDiv div, int x, int y, MouseClickDiv.MouseAction action, MouseButtons mouseButton, ModifierKeys modifierKey)
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
