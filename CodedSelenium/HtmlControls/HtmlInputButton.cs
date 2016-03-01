namespace CodedSelenium.HtmlControls
{
    public class HtmlInputButton : HtmlControl
    {
        public HtmlInputButton()
        {
        }

        public HtmlInputButton(UITestControl parent)
          : base(parent)
        {
            this.SearchProperties.Add(HtmlControl.PropertyNames.Type, "button");
            this.SearchProperties.Add(HtmlControl.PropertyNames.TagName, "input");
        }
    }
}
