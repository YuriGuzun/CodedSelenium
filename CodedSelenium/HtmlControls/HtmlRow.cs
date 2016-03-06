using CodedSelenium.Selectors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodedSelenium.HtmlControls
{
    public class HtmlRow : HtmlControl
    {
        public HtmlRow()
        {
        }

        public HtmlRow(UITestControl parent)
          : base(parent)
        {
            SearchProperties.Add(HtmlRow.PropertyNames.TagName, "tr");

            RulesDictionary.Add(HtmlRow.PropertyNames.RowIndex, ByRowIndex);
        }

        public virtual UITestControlCollection Cells
        {
            get
            {
                return GetChildren();
            }
        }

        public virtual int RowIndex
        {
            get
            {
                PropertyExpression property = SearchProperties
                    .FirstOrDefault(item => item.PropertyName.Equals(HtmlRow.PropertyNames.RowIndex));
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

        public virtual int CellCount
        {
            get
            {
                return Cells.Count;
            }
        }

        public string[] GetContent()
        {
            UITestControlCollection cells = Cells;
            if (cells != null)
            {
                return cells.GetValuesOfControls();
            }

            return null;
        }

        public HtmlCell GetCell(int cellIndex)
        {
            HtmlCell htmlCell = new HtmlCell(this);
            htmlCell.SearchProperties[HtmlCell.PropertyNames.ColumnIndex] = cellIndex.ToString();
            return htmlCell;
        }

        private SelectorPart ByRowIndex(PropertyExpression propertyExpression)
        {
            int rowIndex = 0;
            if (int.TryParse(propertyExpression.PropertyValue, out rowIndex))
            {
                return new SelectorPart(string.Format(":nth-child({0})", rowIndex), SelectorPart.FilterType.ContentFilter);
            }

            throw new ArgumentOutOfRangeException("PropertyNames.RowIndex");
        }

        public abstract new class PropertyNames : HtmlControl.PropertyNames
        {
            public static readonly string RowIndex = "rowindex";
            public static readonly string CellCount = "cellcount";
        }
    }
}
