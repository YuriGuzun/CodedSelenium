using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodedSelenium.HtmlControls
{
    public class HtmlList : HtmlSelect
    {
        public HtmlList()
          : base()
        {
        }

        public HtmlList(UITestControl parent)
          : base(parent)
        {
        }

        public virtual string[] SelectedItems
        {
            get
            {
                return Selector.AllSelectedOptions.Select(item => item.Text).ToArray();
            }

            set
            {
                Selector.DeselectAll();
                foreach (string item in value)
                    Selector.SelectByText(item);
            }
        }

        public virtual int[] SelectedIndices
        {
            get
            {
                string[] allValues = GetContent();

                return SelectedItems.Select(item => Array.IndexOf(allValues, item)).ToArray();
            }

            set
            {
                Selector.DeselectAll();
                foreach (int index in value)
                    Selector.SelectByIndex(index);
            }
        }

        public virtual string SelectedItemsAsString
        {
            get
            {
                return string.Join(string.Empty, SelectedItems);
            }

            // TODO: Clarify what is expected.
            set
            {
                throw new NotImplementedException();
            }
        }

        public virtual bool IsMultipleSelection
        {
            get
            {
                return Selector.IsMultiple;
            }
        }

        public abstract new class PropertyNames : HtmlControl.PropertyNames
        {
            // TODO: Confirm if CodedUI can search by this property.

            public static readonly string SelectedItems = "SelectedItems";
            public static readonly string SelectedIndices = "SelectedIndices";
            public static readonly string SelectedItemsAsString = "SelectedItemsAsString";
            public static readonly string IsMultipleSelection = "multiple";
        }
    }
}
