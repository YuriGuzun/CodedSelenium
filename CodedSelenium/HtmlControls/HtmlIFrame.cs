﻿namespace CodedSelenium.HtmlControls
{
    public class HtmlIFrame : HtmlControl
    {
        public HtmlIFrame()
        {
        }

        public HtmlIFrame(UITestControl parent)
          : base(parent)
        {
            SearchProperties.Add(HtmlControl.PropertyNames.TagName, "iframe");
        }

        public virtual string PageUrl
        {
            get
            {
                return GetAttribute(HtmlIFrame.PropertyNames.PageUrl);
            }
        }

        public virtual string AbsolutePath
        {
            get
            {
                return GetAttribute(HtmlIFrame.PropertyNames.AbsolutePath);
            }
        }

        public virtual bool Scrollable
        {
            get
            {
                string scrolingValue = GetAttribute(HtmlIFrame.PropertyNames.Scrollable);

                if (string.IsNullOrEmpty(scrolingValue) || !scrolingValue.Contains("no"))
                {
                    return true;
                }

                return false;
            }
        }

        public abstract new class PropertyNames : HtmlControl.PropertyNames
        {
            public static readonly string PageUrl = "src";
            public static readonly string AbsolutePath = PropertyNames.PageUrl;
            public static readonly string Scrollable = "scrolling";
        }
    }
}
