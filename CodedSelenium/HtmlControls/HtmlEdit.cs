namespace CodedSelenium.HtmlControls
{
    public class HtmlEdit : HtmlControl
    {
        public HtmlEdit(UITestControl parent)
            : base(parent)
        {
            SearchProperties.Add(HtmlButton.PropertyNames.TagName, "input");
        }

        public string Text
        {
            get
            {
                return WebElement.GetAttribute(HtmlEdit.PropertyNames.ValueAttribute);
            }

            set
            {
                WebElement.SendKeys(value);
            }
        }

        public virtual bool IsPassword
        {
            get
            {
                return this.WebElement.GetAttribute(HtmlEdit.PropertyNames.Type).Equals("password");
            }
        }

        public virtual string DefaultText
        {
            get
            {
                return this.WebElement.GetAttribute(HtmlEdit.PropertyNames.DefaultText);
            }
        }

        public virtual string LabeledBy
        {
            get
            {
                return this.WebElement.GetAttribute(HtmlEdit.PropertyNames.LabeledBy);
            }
        }

        public virtual bool ReadOnly
        {
            get
            {
                return this.WebElement.GetAttribute(HtmlEdit.PropertyNames.ReadOnly).Equals(HtmlEdit.PropertyNames.ReadOnly);
            }
        }

        public virtual string Password
        {
            set
            {
                this.Text = value;
            }
        }

        public virtual int MaxLength
        {
            get
            {
                string value = this.WebElement.GetAttribute(HtmlEdit.PropertyNames.MaxLength);
                return int.Parse(value);
            }
        }

        public abstract class PropertyNames : HtmlControl.PropertyNames
        {
            public static readonly string Text = "text";
            public static readonly string IsPassword = "ispassword";
            public static readonly string Password = "password";
            public static readonly string DefaultText = "value";
            public static readonly string CopyPastedText = "copypastedtext";
            public static readonly string LabeledBy = "labeledby";
            public static readonly string ReadOnly = "readonly";
            public static readonly string MaxLength = "maxlength";
        }
    }
}