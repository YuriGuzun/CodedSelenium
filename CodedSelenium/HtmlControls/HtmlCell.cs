using CodedSelenium.Selectors;
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

            this.RulesDictionary.Add(HtmlCell.PropertyNames.ColumnIndex, this.ByColumnIndex);
            this.RulesDictionary.Add(HtmlCell.PropertyNames.RowIndex, this.ByRowIndex);
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

        private SelectorPart ByColumnIndex(PropertyExpression propertyExpression)
        {
            int columnIndex = 0;
            if (int.TryParse(propertyExpression.PropertyValue, out columnIndex))
            {
                return new SelectorPart(string.Format(":nth-child({0})", columnIndex), SelectorPart.FilterType.ContentFilter);
            }

            throw new ArgumentOutOfRangeException("PropertyNames.ColumnIndex");
        }

        private SelectorPart ByRowIndex(PropertyExpression propertyExpression)
        {
            int rowIndex = 0;
            if (int.TryParse(propertyExpression.PropertyValue, out rowIndex))
            {
                string selector = string.Format(", jQuery(\"tr:nth-child({0})\"%parent%)", rowIndex);
                return new SelectorPart(selector, SelectorPart.FilterType.ExplicitParent);
            }

            throw new ArgumentOutOfRangeException("PropertyNames.RowIndex");
        }

        public abstract new class PropertyNames : HtmlControl.PropertyNames
        {
            public static readonly string RowIndex = "rowindex";
            public static readonly string ColumnIndex = "columnindex";
            public static readonly string Value = HtmlControl.PropertyNames.InnerText;
        }
    }
}
