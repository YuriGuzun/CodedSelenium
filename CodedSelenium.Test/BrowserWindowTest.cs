using CodedSelenium.Extension;
using CodedSelenium.HtmlControls;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace CodedSelenium.Test
{
    [TestFixture]
    public class BrowserWindowTest : BasicTest
    {
        [Test]
        public void BrowserWindowTest_Alert()
        {
            HtmlButton button = new HtmlButton(BasicTestPage);
            button.SearchProperties.Add(HtmlButton.PropertyNames.Id, "buttonWithAlert");

            foreach (BrowserDialogAction dialogAction in Enum.GetValues(typeof(BrowserDialogAction)))
            {
                Mouse.Click(button);
                Console.WriteLine(dialogAction.ToString());
                switch (dialogAction)
                {
                    case BrowserDialogAction.Ok:
                    case BrowserDialogAction.Yes:
                        BrowserWindow.PerformDialogAction(dialogAction);
                        AssertResult("buttonWithAlert", "click", "OK!");
                        break;

                    case BrowserDialogAction.Cancel:
                    case BrowserDialogAction.Ignore:
                    case BrowserDialogAction.Close:
                    case BrowserDialogAction.No:
                        BrowserWindow.PerformDialogAction(dialogAction);
                        AssertResult("buttonWithAlert", "click", "Cancel!");
                        break;

                    default:
                        Action action = () => BrowserWindow.PerformDialogAction(dialogAction);
                        action.ShouldThrow<NotImplementedException>()
                            .WithMessage(string.Format("'{0}' action type is not implemented", dialogAction.ToString()));
                        BrowserWindow.PerformDialogAction(BrowserDialogAction.Ok);
                        break;
                }
            }
        }
    }
}
