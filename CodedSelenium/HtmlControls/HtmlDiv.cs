namespace CodedSelenium.HtmlControls
{
    public class HtmlDiv : HtmlControl
    {
        public HtmlDiv()
        {
        }

        public HtmlDiv(UITestControl parent)
            : base(parent)
        {
            SearchProperties.Add(HtmlDiv.PropertyNames.TagName, "div");
        }
    }
}
