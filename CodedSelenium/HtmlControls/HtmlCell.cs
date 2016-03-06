using CodedSelenium.Selectors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodedSelenium.HtmlControls
{
    public class HtmlCell : HtmlCellBase
    {
        public HtmlCell()
        {
        }

        public HtmlCell(UITestControl parent)
          : base(parent)
        {
            SearchProperties.Add(HtmlControl.PropertyNames.TagName, "td");
        }
    }
}
