namespace CodedSelenium.Test.ObjectMap
{
    public class BasicTestPage : Page
    {
        public BasicTestPage(BrowserWindow browserWindow)
            : base(browserWindow)
        {
        }

        public override string Endpoint
        {
            get
            {
                return "BasicTestPage.html";
            }
        }
    }
}
