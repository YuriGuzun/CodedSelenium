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

        protected override List<string> PropertyNamesToIgnoreByCssSelector
        {
            get
            {
                foreach (string propertyName in new string[] { HtmlCell.PropertyNames.ColumnIndex, HtmlCell.PropertyNames.RowIndex })
                {
                    if (!base.PropertyNamesToIgnoreByCssSelector.Contains(propertyName))
                    {
                        base.PropertyNamesToIgnoreByCssSelector.Add(propertyName);
                    }
                }

                return base.PropertyNamesToIgnoreByCssSelector;
            }
        }

        protected override string GetCssSelector(Dictionary<string, string> dictionary)
        {
            string cssSelector = string.Empty;

            if (dictionary.ContainsKey(HtmlCell.PropertyNames.RowIndex))
            {
                string rowIndex = dictionary[HtmlCell.PropertyNames.RowIndex];
                cssSelector += string.Format("tr:nth-of-type({0}) ", rowIndex);
            }

            cssSelector += base.GetCssSelector(dictionary);

            if (dictionary.ContainsKey(HtmlCell.PropertyNames.ColumnIndex))
            {
                string columnIndex = dictionary[HtmlCell.PropertyNames.ColumnIndex];
                cssSelector += string.Format(":nth-of-type({0}) ", columnIndex);
            }

            return cssSelector;
        }

        public abstract new class PropertyNames : HtmlControl.PropertyNames
        {
            public static readonly string RowIndex = "rowindex";
            public static readonly string ColumnIndex = "columnindex";
            public static readonly string Value = HtmlControl.PropertyNames.InnerText;
        }
    }
}
