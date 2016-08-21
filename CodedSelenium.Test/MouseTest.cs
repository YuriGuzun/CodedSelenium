using CodedSelenium.HtmlControls;
using CodedSelenium.Test.ObjectMap;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Input;
using MouseAction = CodedSelenium.Test.ObjectMap.MouseClickDiv.MouseAction;

namespace CodedSelenium.Test
{
    [TestFixture]
    public class MouseTest : BasicTest
    {
        private readonly Point _defaultPoint = new Point(151, 151);
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

        [Test]
        public void MouseTest_DoubleClick_Simple()
        {
            Mouse.DoubleClick(TestPage.FirstDiv);
            AssertClick(TestPage.FirstDiv, _defaultPoint.X, _defaultPoint.Y, MouseAction.DoubleClick, MouseButtons.Left, ModifierKeys.None);
        }

        [TestCase(ModifierKeys.Control)]
        [TestCase(ModifierKeys.Alt)]
        [TestCase(ModifierKeys.Shift)]
        [Test]
        public void MouseTest_DoubleClick_ModifierKeys(ModifierKeys modifierKey)
        {
            Mouse.DoubleClick(TestPage.FirstDiv, modifierKey);
            AssertClick(TestPage.FirstDiv, _defaultPoint.X, _defaultPoint.Y, MouseAction.DoubleClick, MouseButtons.Left, modifierKey);
        }

        [TestCase(10, 15, ModifierKeys.Control)]
        [TestCase(20, 25, ModifierKeys.Alt)]
        [TestCase(30, 35, ModifierKeys.Shift)]
        [Test]
        public void MouseTest_DoubleClick_AllParams(
            int x, int y, ModifierKeys modifierKey)
        {
            Mouse.DoubleClick(TestPage.FirstDiv, modifierKey, new Point(x, y));
            AssertClick(TestPage.FirstDiv, x, y, MouseAction.DoubleClick, MouseButtons.Left, modifierKey);
        }

        [TestCase(10, 10)]
        [TestCase(0, 0)]
        [TestCase(200, 200)]
        [Test]
        public void MouseTest_DoubleClick_Point(int x, int y)
        {
            Mouse.DoubleClick(TestPage.FirstDiv, new Point(x, y));
            AssertClick(TestPage.FirstDiv, x, y, MouseAction.DoubleClick, MouseButtons.Left, ModifierKeys.None);
        }

        [Test]
        public void MouseTest_Hover()
        {
            Mouse.Hover(TestPage.FirstDiv);
            AssertClick(TestPage.FirstDiv, _defaultPoint.X, _defaultPoint.Y, MouseAction.Move, MouseButtons.Left, ModifierKeys.None);
        }

        [TestCase(10, 10)]
        [TestCase(0, 0)]
        [TestCase(200, 200)]
        [Test]
        public void MouseTest_Hover_Point(int x, int y)
        {
            Mouse.Hover(TestPage.FirstDiv, new Point(x, y));
            AssertClick(TestPage.FirstDiv, x, y, MouseAction.Move, MouseButtons.Left, ModifierKeys.None);
        }

        [TestCase(-1, -1)]
        [Test]
        public void MouseTest_Hover_Point_Invalid(int x, int y)
        {
            Action action = () => Mouse.Hover(TestPage.FirstDiv, new Point(x, y));
            action.ShouldThrow<ArgumentOutOfRangeException>("relativeCoordinate has negative values");
            CleanLogs();
        }

        [Test]
        public void MouseTest_Hover_Point_Timeout()
        {
            Point point = new Point(10, 10);
            int waitTimemilliseconds = 2000;
            TestPage.FirstDiv.Find();

            DateTime stopTime = DateTime.Now.AddMilliseconds(waitTimemilliseconds);
            Mouse.Hover(TestPage.FirstDiv, point, waitTimemilliseconds);
            DateTime.Now.Should().BeCloseTo(stopTime, 100);
            AssertClick(TestPage.FirstDiv, point.X, point.Y, MouseAction.Move, MouseButtons.Left, ModifierKeys.None);
        }

        [Test]
        public void MouseTest_Hover_Point_Timeout_Invalid()
        {
            TestPage.FirstDiv.Find();

            Action action = () => Mouse.Hover(TestPage.FirstDiv, new Point(10, 10), -1000);
            action.ShouldThrow<ArgumentOutOfRangeException>("relativeCoordinate has negative values");
        }

        [TestCase(-1, -1)]
        [Test]
        public void MouseTest_DoubleClick_Point_Invalid(int x, int y)
        {
            Action action = () => Mouse.DoubleClick(TestPage.FirstDiv, new Point(x, y));
            action.ShouldThrow<ArgumentOutOfRangeException>("relativeCoordinate has negative values");
            CleanLogs();
        }

        [TestCase(10, 10)]
        [TestCase(0, 0)]
        [TestCase(200, 200)]
        [Test]
        public void MouseTest_Move_Point(int x, int y)
        {
            Mouse.Move(TestPage.FirstDiv, new Point(x, y));
            AssertClick(TestPage.FirstDiv, x, y, MouseAction.Move, MouseButtons.Left, ModifierKeys.None);
        }

