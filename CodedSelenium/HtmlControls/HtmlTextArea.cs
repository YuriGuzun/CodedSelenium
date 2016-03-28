namespace CodedSelenium.HtmlControls
{
    public class HtmlTextArea : HtmlTextControl
    {
        public HtmlTextArea()
        {
        }

        public HtmlTextArea(UITestControl parent)
          : base(parent)
        {
            SearchProperties.Add(HtmlTextArea.PropertyNames.TagName, "textarea");
        }
    }
}
