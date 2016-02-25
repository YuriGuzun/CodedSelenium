using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodedSelenium.HtmlControls
{
    public class HtmlDocument : HtmlControl
    {
        public HtmlDocument(UITestControl parent)
          : base(parent)
        {
            this.SearchProperties.Add(HtmlControl.PropertyNames.TagName, "body");
        }

        public abstract class PropertyNames : HtmlControl.PropertyNames
        {
            public static readonly string PageUrl = "PageUrl";
            public static readonly string FrameDocument = "FrameDocument";
            public static readonly string RedirectingPage = "RedirectingPage";
            public static readonly string AbsolutePath = "AbsolutePath";
        }
    }
}