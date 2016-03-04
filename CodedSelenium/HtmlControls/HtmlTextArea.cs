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

        protected override List<string> PropertyNamesToIgnoreBy
        {
            get
            {
                foreach (string propertyName in new string[] { HtmlTextArea.PropertyNames.LabeledBy })
                {
                    if (!base.PropertyNamesToIgnoreBy.Contains(propertyName))
                    {
                        base.PropertyNamesToIgnoreBy.Add(propertyName);
                    }
                }

                return base.PropertyNamesToIgnoreBy;
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
