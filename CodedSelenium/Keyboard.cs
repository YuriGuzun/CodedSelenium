using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodedSelenium
{
    public static class Keyboard
    {
        private static readonly List<string> _notSupportedKeys = new List<string>()
        {
            "{BREAK}",
            "{CAPSLOCK}",
            "{NUMLOCK}",
            "{PRTSC}",
            "{SCROLLLOCK}",
            "{F13}",
            "{F14}",
            "{F15}",
            "{F16}"
        };

        private static readonly Dictionary<string, string> _keysDictionary = new Dictionary<string, string>()
        {
            { "~", Keys.Enter },
            { "+", Keys.Shift },
            { "^", Keys.Control },
            { "%", Keys.Alt },
            { "{+}", "+" },
            { "{^}", "^" },
            { "{%}", "%" },
            { "{{}", "{" },
            { "{}}", "}" },
            { "{BACKSPACE}", Keys.Backspace },
            { "{BS}", Keys.Backspace },
            { "{BKSP}", Keys.Backspace },
            { "{DELETE}", Keys.Delete },
            { "{DEL}", Keys.Delete },
            { "{DOWN}", Keys.Down },
            { "{END}", Keys.End },
            { "{ENTER}", Keys.Enter },
            { "{ESC}", Keys.Escape },
            { "{HELP}", Keys.Help },
            { "{HOME}", Keys.Home },
            { "{INSERT}", Keys.Insert },
            { "{INS}", Keys.Insert },
            { "{LEFT}", Keys.Left },
            { "{PGDN}", Keys.PageDown },
            { "{PGUP}", Keys.PageUp },
            { "{RIGHT}", Keys.Right },
            { "{TAB}", Keys.Tab },
            { "{UP}", Keys.Up },
            { "{F1}", Keys.F1 },
            { "{F2}", Keys.F2 },
            { "{F3}", Keys.F3 },
            { "{F4}", Keys.F4 },
            { "{F5}", Keys.F5 },
            { "{F6}", Keys.F6 },
            { "{F7}", Keys.F7 },
            { "{F8}", Keys.F8 },
            { "{F9}", Keys.F9 },
            { "{F10}", Keys.F10 },
            { "{F11}", Keys.F11 },
            { "{F12}", Keys.F12 },
            { "{ADD}", Keys.Add },
            { "{SUBTRACT}", Keys.Subtract },
            { "{MULTIPLY}", Keys.Multiply },
            { "{DIVIDE}", Keys.Divide }
        };

        public static void SendKeys(string text)
        {
            SendKeys(text, null);
        }

        private static void SendKeys(string text, UITestControl control)
        {
            var driver = control == null ? BrowserWindow.ActiveBrowserWindow.Driver : (control.TopParent as BrowserWindow).Driver;
            new Actions(driver).SendKeys(text.ReplaceKeys()).Perform();
        }

        private static string ReplaceKeys(this string inputString)
        {
            if (_notSupportedKeys.Any(item => inputString.Contains(item)))
                throw new InvalidOperationException(
                    "Following keys are not supported: " + string.Join(Environment.NewLine, _notSupportedKeys));

            var sb = new StringBuilder(inputString);

            foreach (var item in _keysDictionary)
                sb.Replace(item.Key, item.Value);

            return sb.ToString();
        }
    }
}
