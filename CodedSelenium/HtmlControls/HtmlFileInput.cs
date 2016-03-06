namespace CodedSelenium.HtmlControls
{
    public class HtmlFileInput : HtmlControl
    {
        public HtmlFileInput()
        {
        }

        public HtmlFileInput(UITestControl parent)
          : base(parent)
        {
            SearchProperties.Add(HtmlControl.PropertyNames.TagName, "input");
            SearchProperties.Add(HtmlControl.PropertyNames.Type, "file");
        }

        public virtual string FileName
        {
            get
            {
                return WebElement.GetAttribute(HtmlFileInput.PropertyNames.FileName);
            }

            set
            {
                WebElement.SendKeys(value);
            }
        }

        public virtual string LabeledBy
        {
            get
            {
                return WebElement.GetAttribute(HtmlFileInput.PropertyNames.LabeledBy);
            }
        }

        public virtual bool ReadOnly
        {
            get
            {
                return bool.Parse(WebElement.GetAttribute(HtmlFileInput.PropertyNames.ReadOnly));
            }
        }

        public abstract new class PropertyNames : HtmlControl.PropertyNames
        {
            public static readonly string FileName = "value";
            public static readonly string LabeledBy = "labeledby";
            public static readonly string ReadOnly = "readonly";
        }
    }
}
