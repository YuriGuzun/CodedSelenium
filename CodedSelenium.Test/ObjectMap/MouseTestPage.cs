using CodedSelenium.HtmlControls;
using System.Collections.Generic;

namespace CodedSelenium.Test.ObjectMap
{
    public class MouseTestPage : Page
    {
        private HtmlDiv _divToDrag;
        private MouseClickDiv _firstDiv;
        private HtmlTextArea _textArea;
        private HtmlTable _table;

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

        public HtmlDiv DivToDrag
        {
            get
            {
                if (_divToDrag == null)
                {
                    _divToDrag = new HtmlDiv(this);
                    _divToDrag.SearchProperties.Add(HtmlDiv.PropertyNames.Id, "drag");
                }

                return _divToDrag;
            }
        }

        public HtmlTable Table
        {
            get
            {
                if (_table == null)
                    _table = new HtmlTable(this);

                return _table;
            }
        }

        public MouseClickDiv FirstDiv
        {
            get
            {
                if (_firstDiv == null)
                {
                    _firstDiv = new MouseClickDiv(this);
                    _firstDiv.SearchProperties.Add(HtmlDiv.PropertyNames.Id, "firstDiv");
                }

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
