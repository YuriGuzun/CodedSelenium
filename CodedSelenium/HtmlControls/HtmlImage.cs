namespace CodedSelenium.HtmlControls
{
    public class HtmlImage : HtmlControl
    {
        public HtmlImage()
        {
        }

        public HtmlImage(UITestControl parent)
          : base(parent)
        {
            SearchProperties.Add(HtmlControl.PropertyNames.TagName, "img");
        }

        public virtual string Alt
        {
            get
            {
                return WebElement.GetAttribute(HtmlImage.PropertyNames.Alt);
            }
        }

        public virtual string Src
        {
            get
            {
                return WebElement.GetAttribute(HtmlImage.PropertyNames.Src);
            }
        }

        public virtual string AbsolutePath
        {
            get
            {
                return WebElement.GetAttribute(HtmlImage.PropertyNames.AbsolutePath);
            }
        }

        public virtual string LinkAbsolutePath
        {
            get
            {
                return WebElement.GetAttribute(HtmlImage.PropertyNames.LinkAbsolutePath);
            }
        }

        public virtual string Href
        {
            get
            {
                return WebElement.GetAttribute(HtmlImage.PropertyNames.Href);
            }
        }

        public abstract new class PropertyNames : HtmlControl.PropertyNames
        {
            public static readonly string Src = "src";
            public static readonly string Alt = PropertyNames.Src;
            public static readonly string AbsolutePath = PropertyNames.Src;
            public static readonly string LinkAbsolutePath = PropertyNames.Src;
            public static readonly string Href = PropertyNames.Src;
        }
    }
}
