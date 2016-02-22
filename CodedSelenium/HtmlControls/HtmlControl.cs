namespace CodedSelenium.HtmlControls
{
    public class HtmlControl : UITestControl
    {
        public HtmlControl(UITestControl parent)
            : base(parent)
        {
        }

        public string InnerText
        {
            get
            {
                return WebElement.Text;
            }
        }
    }
}