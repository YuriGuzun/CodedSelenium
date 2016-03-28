namespace CodedSelenium.HtmlControls
{
    public class HtmlRadioButton : HtmlControl
    {
        public HtmlRadioButton()
        {
        }

        public HtmlRadioButton(UITestControl parent)
          : base(parent)
        {
            SearchProperties.Add(HtmlControl.PropertyNames.TagName, "input");
            SearchProperties.Add(HtmlControl.PropertyNames.Type, "radio");
        }

        public virtual bool Selected
        {
            get
            {
                return WebElement.Selected;
            }

            set
            {
                if (WebElement.Selected != value)
                {
                    WebElement.Click();
                }
            }
        }

        public virtual string Value
        {
            get
            {
                return WebElement.GetAttribute(HtmlRadioButton.PropertyNames.Value);
            }
        }

        public virtual UITestControlCollection Group
        {
            get
            {
                return GetParent().GetChildren();
            }
        }

        public virtual int ItemCount
        {
            get
            {
                string value = WebElement.GetAttribute(HtmlRadioButton.PropertyNames.ItemCount);
                return int.Parse(value);
            }
        }

        public virtual string LabeledBy
        {
            get
            {
                return WebElement.GetAttribute(HtmlRadioButton.PropertyNames.LabeledBy);
            }
        }

        public abstract new class PropertyNames : HtmlControl.PropertyNames
        {
            public static readonly string Selected = "selected";
            public static readonly string Value = HtmlControl.PropertyNames.ValueAttribute;
            public static readonly string Group = "group";
            public static readonly string ItemCount = HtmlControl.PropertyNames.Instance;
            public static readonly string LabeledBy = HtmlControl.PropertyNames.InnerText;
        }
    }
}
