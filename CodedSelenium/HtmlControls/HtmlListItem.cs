using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodedSelenium.HtmlControls
{
    public class HtmlListItem : HtmlControl
    {
        public HtmlListItem()
            : base()
        {
        }

        public HtmlListItem(UITestControl parent)
            : base(parent)
        {
        }

        public virtual bool Selected
        {
            get
            {
                return ParentList.SelectedItems.Contains(this.InnerText);
            }
        }

        public virtual string DisplayText
        {
            get
            {
                return this.InnerText;
            }
        }

        private HtmlList ParentList
        {
            get
            {
                return ParentTestControl as HtmlList;
            }
        }

        /// <summary>
        /// Selects the Item in the ParentList.
        /// </summary>
        public void Select()
        {
            ParentList.SelectedItems = new string[] { this.InnerText };
        }
    }
}
