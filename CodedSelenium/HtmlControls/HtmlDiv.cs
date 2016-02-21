using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodedSelenium.HtmlControls
{
    internal class HtmlDiv : HtmlControl
    {
        public HtmlDiv(UITestControl parent)
            : base(parent)
        {
            SearchProperties.Add(HtmlDiv.PropertyNames.TagName, "div");
        }
    }
}