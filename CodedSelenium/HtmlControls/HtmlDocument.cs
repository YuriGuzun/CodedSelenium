namespace CodedSelenium.HtmlControls
{
    public class HtmlDocument : HtmlControl
    {
        public HtmlDocument()
        {
        }

        public HtmlDocument(UITestControl parent)
          : base(parent)
        {
            this.SearchProperties.Add(HtmlControl.PropertyNames.TagName, "body");
        }

        public abstract new class PropertyNames : HtmlControl.PropertyNames
        {
            public static readonly string PageUrl = "pageurl";
            public static readonly string FrameDocument = "framedocument";
            public static readonly string RedirectingPage = "redirectingpage";
            public static readonly string AbsolutePath = "absolutepath";
        }
    }
}
