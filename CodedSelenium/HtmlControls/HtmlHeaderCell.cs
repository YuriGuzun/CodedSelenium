namespace CodedSelenium.HtmlControls
{
    public class HtmlHeaderCell : HtmlControl
    {
        public HtmlHeaderCell()
        {
        }

        public HtmlHeaderCell(UITestControl parent)
          : base(parent)
        {
            SearchProperties.Add(HtmlControl.PropertyNames.TagName, "th");
        }
    }
}
