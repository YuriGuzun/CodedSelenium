using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodedSelenium.HtmlControls
{
    public class HtmlFileInput : HtmlControl
    {
        public HtmlFileInput(UITestControl parent)
          : base(parent)
        {
            this.SearchProperties.Add(HtmlControl.PropertyNames.TagName, "input");
            this.SearchProperties.Add(HtmlControl.PropertyNames.Type, "file");
        }

        public virtual string FileName
        {
            get
            {
                return this.WebElement.GetAttribute(HtmlFileInput.PropertyNames.FileName);
            }

            set
            {
                this.WebElement.SendKeys(value);
            }
        }

        public virtual string LabeledBy
        {
            get
            {
                return this.WebElement.GetAttribute(HtmlFileInput.PropertyNames.LabeledBy);
            }
        }

        public virtual bool ReadOnly
        {
            get
            {
                return bool.Parse(this.WebElement.GetAttribute(HtmlFileInput.PropertyNames.ReadOnly));
            }
        }

        public abstract class PropertyNames : HtmlControl.PropertyNames
        {
            public static readonly string FileName = "value";
            public static readonly string LabeledBy = "labeledby";
            public static readonly string ReadOnly = "readonly";
        }
    }
}