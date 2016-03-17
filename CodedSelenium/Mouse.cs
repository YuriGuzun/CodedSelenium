using CodedSelenium.HtmlControls;
using OpenQA.Selenium.Interactions;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Input;

namespace CodedSelenium
{
    public static class Mouse
    {
        public static void Click()
        {
            Actions action = new Actions(BrowserWindow.ActiveWebDriverInstances[0]);
            action.Click();
        }

        public static void Click(UITestControl control, MouseButtons button, ModifierKeys modifierKeys, Point relativeCoordinate)
        {
            control.Click(button, modifierKeys, relativeCoordinate);
        }

        public static void Click(UITestControl control)

        {
            control.Click();
        }

        public static void Click(HtmlDiv firstDiv, object mouseButtons)
        {
            throw new NotImplementedException();
        }
    }
}
