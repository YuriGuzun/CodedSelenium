using System;
using System.Collections.Generic;
using System.Linq;

namespace CodedSelenium.HtmlControls
{
    public class HtmlCell : HtmlControl
    {
        public HtmlCell()
        {
        }

        public HtmlCell(UITestControl parent)
          : base(parent)
        {
            this.SearchProperties.Add(HtmlControl.PropertyNames.TagName, "td");
        }

        public virtual int RowIndex
        {
            get
            {
                PropertyExpression property = SearchProperties
                    .FirstOrDefault(item => item.PropertyName.Equals(HtmlCell.PropertyNames.RowIndex));
                if (property != null)
                {
                    string value = property.PropertyValue;
                    return int.Parse(value);
                }
                else
                {
                    throw new NotImplementedException("Get row index is not implemented atm");
                }
            }
        }

        public virtual int ColumnIndex
        {
            get
            {
                PropertyExpression property = SearchProperties
                    .FirstOrDefault(item => item.PropertyName.Equals(HtmlCell.PropertyNames.ColumnIndex));
                if (property != null)
                {
                    string value = property.PropertyValue;
                    return int.Parse(value);
                }
                else
                {
                    throw new NotImplementedException("Get column index is not implemented atm");
                }
            }
        }

        public virtual string Value
        {
            get
            {
                return this.WebElement.Text;
            }
        }

        protected override List<string> PropertyNamesToIgnoreBy
        {
            get
            {
                foreach (string propertyName in new string[] { HtmlCell.PropertyNames.ColumnIndex, HtmlCell.PropertyNames.RowIndex })
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
            public static readonly string RowIndex = "rowindex";
            public static readonly string ColumnIndex = "columnindex";
            public static readonly string Value = HtmlControl.PropertyNames.InnerText;
        }
    }
}
