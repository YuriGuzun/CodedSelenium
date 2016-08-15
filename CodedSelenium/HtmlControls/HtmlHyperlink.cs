namespace CodedSelenium.HtmlControls
{
    public class HtmlHyperlink : HtmlControl
    {
        public HtmlHyperlink()
        {
        }

        public HtmlHyperlink(UITestControl parent)
          : base(parent)
        {
            SearchProperties.Add(HtmlControl.PropertyNames.TagName, "a");
        }

        public virtual string AbsolutePath
        {
            get
            {
                return GetAttribute(HtmlHyperlink.PropertyNames.AbsolutePath);
            }
        }

        public virtual string Alt
        {
            get
            {
                return GetAttribute(HtmlHyperlink.PropertyNames.Alt);
            }
        }

        public virtual string Href
        {
            get
            {
                return GetAttribute(HtmlHyperlink.PropertyNames.Href);
            }
        }

        public virtual string Target
        {
            get
            {
                return GetAttribute(HtmlHyperlink.PropertyNames.Target);
            }
        }

        public abstract new class PropertyNames : HtmlControl.PropertyNames
        {
            public static readonly string AbsolutePath = "absolutepath";
            public static readonly string Alt = "alt";
            public static readonly string Href = "href";
            public static readonly string Target = "target";
        }
    }
}
