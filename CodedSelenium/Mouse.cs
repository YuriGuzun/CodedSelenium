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

        public static void Move(UITestControl control, Point relativeCoordinate)
        {
            control.MoveToElement(relativeCoordinate);
        }

        public static void Move(UITestControl control)
        {
            control.MoveToElement(null);
        }
    }
}
