namespace CodedSelenium.HtmlControls
{
    public class HtmlCustom : HtmlControl
    {
        public HtmlCustom()
        {
        }

        public HtmlCustom(UITestControl parent)
          : base(parent)
        {
            SearchProperties.Add(UITestControl.PropertyNames.TagName, "*");
        }
    }
}
