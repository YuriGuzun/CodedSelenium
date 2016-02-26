﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodedSelenium.HtmlControls
{
    public class HtmlHyperlink : HtmlControl
    {
        public HtmlHyperlink(UITestControl parent)
          : base(parent)
        {
            this.SearchProperties.Add(HtmlControl.PropertyNames.TagName, "a");
        }

        public virtual string AbsolutePath
        {
            get
            {
                return (string)this.WebElement.GetAttribute(HtmlHyperlink.PropertyNames.AbsolutePath);
            }
        }

        public virtual string Alt
        {
            get
            {
                return (string)this.WebElement.GetAttribute(HtmlHyperlink.PropertyNames.Alt);
            }
        }

        public virtual string Href
        {
            get
            {
                return (string)this.WebElement.GetAttribute(HtmlHyperlink.PropertyNames.Href);
            }
        }

        public virtual string Target
        {
            get
            {
                return (string)this.WebElement.GetAttribute(HtmlHyperlink.PropertyNames.Target);
            }
        }

        public abstract class PropertyNames : HtmlControl.PropertyNames
        {
            public static readonly string AbsolutePath = "absolutepath";
            public static readonly string Alt = "alt";
            public static readonly string Href = "href";
            public static readonly string Target = "target";
        }
    }
}