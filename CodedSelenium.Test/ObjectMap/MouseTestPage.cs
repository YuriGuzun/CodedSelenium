using CodedSelenium.HtmlControls;

namespace CodedSelenium.Test.ObjectMap
{
    public class MouseTestPage : Page
    {
        private MouseClickDiv _firstDiv;
        private HtmlTextArea _textArea;

        public MouseTestPage(BrowserWindow browserWindow)
            : base(browserWindow)
        {
        }

        public override string Endpoint
        {
            get
            {
                return "MouseTestPage.html";
            }
        }

        public MouseClickDiv FirstDiv
        {
            get
            {
                if (_firstDiv == null)
                    _firstDiv = new MouseClickDiv(this);

                return _firstDiv;
            }
        }

        public HtmlTextArea TextArea
        {
            get
            {
                if (_textArea == null)
                    _textArea = new HtmlTextArea(this);

                return _textArea;
            }
        }
    }
}
