using CodedSelenium.Selectors;

namespace CodedSelenium.HtmlControls
{
    public class HtmlTextControl : HtmlControl
    {
        protected HtmlTextControl()
            : base()
        {
        }

        protected HtmlTextControl(UITestControl parent)
          : base(parent)
        {
            RulesDictionary.Add(HtmlTextArea.PropertyNames.LabeledBy, ByLabeledBy);
        }

        public string Text
        {
            get
            {
                return WebElement.GetAttribute(HtmlTextArea.PropertyNames.ValueAttribute);
            }

            set
            {
                WebElement.Clear();
                WebElement.SendKeys(value);

                Wait.Until((d) => { return this.Text == value; });
            }
        }

        public virtual string LabeledBy
        {
            get
            {
                return WebElement.GetAttribute(HtmlTextArea.PropertyNames.LabeledBy);
            }
        }

        public virtual bool ReadOnly
        {
            get
            {
                return WebElement.GetAttribute(HtmlTextArea.PropertyNames.ReadOnly).Equals(HtmlTextArea.PropertyNames.ReadOnly);
            }
        }

        private SelectorPart ByLabeledBy(PropertyExpression propertyExpression)
        {
            if (propertyExpression.PropertyOperator == PropertyExpressionOperator.Contains)
            {
                string selector = string.Format(".prev(\"label:contains({0})\").next(\"textarea\")", propertyExpression.PropertyValue);
                return new SelectorPart(selector, SelectorPart.FilterType.Method);
            }
            else
            {
                string selector = string.Format(
                    ".prev().filter(function() {{ return $(this).text() === \"{0}\";}}).next(\"textarea\")",
                    propertyExpression.PropertyValue);
                return new SelectorPart(selector, SelectorPart.FilterType.Method);
            }
        }

        public abstract new class PropertyNames : HtmlControl.PropertyNames
        {
            public static readonly string Text = HtmlControl.PropertyNames.InnerText;
            public static readonly string LabeledBy = "labeledby";
            public static readonly string ReadOnly = "readonly";
        }
    }
}
