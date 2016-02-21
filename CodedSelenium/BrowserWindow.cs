using CodedSelenium.Extension;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;

namespace CodedSelenium
{
    public class BrowserWindow : UITestControl, IDisposable
    {
        private IWebDriver driver;
        private bool disposed = false;

        public BrowserWindow()
        {
        }

        public BrowserWindow(IWebDriver driver)
        {
            this.Parent = driver;
            this.driver = driver;
        }

        ~BrowserWindow()
        {
            this.Dispose(true);
        }

        public static string CurrentBrowser { get; set; }

        protected override IWebElement WebElement
        {
            get
            {
                return null;
            }
        }

        public static BrowserWindow Launch(string uri)
        {
            IWebDriver driver = null;

            switch (CurrentBrowser)
            {
                case "Firefox":
                    driver = new FirefoxDriver();
                    break;

                case "IE":
                    driver = new InternetExplorerDriver();
                    break;

                default:
                    driver = new ChromeDriver();
                    break;
            }

            BrowserWindow browserWindow = new BrowserWindow(driver);
            browserWindow.NavigateToUrl(uri);

            return browserWindow;
        }

        public static BrowserWindow Launch(Uri uri)
        {
            return BrowserWindow.Launch(uri.ToString());
        }

        public void NavigateToUrl(string uri)
        {
            this.driver.Navigate().GoToUrl(uri);
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
            this.driver.Quit();
        }

        public void Refresh()
        {
            this.driver.Navigate().Refresh();
        }

        public virtual void PerformDialogAction(BrowserDialogAction actionType)
        {
            IAlert alert = this.driver.SwitchTo().Alert();

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
                    if (this.driver != null)
                    {
                        this.Close();
                    }

                    this.disposed = true;
                }
            }
        }
    }
}