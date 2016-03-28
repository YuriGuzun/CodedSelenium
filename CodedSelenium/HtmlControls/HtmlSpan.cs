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
            SearchProperties.Add(HtmlControl.PropertyNames.TagName, "span");
        }

        public virtual string DisplayText
        {
            get
            {
                return WebElement.GetAttribute(HtmlSpan.PropertyNames.DisplayText);
            }
        }

        public abstract new class PropertyNames : HtmlControl.PropertyNames
        {
            public static readonly string DisplayText = HtmlControl.PropertyNames.InnerText;
        }
    }
}
