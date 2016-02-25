using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodedSelenium.HtmlControls
{
    public class HtmlComboBox : HtmlControl
    {
        private SelectElement selector;

        public HtmlComboBox(UITestControl parent)
          : base(parent)
        {
            this.SearchProperties.Add(HtmlControl.PropertyNames.TagName, "select");
        }

        public virtual int ItemCount
        {
            get
            {
                return this.Items.Count;
            }
        }

        public virtual UITestControlCollection Items
        {
            get
            {
                UITestControlCollection collection = new UITestControlCollection();
                foreach (IWebElement option in this.Selector.Options)
                {
                    collection.Add(new UITestControl(option));
                }

                return collection;
            }
        }

        public virtual string SelectedItem
        {
            get
            {
                return this.Selector.SelectedOption.Text;
            }

            set
            {
                this.Selector.SelectByText(value);
            }
        }

        public virtual int SelectedIndex
        {
            get
            {
                return Array.IndexOf(this.GetContent(), this.SelectedItem);
            }

            set
            {
                this.Selector.SelectByIndex(value);
            }
        }

        public virtual string LabeledBy
        {
            get
            {
                return this.WebElement.GetAttribute(HtmlComboBox.PropertyNames.LabeledBy);
            }
        }

        public virtual int Size
        {
            get
            {
                return this.Items.Count;
            }
        }

        private SelectElement Selector
        {
            get
            {
                if (this.selector == null)
                {
                    this.selector = new SelectElement(this.WebElement);
                }

                return this.selector;
            }
        }

        public string[] GetContent()
        {
            return this.Selector.Options.Select(item => item.Text).ToArray();
        }

        public abstract class PropertyNames : HtmlControl.PropertyNames
        {
            public static readonly string ItemCount = "itemcount";
            public static readonly string Items = "items";
            public static readonly string SelectedItem = "selecteditem";
            public static readonly string SelectedIndex = "selectedindex";
            public static readonly string LabeledBy = "labeledby";
            public static readonly string Size = "size";
        }
    }
}