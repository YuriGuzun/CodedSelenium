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
            this.SearchProperties.Add(HtmlControl.PropertyNames.TagName, "tr");
        }

        public virtual UITestControlCollection Cells
        {
            get
            {
                return this.GetChildren();
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
                return this.Cells.Count;
            }
        }

        protected override List<string> PropertyNamesToIgnoreByCssSelector
        {
            get
            {
                foreach (string propertyName in new string[] { HtmlRow.PropertyNames.CellCount, HtmlRow.PropertyNames.RowIndex })
                {
                    if (!base.PropertyNamesToIgnoreByCssSelector.Contains(propertyName))
                    {
                        base.PropertyNamesToIgnoreByCssSelector.Add(propertyName);
                    }
                }

                return base.PropertyNamesToIgnoreByCssSelector;
            }
        }

        public string[] GetContent()
        {
            UITestControlCollection cells = this.Cells;
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

        protected override string GetCssSelector(Dictionary<string, string> dictionary)
        {
            string cssSelector = base.GetCssSelector(dictionary);

            if (dictionary.ContainsKey(HtmlCell.PropertyNames.RowIndex))
            {
                string columnIndex = dictionary[HtmlCell.PropertyNames.RowIndex];
                cssSelector += string.Format(":nth-of-type({0}) ", columnIndex);
            }

            return cssSelector;
        }

        public abstract new class PropertyNames : HtmlControl.PropertyNames
        {
            public static readonly string RowIndex = "rowindex";
            public static readonly string CellCount = "cellcount";
        }
    }
}
