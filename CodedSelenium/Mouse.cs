using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Input;

namespace CodedSelenium
{
    public static class Mouse
    {
        private static UITestControl _controlToDrag;

        public static UITestControl ControlToDrag
        {
            get
            {
                var copy = _controlToDrag;
                _controlToDrag = null;
                return copy;
            }

            set
            {
                if (_controlToDrag != null)
                {
                    _controlToDrag = null;
                    throw new InvalidOperationException("Mouse.StopDragging should follow after Mouse.StartDragging");
                }

                _controlToDrag = value;
            }
        }

        public static void Click(UITestControl control)
        {
            control.Click(MouseButtons.Left, ModifierKeys.None, null);
        }

        public static void Click(UITestControl control, ModifierKeys modifierKeys)
        {
            control.Click(MouseButtons.Left, modifierKeys, null);
        }

        public static void Click(UITestControl control, MouseButtons button)
        {
            control.Click(button, ModifierKeys.None, null);
        }

        public static void Click(UITestControl control, MouseButtons button, ModifierKeys modifierKeys)
        {
            control.Click(button, modifierKeys, null);
        }

        public static void Click(UITestControl control, MouseButtons button, ModifierKeys modifierKeys, Point relativeCoordinate)
        {
            control.Click(button, modifierKeys, relativeCoordinate);
        }

        public static void Click(UITestControl control, Point screenCoordinate)
        {
            control.Click(MouseButtons.Left, ModifierKeys.None, screenCoordinate);
        }

        public static void DoubleClick(UITestControl control)
        {
            control.DoubleClick(ModifierKeys.None, null);
        }

        public static void DoubleClick(UITestControl control, ModifierKeys modifierKeys)
        {
            control.DoubleClick(modifierKeys, null);
        }

        public static void DoubleClick(UITestControl control, ModifierKeys modifierKeys, Point relativeCoordinate)
        {
            control.DoubleClick(modifierKeys, relativeCoordinate);
        }

        public static void DoubleClick(UITestControl control, Point relativeCoordinate)
        {
            control.DoubleClick(ModifierKeys.None, relativeCoordinate);
        }

        public static void Hover(UITestControl control)
        {
            Move(control);
        }

        public static void Hover(UITestControl control, Point relativeCoordinate)
        {
            Move(control, relativeCoordinate);
        }

        public static void Hover(UITestControl control, Point relativeCoordinate, int millisecondDuration)
        {
            if (millisecondDuration < 0)
                throw new ArgumentOutOfRangeException("millisecondDuration", millisecondDuration.ToString());

            Hover(control, relativeCoordinate);
            Playback.Wait(millisecondDuration);
        }

        public static void Move(UITestControl control, Point relativeCoordinate)
        {
            control.MoveToElement(relativeCoordinate);
        }

        public static void Move(UITestControl control)
        {
            control.MoveToElement(null);
        }

        public static void MoveScrollWheel(int wheelMoveCount)
        {
            MoveScrollWheel(BrowserWindow.ActiveBrowserWindow, wheelMoveCount);
        }

        public static void MoveScrollWheel(int wheelMoveCount, ModifierKeys modifierKeys)
        {
            throw new NotImplementedException();
        }

        public static void MoveScrollWheel(UITestControl control, int wheelMoveCount)
        {
            long currentPosition = control.ScrollWheelPosition;
            control.ScrollWheelPosition = currentPosition + (wheelMoveCount * 100);
        }

        public static void MoveScrollWheel(UITestControl control, int wheelMoveCount, ModifierKeys modifierKeys)
        {
            throw new NotImplementedException();
        }

        public static void StartDragging(UITestControl control)
        {
            ControlToDrag = control;
        }

        public static void StopDragging(UITestControl control)
        {
            ControlToDrag.DragAndDropTo(control);
        }

        public static void StopDragging(int moveByX, int moveByY)
        {
            ControlToDrag.DragAndDropTo(moveByX, moveByY);
        }

        public static void StopDragging(Point pointToStop)
        {
            throw new NotImplementedException();
        }

        public static void StopDragging(UITestControl control, int moveByX, int moveByY)
        {
            int controlToDragX = _controlToDrag.BoundingRectangle.X + (_controlToDrag.BoundingRectangle.Width / 2);
            int controlToDragY = _controlToDrag.BoundingRectangle.Y + (_controlToDrag.BoundingRectangle.Height / 2);

            int destinationControlX = control.BoundingRectangle.X + (control.BoundingRectangle.Width / 2);
            int destinationControlY = control.BoundingRectangle.Y + (control.BoundingRectangle.Height / 2);

            int x = (-1 * (controlToDragX - destinationControlX)) + moveByX;
            int y = (-1 * (controlToDragY - destinationControlY)) + moveByY;
            ControlToDrag.DragAndDropTo(x, y);
        }

        public static void StopDragging(UITestControl control, Point relativeCoordinate)
        {
            StopDragging(control, relativeCoordinate.X, relativeCoordinate.Y);
        }
    }
}
