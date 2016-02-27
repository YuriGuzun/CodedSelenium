namespace CodedSelenium.HtmlControls
{
    public class HtmlControl : UITestControl
    {
        public HtmlControl()
        {
        }

        public HtmlControl(UITestControl parent)
            : base(parent)
        {
        }

        public virtual string HelpText
        {
            get
            {
                return this.WebElement.GetAttribute(HtmlControl.PropertyNames.HelpText);
            }
        }

        public virtual string Class
        {
            get
            {
                return this.WebElement.GetAttribute(HtmlControl.PropertyNames.Class);
            }
        }

        public virtual string Id
        {
            get
            {
                return this.WebElement.GetAttribute(HtmlControl.PropertyNames.Id);
            }
        }

        public virtual string InnerText
        {
            get
            {
                return WebElement.Text;
            }
        }

        public virtual string TagName
        {
            get
            {
                return this.WebElement.GetAttribute(HtmlControl.PropertyNames.TagName);
            }
        }

        public virtual string Title
        {
            get
            {
                return this.WebElement.GetAttribute(HtmlControl.PropertyNames.Title);
            }
        }

        public virtual string Type
        {
            get
            {
                return this.WebElement.GetAttribute(HtmlControl.PropertyNames.Type);
            }
        }

        public virtual string ValueAttribute
        {
            get
            {
                return this.WebElement.GetAttribute(HtmlControl.PropertyNames.ValueAttribute);
            }
        }
    }
}