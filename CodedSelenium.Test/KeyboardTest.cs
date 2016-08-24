using CodedSelenium.Test.ObjectMap;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Windows.Input;

namespace CodedSelenium.Test
{
    [TestFixture]
    public class KeyboardTest : BasicTest
    {
        [TestCase("", "", false)]
        [TestCase("Simple{home}Text", "TextSimple", false)]
        [TestCase("Text^a+simpl+e", "SimplE", false)]
        [TestCase("Simple{enter}Text", "Simple\r\nText", false)]
        [TestCase("Simple^a{DEL}", "", false)]
        [TestCase("", "", true)]
        [TestCase("Simple{home}Text", "TextSimple", true)]
        [TestCase("Text^a+simpl+e", "SimplE", true)]
        [TestCase("Simple{enter}Text", "Simple\r\nText", true)]
        [TestCase("Simple^a{DEL}", "", true)]
        [Test]
        public void KeyboardTest_TextOnly(string inputString, string expectedText, bool withControl)
        {
            expectedText.Replace("\r\n", Environment.NewLine);

            if (withControl)
                Keyboard.SendKeys(BasicTestPage.TextArea, inputString);
            else
            {
                Mouse.Click(BasicTestPage.TextArea);
                Keyboard.SendKeys(inputString);
            }

            BasicTestPage.TextArea.Text.Should().Be(expectedText, "text entered previously was {0}", expectedText);
        }

        [Test]
        public void KeyboardTest_NoControl_TextOnly_NotSupportedKeys()
        {
            Action action = () => Keyboard.SendKeys("{BREAK}");
            action.ShouldThrow<NotImplementedException>();
        }

        [Test]
        public void KeyboardTest_NoControl_WithModifiers_Control()
        {
            Mouse.Click(BasicTestPage.TextArea);
            Keyboard.SendKeys("Hello");
            Keyboard.SendKeys("a", ModifierKeys.Control);
            Keyboard.SendKeys("World");

            BasicTestPage.TextArea.Text.Should().Be("World");
        }

        [Test]
        public void KeyboardTest_NoControl_WithModifiers_Shift()
        {
            string inputString = "hello";
            Mouse.Click(BasicTestPage.TextArea);
            Keyboard.SendKeys(inputString, ModifierKeys.Shift);

            BasicTestPage.TextArea.Text.Should().Be(inputString.ToUpper());
        }

        [Test]
        public void KeyboardTest_NoControl_WithModifiers_Windows()
        {
            Action action = () => Keyboard.SendKeys(string.Empty, ModifierKeys.Windows);
            action.ShouldThrow<NotImplementedException>();
        }

        [Test]
        public void KeyboardTest_NoControl_WithModifiers_None()
        {
            string inputString = "hello";
            Mouse.Click(BasicTestPage.TextArea);
            Keyboard.SendKeys(inputString, ModifierKeys.None);

            BasicTestPage.TextArea.Text.Should().Be(inputString);
        }

        [Test]
        public void KeyboardTest_WithControl_WithModifiers_Control()
        {
            Mouse.Click(BasicTestPage.TextArea);
            Keyboard.SendKeys("Hello");
            Keyboard.SendKeys(BasicTestPage.TextArea, "a", ModifierKeys.Control);
            Keyboard.SendKeys("World");

            BasicTestPage.TextArea.Text.Should().Be("World");
        }

        [Test]
        public void KeyboardTest_WithControl_WithModifiers_Shift()
        {
            string inputString = "hello";
            Keyboard.SendKeys(BasicTestPage.TextArea, inputString, ModifierKeys.Shift);

            BasicTestPage.TextArea.Text.Should().Be(inputString.ToUpper());
        }

        [Test]
        public void KeyboardTest_WithControl_WithModifiers_None()
        {
            string inputString = "hello";
            Keyboard.SendKeys(BasicTestPage.TextArea, inputString, ModifierKeys.None);

            BasicTestPage.TextArea.Text.Should().Be(inputString);
        }

        [SetUp]
        public void Setup()
        {
            string script =
                "jQuery('#textArea').each(function(index) {" +
                "   $( this ).val('')" +
                "});";
            BrowserWindow.ExecuteScript(script);
            BasicTestPage.TextArea.Text.Should().BeNullOrEmpty("there shouldn't be any text in that text area");
        }
    }
}
