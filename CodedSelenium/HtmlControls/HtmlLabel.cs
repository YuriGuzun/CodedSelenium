namespace CodedSelenium.HtmlControls
{
    public class HtmlLabel : HtmlControl
    {
        public HtmlLabel()
        {
        }

        public HtmlLabel(UITestControl parent)
          : base(parent)
        {
            SearchProperties.Add(HtmlControl.PropertyNames.TagName, "label");
        }

        public virtual string DisplayText
        {
            get
            {
                return WebElement.GetAttribute(HtmlLabel.PropertyNames.DisplayText);
            }
        }

        public virtual string LabelFor
        {
            get
            {
                return WebElement.GetAttribute(HtmlLabel.PropertyNames.LabelFor);
            }
        }

        public abstract new class PropertyNames : HtmlControl.PropertyNames
        {
            public static readonly string DisplayText = HtmlControl.PropertyNames.InnerText;
            public static readonly string LabelFor = "for";
        }
    }
}
