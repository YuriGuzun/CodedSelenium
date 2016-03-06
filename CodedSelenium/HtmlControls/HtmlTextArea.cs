using CodedSelenium.Selectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodedSelenium.HtmlControls
{
    public class HtmlTextArea : HtmlTextControl
    {
        public HtmlTextArea()
        {
        }

        public HtmlTextArea(UITestControl parent)
          : base(parent)
        {
            SearchProperties.Add(HtmlTextArea.PropertyNames.TagName, "textarea");
        }
    }
}
