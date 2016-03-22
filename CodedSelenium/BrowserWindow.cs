using CodedSelenium.Extension;
using CodedSelenium.HtmlControls;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

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
            ParentSearchContext = driver;
            Driver = driver;

            Driver.ExceptionThrown += TakeScreenshotOnException;
        }

        ~BrowserWindow()
        {
            Dispose(true);
        }

        public static string CurrentBrowser { get; set; }

        public EventFiringWebDriver Driver { get; private set; }

        public virtual Uri Uri
        {
            get
            {
                return new Uri(Driver.Url);
            }
        }

        public override UITestControl TopParent
        {
            get
            {
                return this;
            }
        }

        protected override IWebElement WebElement
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// BrowserWindow class sets this property on launch. It is required to mimic some coded ui features that
        /// does not require context to be passed. For example Mouse.Click();
        /// </summary>
        public static List<BrowserWindow> ActiveBrowserWindowInstances
        {
            get; private set;
        }

        public static BrowserWindow ActiveBrowserWindow
        {
            get
            {
                //// TODO: Think how to identify active window
                return BrowserWindow.ActiveBrowserWindowInstances[0];
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

            if (ActiveBrowserWindowInstances == null)
            {
                ActiveBrowserWindowInstances = new List<BrowserWindow>();
            }
            
            BrowserWindow browserWindow = new BrowserWindow(driver);
            browserWindow.NavigateToUrl(uri);

            ActiveBrowserWindowInstances.Add(browserWindow);

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
            IJavaScriptExecutor js = Driver as IJavaScriptExecutor;
            return (string)js.ExecuteScript(script, args);
        }

        public void ClearCoockies()
        {
            Driver.Manage().Cookies.DeleteAllCookies();
        }

        public void NavigateToUrl(string uri)
        {
            Driver.Navigate().GoToUrl(uri);
        }

        public void NavigateToUrl(Uri uri)
        {
            NavigateToUrl(uri.ToString());
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Close()
        {
            BrowserWindow.ActiveBrowserWindowInstances.Remove(this);
            Driver.Quit();
        }

        public void Refresh()
        {
            Driver.Navigate().Refresh();
        }

        public virtual void Forward()
        {
            Driver.Navigate().Forward();
        }

        public virtual void Back()
        {
            Driver.Navigate().Back();
        }

        public virtual void PerformDialogAction(BrowserDialogAction actionType)
        {
            switch (actionType)
            {
                case BrowserDialogAction.Ok:
                case BrowserDialogAction.Yes:
                    Driver.SwitchTo().Alert().Accept();
                    break;

                case BrowserDialogAction.Cancel:
                case BrowserDialogAction.Ignore:
                case BrowserDialogAction.Close:
                case BrowserDialogAction.No:
                    Driver.SwitchTo().Alert().Dismiss();
                    break;

                default:
                    throw new NotImplementedException(string.Format("'{0}' action type is not implemented", actionType.ToString()));
            }
        }

        internal override void MoveToElement(Nullable<Point> relativeCoordinate)
        {
            if (relativeCoordinate.HasValue)
            {
                Cursor cursor = new Cursor(Cursor.Current.Handle);
                Cursor.Position = relativeCoordinate.Value;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (Driver != null)
                    {
                        Close();
                    }

                    disposed = true;
                }
            }
        }

        private void TakeScreenshotOnException(object sender, WebDriverExceptionEventArgs e)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd-hhmm-ss");
            Driver.TakeScreenshot().SaveAsFile("Exception-" + timestamp + ".png", ImageFormat.Png);
        }
    }
}
