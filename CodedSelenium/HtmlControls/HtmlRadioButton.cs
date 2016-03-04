using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            this.SearchProperties.Add(HtmlControl.PropertyNames.TagName, "input");
            this.SearchProperties.Add(HtmlControl.PropertyNames.Type, "radio");
        }

        public virtual bool Selected
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
                return this.WebElement.GetAttribute(HtmlRadioButton.PropertyNames.Value);
            }
        }

        public virtual UITestControlCollection Group
        {
            get
            {
                return this.GetParent().GetChildren();
            }
        }

        public virtual int ItemCount
        {
            get
            {
                string value = this.WebElement.GetAttribute(HtmlRadioButton.PropertyNames.ItemCount);
                return int.Parse(value);
            }
        }

        public virtual string LabeledBy
        {
            get
            {
                return this.WebElement.GetAttribute(HtmlRadioButton.PropertyNames.LabeledBy);
            }
        }

        protected override List<string> PropertyNamesToIgnoreBy
        {
            get
            {
                string[] ignoreList = new string[] { HtmlRadioButton.PropertyNames.Selected, HtmlRadioButton.PropertyNames.Group };
                foreach (string propertyName in ignoreList)
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
            public static readonly string Selected = "selected";
            public static readonly string Value = HtmlControl.PropertyNames.ValueAttribute;
            public static readonly string Group = "group";
            public static readonly string ItemCount = HtmlControl.PropertyNames.Instance;
            public static readonly string LabeledBy = HtmlControl.PropertyNames.InnerText;
        }
    }
}
