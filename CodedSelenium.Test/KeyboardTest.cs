using CodedSelenium.Test.ObjectMap;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace CodedSelenium.Test
{
    [TestFixture]
    public class KeyboardTest : BasicTest
    {
        [TestCase("", "")]
        [TestCase("Simple{home}Text", "TextSimple")]
        [TestCase("Text^a+simpl+e", "SimplE")]
        [TestCase("Simple{enter}Text", "Text\r\nSimple")]
        [TestCase("Simple^a{DEL}", "")]
        [Test]
        public void KeyboardTest_NoControl_TextOnly(string inputString, string expectedText)
        {
            expectedText.Replace("\r\n", Environment.NewLine);

            Mouse.Click(BasicTestPage.TextArea);
            Keyboard.SendKeys(expectedText);

            BasicTestPage.TextArea.Text.Should().Be(expectedText, "text entered previously was {0}", expectedText);
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
