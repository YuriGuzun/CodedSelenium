using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Input;

namespace CodedSelenium
{
    public static class Mouse
    {
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
    }
}
