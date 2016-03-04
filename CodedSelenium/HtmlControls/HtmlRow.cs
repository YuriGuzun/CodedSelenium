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

        protected override List<string> PropertyNamesToIgnoreBy
        {
            get
            {
                foreach (string propertyName in new string[] { HtmlRow.PropertyNames.CellCount, HtmlRow.PropertyNames.RowIndex })
                {
                    if (!base.PropertyNamesToIgnoreBy.Contains(propertyName))
                    {
                        base.PropertyNamesToIgnoreBy.Add(propertyName);
                    }
                }

                return base.PropertyNamesToIgnoreBy;
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

        public abstract new class PropertyNames : HtmlControl.PropertyNames
        {
            public static readonly string RowIndex = "rowindex";
            public static readonly string CellCount = "cellcount";
        }
    }
}
