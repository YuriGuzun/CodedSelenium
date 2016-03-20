using CodedSelenium.HtmlControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodedSelenium.Test.ObjectMap
{
    public class MouseTestPage : Page
    {
        private MouseClickDiv firstDiv;

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
                if (firstDiv == null)
                    firstDiv = new MouseClickDiv(this);

                return firstDiv;
            }
        }
    }
}
