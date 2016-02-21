using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodedSelenium.HtmlControls
{
    public class HtmlEdit : HtmlControl
    {
        public HtmlEdit(UITestControl parent)
            : base(parent)
        {
            SearchProperties.Add(HtmlButton.PropertyNames.TagName, "input");
        }

        public string Text
        {
            get
            {
                return WebElement.GetAttribute(HtmlEdit.PropertyNames.ValueAttribute);
            }

            set
            {
                WebElement.SendKeys(value);
            }
        }
    }
}