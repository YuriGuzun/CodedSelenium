using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;

namespace CodedSelenium.HtmlControls
{
    public class HtmlComboBox : HtmlControl
    {
        private SelectElement selector;

        public HtmlComboBox()
        {
        }

        public HtmlComboBox(UITestControl parent)
          : base(parent)
        {
            SearchProperties.Add(HtmlControl.PropertyNames.TagName, "select");
        }

        public virtual int ItemCount
        {
            get
            {
                return Items.Count;
            }
        }

        public virtual UITestControlCollection Items
        {
            get
            {
                UITestControlCollection collection = new UITestControlCollection();
                foreach (IWebElement option in Selector.Options)
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
                return Selector.SelectedOption.Text;
            }

            set
            {
                Selector.SelectByText(value);
            }
        }

        public virtual int SelectedIndex
        {
            get
            {
                return Array.IndexOf(GetContent(), SelectedItem);
            }

            set
            {
                Selector.SelectByIndex(value);
            }
        }

        public virtual string LabeledBy
        {
            get
            {
                return WebElement.GetAttribute(HtmlComboBox.PropertyNames.LabeledBy);
            }
        }

        public virtual int Size
        {
            get
            {
                return Items.Count;
            }
        }

        protected SelectElement Selector
        {
            get
            {
                if (selector == null)
                {
                    selector = new SelectElement(WebElement);
                }

                return selector;
            }
        }

        public string[] GetContent()
        {
            return Selector.Options.Select(item => item.Text).ToArray();
        }

        public abstract new class PropertyNames : HtmlControl.PropertyNames
        {
            public static readonly string ItemCount = "itemcount";
            public static readonly string Items = "items";
            public static readonly string SelectedItem = "selecteditem";

            public static readonly string SelectedIndex = "selectedindex";

            // TODO: Handle LabeledBy search property.
            public static readonly string LabeledBy = "labeledby";

            public static readonly string Size = "size";
        }
    }
}
