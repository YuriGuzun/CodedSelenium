using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodedSelenium.HtmlControls
{
    public class HtmlSpan : HtmlControl
    {
        public HtmlSpan()
        {
        }

        public HtmlSpan(UITestControl parent)
          : base(parent)
        {
            this.SearchProperties.Add(HtmlControl.PropertyNames.TagName, "span");
        }

        public virtual string DisplayText
        {
            get
            {
                return this.WebElement.GetAttribute(HtmlSpan.PropertyNames.DisplayText);
            }
        }

        public abstract new class PropertyNames : HtmlControl.PropertyNames
        {
            public static readonly string DisplayText = HtmlControl.PropertyNames.InnerText;
        }
    }
}
