using CodedSelenium.Selectors;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace CodedSelenium
{
    /// <summary>
    /// Coded UI content
    /// </summary>
    public partial class UITestControl : SelectorBasedControl
    {
        private PropertyExpressionCollection searchProperties;
        private PropertyExpressionCollection filterProperties;
        private IWebElement privateWebElement;

        public UITestControl()
        {
        }

        public UITestControl(UITestControl parent)
        {
            ParentTestControl = parent;

            if (parent.WebElement == null)
            {
                ParentSearchContext = parent.ParentSearchContext;
            }
            else
            {
                ParentSearchContext = parent.WebElement;
            }
        }

        public UITestControl(ISearchContext parent)
        {
            ParentSearchContext = parent;
        }

        public UITestControl(IWebElement webElement)
        {
            privateWebElement = webElement;
        }

        public virtual string InnerText
        {
            get
            {
                return WebElement.Text;
            }
        }

        public virtual bool Exists
        {
            get
            {
                return InternalWebElement != null;
            }
        }

        public virtual Rectangle BoundingRectangle
        {
            get
            {
                return new Rectangle(WebElement.Location, WebElement.Size);
            }
        }

        public PropertyExpressionCollection SearchProperties
        {
            get
            {
                if (searchProperties == null)
                {
                    searchProperties = new PropertyExpressionCollection();
                }

                return searchProperties;
            }
        }

        public PropertyExpressionCollection FilterProperties
        {
            get
            {
                if (filterProperties == null)
                {
                    filterProperties = new PropertyExpressionCollection();
                }

                return filterProperties;
            }
        }

        public void DrawHighlight()
        {
        }

        public bool TryFind()
        {
            return WebElement.Displayed;
        }

        public UITestControl GetParent()
        {
            return new UITestControl(WebElement.FindElement(By.XPath("..")));
        }

        public virtual UITestControlCollection GetChildren()
        {
            ReadOnlyCollection<IWebElement> descendants = WebElement.FindElements(By.XPath("*"));
            UITestControlCollection collection = new UITestControlCollection();

            foreach (IWebElement webElement in descendants)
            {
                collection.Add(new UITestControl(webElement));
            }

            return collection;
        }

        public void Find()
        {
        }

        public UITestControlCollection FindMatchingControls()
        {
            UITestControlCollection collection = new UITestControlCollection();
            ReadOnlyCollection<IWebElement> matchingElements = FindMatchingWebElements();

            if (matchingElements != null)
            {
                foreach (IWebElement webElement in matchingElements)
                {
                    collection.Add(new UITestControl(webElement));
                }
            }

            return collection;
        }

        public virtual void CopyFrom(UITestControl controlToCopy)
        {
            ParentSearchContext = controlToCopy.ParentSearchContext;
            privateWebElement = controlToCopy.privateWebElement;
            searchProperties = controlToCopy.searchProperties;
            filterProperties = controlToCopy.filterProperties;
        }

        public abstract class PropertyNames
        {
            public const string Class = "Class";
            public const string HelpText = "HelpText";
            public const string Id = "Id";
            public const string InnerText = "InnerText";
            public const string TagInstance = "TagInstance";
            public const string Title = "Title";
            public const string Type = "Type";
            public const string ValueAttribute = "value";
            public const string TagName = "TagName";
            public const string Instance = "Instance";

            [EditorBrowsable(EditorBrowsableState.Never)]
            public static new bool Equals(object objA, object objB)
            {
                return object.Equals(objA, objB);
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            public static new bool ReferenceEquals(object objA, object objB)
            {
                return object.ReferenceEquals(objA, objB);
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            public override bool Equals(object obj)
            {
                return base.Equals(obj);
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            public override string ToString()
            {
                return base.ToString();
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            public new Type GetType()
            {
                return base.GetType();
            }
        }
    }
}
