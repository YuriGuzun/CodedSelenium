using CodedSelenium.HtmlControls;
using System;
using System.Linq;

namespace CodedSelenium.Test.ObjectMap
{
    public abstract class Page : HtmlDocument
    {
        public Page(BrowserWindow browserWindow)
            : base(browserWindow)
        {
            BrowserWindow = browserWindow;
        }

        public abstract string Endpoint { get; }

        public BrowserWindow BrowserWindow { get; private set; }

        public void Launch(bool shouldForceLaunch = false)
        {
            if (shouldForceLaunch || !BrowserWindow.Uri.PathAndQuery.ToString().Contains(Endpoint))
            {
                UriBuilder builder = new UriBuilder(BrowserWindow.Uri);
                builder.Path = string.Join(string.Empty, builder.Uri.Segments.Take(builder.Uri.Segments.Count() - 1)) + Endpoint;
                BrowserWindow.NavigateToUrl(builder.Uri);
            }
        }
    }
}
