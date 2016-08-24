using CodedSelenium.HtmlControls;

namespace CodedSelenium.Test.ObjectMap
{
    public class BasicTestPage : Page
    {
        private HtmlTextArea _textArea;

        public BasicTestPage(BrowserWindow browserWindow)
            : base(browserWindow)
        {
        }

        public HtmlTextArea TextArea
        {
            get
            {
                if (_textArea == null)
                {
                    _textArea = new HtmlTextArea(this);
                    _textArea.SearchProperties.Add(HtmlTextArea.PropertyNames.Id, "textArea");
                }

                return _textArea;
            }
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
