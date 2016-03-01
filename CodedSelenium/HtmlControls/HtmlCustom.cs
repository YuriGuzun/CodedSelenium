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
            this.SearchProperties.Add(UITestControl.PropertyNames.TagName, "*");
        }
    }
}