        [TestCase(-1, -1)]
        [Test]
        public void MouseTest_Move_Point_Invalid(int x, int y)
        {
            Action action = () => Mouse.DoubleClick(TestPage.FirstDiv, new Point(x, y));
            action.ShouldThrow<ArgumentOutOfRangeException>("relativeCoordinate has negative values");
            CleanLogs();
        }

        [Test]
        public void MouseTest_Move()
        {
            Mouse.Move(TestPage.FirstDiv);
            AssertClick(TestPage.FirstDiv, _defaultPoint.X, _defaultPoint.Y, MouseAction.Move, MouseButtons.Left, ModifierKeys.None);
        }

        [TestCase(0, -1, 0)]
        [TestCase(0, 0, 0)]
        [TestCase(0, 1, 100)]
        [TestCase(100, -1, 0)]
        [Test]
        public void MouseTest_MoveScrollWheel_NoControl(int initialPosition, int wheelMoveCount, int expectedLocation)
        {
            TestPage.Launch();
            BrowserWindow.ScrollWheelPosition = initialPosition;
            Mouse.MoveScrollWheel(wheelMoveCount);
            BrowserWindow.ScrollWheelPosition.Should().Be(expectedLocation);
        }

        [TestCase(0, -1, 0)]
        [TestCase(0, 0, 0)]
        [TestCase(0, 1, 100)]
        [TestCase(100, -1, 0)]
        [Test]
        public void MouseTest_MoveScrollWheel_Control(int initialPosition, int wheelMoveCount, int expectedLocation)
        {
            TestPage.TextArea.ScrollWheelPosition = initialPosition;
            Mouse.MoveScrollWheel(TestPage.TextArea, wheelMoveCount);
            TestPage.TextArea.ScrollWheelPosition.Should().Be(expectedLocation);
        }

        [TestCase(1, 1)]
        [TestCase(1, 2)]
        [TestCase(3, 3)]
        [Test]
        public void MouseTest_DraggAndDrop_TestControl(int cellColumnIndex, int cellRowIndex)
        {
            BrowserWindow.Refresh();
            var cell = new HtmlCell(TestPage.Table);
            cell.SearchProperties.Add(HtmlCell.PropertyNames.ColumnIndex, cellColumnIndex.ToString());
            cell.SearchProperties.Add(HtmlCell.PropertyNames.RowIndex, cellRowIndex.ToString());

            Mouse.StartDragging(TestPage.DivToDrag);
            Mouse.StopDragging(cell);

            new HtmlDiv(cell).Exists
                .Should().BeTrue("div should have been moved to {0},{1} cell position", cellColumnIndex, cellRowIndex);
        }

        // Cell size is 50px
        [TestCase(-125, -125, 1, 1)]
        [TestCase(-50, 0, 2, 3)]
        [Test]
        public void MouseTest_DraggAndDrop_Coordinates(int moveByX, int moveByY, int cellColumnIndex, int cellRowIndex)
        {
            BrowserWindow.Refresh();
            var cell = new HtmlCell(TestPage.Table);
            cell.SearchProperties.Add(HtmlCell.PropertyNames.ColumnIndex, cellColumnIndex.ToString());
            cell.SearchProperties.Add(HtmlCell.PropertyNames.RowIndex, cellRowIndex.ToString());

            Mouse.StartDragging(TestPage.DivToDrag);
            Mouse.StopDragging(moveByX, moveByY);

            new HtmlDiv(cell).Exists
                .Should().BeTrue("div should have been moved to {0},{1} cell position", cellColumnIndex, cellRowIndex);
        }

        [Test]
        public void MouseTest_DraggAndDrop_TestControlAndPoint()
        {
            BrowserWindow.Refresh();
            var parentCell = new HtmlCell(TestPage.Table);
            parentCell.SearchProperties.Add(HtmlCell.PropertyNames.ColumnIndex, "1");
            parentCell.SearchProperties.Add(HtmlCell.PropertyNames.RowIndex, "1");

            var targetCell = new HtmlCell(TestPage.Table);
            targetCell.SearchProperties.Add(HtmlCell.PropertyNames.ColumnIndex, "3");
            targetCell.SearchProperties.Add(HtmlCell.PropertyNames.RowIndex, "2");

            Mouse.StartDragging(TestPage.DivToDrag);
            Mouse.StopDragging(parentCell, new Point(125, 75));

            new HtmlDiv(targetCell).Exists
                .Should().BeTrue("div should have been moved to {0},{1} cell position", 2, 1);
        }
        
        [Test]
        public void MouseTest_NotImplemented()
        {
            List<Action> actions = new List<Action>()
            {
                () => Mouse.MoveScrollWheel(1, ModifierKeys.Alt),
                () => Mouse.MoveScrollWheel(BrowserWindow, 1, ModifierKeys.Alt),
                () => Mouse.StopDragging(new Point())
            };

            foreach (var action in actions)
            {
                action.ShouldThrow<NotImplementedException>();
            }
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
