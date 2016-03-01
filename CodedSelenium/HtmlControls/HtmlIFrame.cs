using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodedSelenium.HtmlControls
{
    public class HtmlIFrame : HtmlControl
    {
        public HtmlIFrame()
        {
        }

        public HtmlIFrame(UITestControl parent)
          : base(parent)
        {
            this.SearchProperties.Add(HtmlControl.PropertyNames.TagName, "iframe");
        }

        public virtual string PageUrl
        {
            get
            {
                return this.WebElement.GetAttribute(HtmlIFrame.PropertyNames.PageUrl);
            }
        }

        public virtual string AbsolutePath
        {
            get
            {
                return this.WebElement.GetAttribute(HtmlIFrame.PropertyNames.AbsolutePath);
            }
        }

        public virtual bool Scrollable
        {
            get
            {
                string scrolingValue = this.WebElement.GetAttribute(HtmlIFrame.PropertyNames.Scrollable);

                if (string.IsNullOrEmpty(scrolingValue) || !scrolingValue.Contains("no"))
                {
                    return true;
                }

                return false;
            }
        }

        public abstract new class PropertyNames : HtmlControl.PropertyNames
        {
            public static readonly string PageUrl = "src";
            public static readonly string AbsolutePath = PropertyNames.PageUrl;
            public static readonly string Scrollable = "scrolling";
        }
    }
}
