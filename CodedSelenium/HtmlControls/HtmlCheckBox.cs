namespace CodedSelenium.HtmlControls
{
    public class HtmlCheckBox : HtmlControl
    {
        public HtmlCheckBox()
        {
        }

        public HtmlCheckBox(UITestControl parent)
          : base(parent)
        {
            this.SearchProperties.Add(HtmlControl.PropertyNames.TagName, "input");
            this.SearchProperties.Add(HtmlControl.PropertyNames.Type, "checkbox");
        }

        public virtual bool Checked
        {
            get
            {
                return this.WebElement.Selected;
            }

            set
            {
                if (this.WebElement.Selected != value)
                {
                    this.WebElement.Click();
                }
            }
        }

        public virtual string Value
        {
            get
            {
                return (string)this.WebElement.GetAttribute(HtmlCheckBox.PropertyNames.ValueAttribute);
            }
        }

        public virtual string LabeledBy
        {
            get
            {
                return (string)this.WebElement.GetAttribute(HtmlCheckBox.PropertyNames.LabeledBy);
            }
        }

        public abstract new class PropertyNames : HtmlControl.PropertyNames
        {
            public static readonly string Checked = "checked";
            public static readonly string LabeledBy = "labeledby";
        }
    }
}
