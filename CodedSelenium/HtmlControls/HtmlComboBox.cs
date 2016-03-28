using System;

namespace CodedSelenium.HtmlControls
{
    public class HtmlComboBox : HtmlSelect
    {
        public HtmlComboBox(UITestControl parent)
          : base(parent)
        {
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
    }
}
