using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodedSelenium.HtmlControls
{
    public class HtmlTextArea : HtmlControl
    {
        public HtmlTextArea()
        {
        }

        public HtmlTextArea(UITestControl parent)
          : base(parent)
        {
            this.SearchProperties.Add(HtmlControl.PropertyNames.TagName, "textarea");
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

        protected override List<string> PropertyNamesToIgnoreByCssSelector
        {
            get
            {
                foreach (string propertyName in new string[] { HtmlTextArea.PropertyNames.LabeledBy })
                {
                    if (!base.PropertyNamesToIgnoreByCssSelector.Contains(propertyName))
                    {
                        base.PropertyNamesToIgnoreByCssSelector.Add(propertyName);
                    }
                }

                return base.PropertyNamesToIgnoreByCssSelector;
            }
        }

        protected override string GetCssSelector(Dictionary<string, string> dictionary)
        {
            string cssSelector = base.GetCssSelector(dictionary);

            if (dictionary.ContainsKey(HtmlTextArea.PropertyNames.LabeledBy))
            {
                PropertyExpression labledByProperty = this.SearchProperties.FirstOrDefault(
                    item => item.PropertyName.Equals(HtmlTextArea.PropertyNames.LabeledBy));
                UITestControl label = new UITestControl(this.Parent);
                label.SearchProperties.Add(
                    UITestControl.PropertyNames.InnerText, labledByProperty.PropertyValue, labledByProperty.PropertyOperator);
                label.SearchProperties.Add(UITestControl.PropertyNames.TagName, "label");
            }

            return cssSelector;
        }

        public abstract new class PropertyNames : HtmlControl.PropertyNames
        {
            public static readonly string Text = HtmlControl.PropertyNames.InnerText;
            public static readonly string LabeledBy = "labeledby";
            public static readonly string ReadOnly = "readonly";
        }
    }
}
