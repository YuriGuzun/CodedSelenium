using CodedSelenium.Selectors;
using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;

namespace CodedSelenium
{
    /// <summary>
    /// Coded UI content
    /// </summary>
    public partial class UITestControl : SelectorBasedControl
    {
        private PropertyExpressionCollection _searchProperties;
        private PropertyExpressionCollection _filterProperties;
        private IWebElement _privateWebElement;

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

            TopParent = parent.TopParent ?? parent;
        }

        public UITestControl(UITestControl parent, ISearchContext parentSearchContext)
            : this(parent)
        {
            ParentSearchContext = parentSearchContext;
        }

        public UITestControl(UITestControl parent, IWebElement webElement)
            : this(parent)
        {
            _privateWebElement = webElement;
        }

        public virtual UITestControl TopParent
        {
            get; private set;
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
                if (_searchProperties == null)
                {
                    _searchProperties = new PropertyExpressionCollection();
                }

                return _searchProperties;
            }
        }

        public PropertyExpressionCollection FilterProperties
        {
            get
            {
                if (_filterProperties == null)
                {
                    _filterProperties = new PropertyExpressionCollection();
                }

                return _filterProperties;
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
            return new UITestControl(this.TopParent, WebElement.FindElement(By.XPath("..")));
        }

        public virtual UITestControlCollection GetChildren()
        {
            ReadOnlyCollection<IWebElement> descendants = WebElement.FindElements(By.XPath("*"));
            UITestControlCollection collection = new UITestControlCollection();

            foreach (IWebElement webElement in descendants)
            {
                collection.Add(new UITestControl(this, webElement));
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
                    collection.Add(new UITestControl(ParentTestControl, webElement));
                }
            }

            return collection;
        }

        public virtual void CopyFrom(UITestControl controlToCopy)
        {
            ParentSearchContext = controlToCopy.ParentSearchContext;
            _privateWebElement = controlToCopy._privateWebElement;
            _searchProperties = controlToCopy._searchProperties;
            _filterProperties = controlToCopy._filterProperties;
        }

        public abstract class PropertyNames
        {
            public const string Class = "Class";
            public const string Id = "Id";
            public const string InnerText = "InnerText";
            public const string TagInstance = "TagInstance";
            public const string Title = "Title";
            public const string HelpText = Title;
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
