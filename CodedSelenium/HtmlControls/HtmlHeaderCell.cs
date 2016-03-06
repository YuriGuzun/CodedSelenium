using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodedSelenium.HtmlControls
{
    public class HtmlHeaderCell : HtmlControl
    {
        public HtmlHeaderCell()
        {
        }

        public HtmlHeaderCell(UITestControl parent)
          : base(parent)
        {
            this.SearchProperties.Add(HtmlControl.PropertyNames.TagName, "th");
        }
    }
}
