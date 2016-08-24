using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace CodedSelenium
{
    public static class Keyboard
    {
        private static readonly List<string> _notSupportedKeys = new List<string>()
        {
            "BREAK",
            "CAPSLOCK",
            "NUMLOCK",
            "PRTSC",
            "SCROLLLOCK",
            "F13",
            "F14",
            "F15",
            "F16"
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
            SendKeys(text, ModifierKeys.None);
        }

        public static void SendKeys(string text, ModifierKeys modifierKeys)
        {
            SendKeys(text, null, modifierKeys);
        }

        public static void SendKeys(UITestControl control, string text)
        {
            SendKeys(text, control, ModifierKeys.None);
        }

        public static void SendKeys(UITestControl control, string text, ModifierKeys modifierKeys)
        {
            SendKeys(text, control, modifierKeys);
        }

        private static void SendKeys(string text, UITestControl control, ModifierKeys modifierKeys)
        {
            CheckForNonSupportedKeys(text);

            var driver = control == null ? BrowserWindow.ActiveBrowserWindow.Driver : (control.TopParent as BrowserWindow).Driver;
            string keyToPress = GetKeyToPress(modifierKeys);
            var actions = new Actions(driver);

            if (!string.IsNullOrEmpty(keyToPress))
                actions = actions.KeyDown(keyToPress);

            if (control == null)
                actions = actions.SendKeys(text.ReplaceKeys());
            else
                actions = actions.SendKeys(control.WebElement, text.ReplaceKeys());

            if (!string.IsNullOrEmpty(keyToPress))
                actions = actions.KeyUp(keyToPress);
            actions.Perform();
        }

        private static string GetKeyToPress(ModifierKeys modifierKeys)
        {
            switch (modifierKeys)
            {
                case ModifierKeys.Alt:
                    return Keys.Alt;

                case ModifierKeys.Control:
                    return Keys.Control;

                case ModifierKeys.Shift:
                    return Keys.Shift;

                case ModifierKeys.Windows:
                    throw new NotImplementedException("Following keys are not supported: ModifierKeys.Windows");
                default:
                    return string.Empty;
            }
        }

        private static string ReplaceKeys(this string inputString)
        {
            inputString = ReplaceControlAltShift(inputString);

            foreach (var item in _keysDictionary)
            {
                string pattern = item.Key;
                if (pattern.StartsWith("{"))
                    pattern = item.Key.Replace("{", "\\{").Replace("}", "\\}");
                else
                    pattern = "\\" + pattern;

                inputString = Regex.Replace(inputString, pattern, item.Value, RegexOptions.IgnoreCase);
            }

            return inputString;
        }

        private static void CheckForNonSupportedKeys(string inputString)
        {
            string pattern = "\\{" + string.Join("\\}|\\{", _notSupportedKeys) + "\\}";
            if (Regex.IsMatch(inputString, pattern, RegexOptions.IgnoreCase))
                throw new NotImplementedException(
                    "Following keys are not supported: " + string.Join(Environment.NewLine, _notSupportedKeys));
        }

        private static string ReplaceControlAltShift(string inputString)
        {
            string pattern = @"(\+|\%|\^)([^}]{1})";
            string replacedString = Regex.Replace(inputString, pattern, "$1$2$1", RegexOptions.IgnoreCase);
            return replacedString;
        }
    }
}
