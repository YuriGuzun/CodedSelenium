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
            SearchProperties.Add(HtmlControl.PropertyNames.Type, "button");
            SearchProperties.Add(HtmlControl.PropertyNames.TagName, "input");
        }
    }
}
