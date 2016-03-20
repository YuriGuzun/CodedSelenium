using CodedSelenium.HtmlControls;
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
        private MouseTestPage mouseTestPage;

        private MouseTestPage MouseTestPage
        {
            get
            {
                if (mouseTestPage == null)
                {
                    mouseTestPage = new MouseTestPage(BasicTest.BrowserWindow);
                }

                mouseTestPage.Launch();
                return mouseTestPage;
            }
        }

        [Test]
        public void MouseTest_Click_Simple()
        {
            Mouse.Click(MouseTestPage.FirstDiv);
            AssertClick(MouseTestPage.FirstDiv, 113, 106, MouseClickDiv.MouseAction.Click, MouseButtons.Left, ModifierKeys.None);
        }

        [TestCase(15, 20, MouseClickDiv.MouseAction.Click, MouseButtons.Left, ModifierKeys.Control)]
        [TestCase(15, 20, MouseClickDiv.MouseAction.MouseUp, MouseButtons.Right, ModifierKeys.Control)]
        [TestCase(15, 20, MouseClickDiv.MouseAction.Click, MouseButtons.Left, ModifierKeys.Alt)]
        [TestCase(15, 20, MouseClickDiv.MouseAction.Click, MouseButtons.Left, ModifierKeys.Shift)]
        public void MouseTest_Click(
            int x, int y, MouseClickDiv.MouseAction action, MouseButtons mouseButton, ModifierKeys modifierKey)
        {
            Mouse.Click(MouseTestPage.FirstDiv, mouseButton, modifierKey, new Point(x, y));
            AssertClick(MouseTestPage.FirstDiv, x, y, action, mouseButton, modifierKey);
        }

        private void AssertClick(
            MouseClickDiv div, int x, int y, MouseClickDiv.MouseAction action, MouseButtons mouseButton, ModifierKeys modifierKey)
        {
            div.X.Should().Be(x, "X");
            div.Y.Should().Be(y, "Y");
            div.Action.Should().Be(action, "Action");
            div.MouseButton.Should().Be(mouseButton, "MouseButton");
            div.ModifierKey.Should().Be(modifierKey, "ModifierKey");
        }
    }
}
