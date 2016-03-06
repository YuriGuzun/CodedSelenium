using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodedSelenium.HtmlControls
{
    public class HtmlTable : HtmlControl
    {
        public HtmlTable()
        {
        }

        public HtmlTable(UITestControl parent)
          : base(parent)
        {
            SearchProperties.Add(HtmlControl.PropertyNames.TagName, "table");
        }

        public virtual UITestControlCollection Cells
        {
            get
            {
                return new HtmlCell(this).FindMatchingControls();
            }
        }

        public virtual UITestControlCollection Rows
        {
            get
            {
                return new HtmlRow(this).FindMatchingControls();
            }
        }

        public virtual int RowCount
        {
            get
            {
                return Rows.Count;
            }
        }

        public virtual int ColumnCount
        {
            get
            {
                return new HtmlCell(new HtmlRow(this)).FindMatchingControls().Count;
            }
        }

        public virtual int CellCount
        {
            get
            {
                return Cells.Count;
            }
        }

        public HtmlControl GetCell(int rowIndex, int columnIndex)
        {
            HtmlCell htmlCell = new HtmlCell(this);
            htmlCell.SearchProperties[HtmlCell.PropertyNames.RowIndex] = rowIndex.ToString();
            htmlCell.SearchProperties[HtmlCell.PropertyNames.ColumnIndex] = columnIndex.ToString();
            return htmlCell;
        }

        public HtmlControl GetRow(int rowIndex)
        {
            HtmlRow htmlRow = new HtmlRow(this);
            htmlRow.SearchProperties[HtmlRow.PropertyNames.RowIndex] = rowIndex.ToString();
            return htmlRow;
        }

        public HtmlCell FindFirstCellWithValue(string value)
        {
            HtmlCell htmlCell = new HtmlCell(this);
            htmlCell.SearchProperties[HtmlControl.PropertyNames.InnerText] = value;
            return htmlCell;
        }

        public string[] GetContent()
        {
            UITestControlCollection cells = Cells;
            if (cells != null)
                return cells.GetValuesOfControls();
            return null;
        }

        public string[] GetColumnNames()
        {
            UITestControlCollection cells = new HtmlHeaderCell(this).FindMatchingControls();
            if (cells != null)
                return cells.GetValuesOfControls();
            return null;
        }

        public abstract new class PropertyNames : HtmlControl.PropertyNames
        {
            public static readonly string Cells = "cells";
            public static readonly string Rows = "rows";
            public static readonly string RowCount = "rowcount";
            public static readonly string CellCount = "cellcount";
            public static readonly string ColumnCount = "columncount";
        }
    }
}
