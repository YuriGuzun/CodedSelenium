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

        //// TODO: Clarify what is expected.

        public virtual string HelpText
        {
            get
            {
                return GetAttribute(HtmlControl.PropertyNames.HelpText);
            }
        }

        public virtual string Class
        {
            get
            {
                return GetAttribute(HtmlControl.PropertyNames.Class);
            }
        }

        public virtual string Id
        {
            get
            {
                return GetAttribute(HtmlControl.PropertyNames.Id);
            }
        }

        public virtual string TagName
        {
            get
            {
                return WebElement.TagName;
            }
        }

        public virtual string Title
        {
            get
            {
                return GetAttribute(HtmlControl.PropertyNames.Title);
            }
        }

        public virtual string Type
        {
            get
            {
                return GetAttribute(HtmlControl.PropertyNames.Type);
            }
        }

        public virtual string ValueAttribute
        {
            get
            {
                return GetAttribute(HtmlControl.PropertyNames.ValueAttribute);
            }
        }

        protected virtual string GetAttribute(string name)
        {
            return WebElement.GetAttribute(name);
        }
    }
}
