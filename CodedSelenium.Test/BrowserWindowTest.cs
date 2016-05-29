using CodedSelenium.Extension;
using CodedSelenium.HtmlControls;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.IO;

namespace CodedSelenium.Test
{
    [TestFixture]
    public class BrowserWindowTest : BasicTest
    {
        public const string GoogleUrl = "https://www.google.com";
        public const string GitHubUrl = "https://github.com";

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
                        AssertResult("buttonWithAlert", "click", "OK!");
                        break;
                }
            }
        }

        [Test]
        public void BrowserWindowTest_CanConstructWithoutParameters()
        {
            BrowserWindow browserWindow = new BrowserWindow();
            browserWindow
                .Should()
                .NotBeNull("because the BrowserWindow should be able to be constructed without parameters.");
        }

        [Test]
        public void BrowserWindowTest_RefreshShouldNotThrow()
        {
            BrowserWindow
                .Invoking(x => x.Refresh())
                .ShouldNotThrow();
        }

        [Test]
        public void BrowserWindowTest_ForwardShouldNotThrow()
        {
            BrowserWindow
                .Invoking(x => x.Forward())
                .ShouldNotThrow();
        }

        [Test]
        public void BrowserWindowTest_BackShouldNotThrow()
        {
            BrowserWindow
                .Invoking(x => x.Back())
                .ShouldNotThrow();
        }

        [Test]
        public void BrowserWindowTest_RefreshShouldPersistTheCurrentPage()
        {
            BrowserWindow.NavigateToUrl(GoogleUrl);
            var currentUri = BrowserWindow.Uri;

            BrowserWindow.Refresh();

            BrowserWindow
                .Uri
                .ShouldBeEquivalentTo(currentUri, "because refreshing the page should not lead to another page.");
        }

        [Test]
        public void BrowserWindowTest_BackShouldLeadToPreviousUri()
        {
            BrowserWindow.NavigateToUrl(GoogleUrl);
            var currentUri = BrowserWindow.Uri;

            BrowserWindow.NavigateToUrl(GitHubUrl);
            BrowserWindow.Back();

            BrowserWindow
                .Uri
                .ShouldBeEquivalentTo(currentUri, "because back should return to the previous page.");
        }

        [Test]
        public void BrowserWindowTest_ForwardShouldUndoBack()
        {
            BrowserWindow.NavigateToUrl(GoogleUrl);

            BrowserWindow.NavigateToUrl(GitHubUrl);
            BrowserWindow
                .Uri
                .ShouldBeEquivalentTo(GitHubUrl, "because browser should have been redirected to '{0}'.", GitHubUrl);

            BrowserWindow.Back();
            BrowserWindow
                .Uri
                .ShouldBeEquivalentTo(GoogleUrl, "because back should redirect browser to '{0}'.", GoogleUrl);

            BrowserWindow.Forward();
            BrowserWindow
                .Uri
                .ShouldBeEquivalentTo(GitHubUrl, "because forward should undo back.");
        }

        [TearDown]
        public void Teardown()
        {
            BrowserWindow.NavigateToUrl(PathToPage);
        }
    }
}
