using CodedSelenium.Extension;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.Drawing.Imaging;

namespace CodedSelenium
{
    public class BrowserWindow : UITestControl, IDisposable
    {
        private bool disposed = false;

        public BrowserWindow()
        {
        }

        public BrowserWindow(EventFiringWebDriver driver)
        {
            this.Parent = driver;
            this.Driver = driver;

            this.Driver.ExceptionThrown += this.TakeScreenshotOnException;
        }

        ~BrowserWindow()
        {
            this.Dispose(true);
        }

        public static string CurrentBrowser { get; set; }

        public EventFiringWebDriver Driver { get; private set; }

        public virtual Uri Uri
        {
            get
            {
                return new Uri(this.Driver.Url);
            }
        }

        protected override IWebElement WebElement
        {
            get
            {
                return null;
            }
        }

        public static BrowserWindow Launch(string uri)
        {
            EventFiringWebDriver driver = null;

            switch (CurrentBrowser)
            {
                case "Firefox":
                    driver = new EventFiringWebDriver(new FirefoxDriver());
                    break;

                case "IE":
                    driver = new EventFiringWebDriver(new InternetExplorerDriver());
                    break;

                default:
                    driver = new EventFiringWebDriver(new ChromeDriver());
                    break;
            }

            BrowserWindow browserWindow = new BrowserWindow(driver);
            browserWindow.NavigateToUrl(uri);

            return browserWindow;
        }

        public static void ClearCache()
        {
            throw new NotImplementedException("Please see non static analogue");
        }

        public static void ClearCookies()
        {
            throw new NotImplementedException("Please see non static analogue");
        }

        public static BrowserWindow Launch(Uri uri)
        {
            return BrowserWindow.Launch(uri.ToString());
        }

        public virtual object ExecuteScript(string script, params object[] args)
        {
            IJavaScriptExecutor js = this.Driver as IJavaScriptExecutor;
            return (string)js.ExecuteScript(string.Format(script, args));
        }

        public void ClearCoockies()
        {
            this.Driver.Manage().Cookies.DeleteAllCookies();
        }

        public void NavigateToUrl(string uri)
        {
            this.Driver.Navigate().GoToUrl(uri);
        }

        public void NavigateToUrl(Uri uri)
        {
            this.NavigateToUrl(uri.ToString());
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Close()
        {
            this.Driver.Quit();
        }

        public void Refresh()
        {
            this.Driver.Navigate().Refresh();
        }

        public virtual void Forward()
        {
            this.Driver.Navigate().Forward();
        }

        public virtual void Back()
        {
            this.Driver.Navigate().Back();
        }

        public virtual void PerformDialogAction(BrowserDialogAction actionType)
        {
            IAlert alert = this.Driver.SwitchTo().Alert();

            switch (actionType)
            {
                case BrowserDialogAction.Ok:
                case BrowserDialogAction.Yes:
                    alert.Accept();
                    break;

                case BrowserDialogAction.Cancel:
                case BrowserDialogAction.Ignore:
                case BrowserDialogAction.Close:
                case BrowserDialogAction.No:
                    alert.Dismiss();
                    break;

                default:
                    throw new NotImplementedException(string.Format("'{0}' action type is not implemented", actionType.ToString()));
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (this.Driver != null)
                    {
                        this.Close();
                    }

                    this.disposed = true;
                }
            }
        }

        private void TakeScreenshotOnException(object sender, WebDriverExceptionEventArgs e)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd-hhmm-ss");
            this.Driver.TakeScreenshot().SaveAsFile("Exception-" + timestamp + ".png", ImageFormat.Png);
        }
    }
}
