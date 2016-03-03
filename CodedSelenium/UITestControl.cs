using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace CodedSelenium
{
    public class UITestControl
    {
        protected const string AttributeTemplate = "[{0}=\"{1}\"]";
        private const char ContainsSufix = '*';
        private PropertyExpressionCollection searchProperties;
        private PropertyExpressionCollection filterProperties;
        private IWebElement privateWebElement;
        private List<string> propertyNamesToIgnore;

        public UITestControl()
        {
        }

        public UITestControl(UITestControl parent)
        {
            if (parent.WebElement == null)
            {
                this.Parent = parent.Parent;
            }
            else
            {
                this.Parent = parent.WebElement;
            }
        }

        public UITestControl(ISearchContext parent)
        {
            this.Parent = parent;
        }

        public UITestControl(IWebElement webElement)
        {
            this.privateWebElement = webElement;
        }

        public virtual string InnerText
        {
            get
            {
                return this.WebElement.Text;
            }
        }

        public virtual bool Exists
        {
            get
            {
                return this.InternalWebElement != null;
            }
        }

        public virtual Rectangle BoundingRectangle
        {
            get
            {
                return new Rectangle(this.WebElement.Location, this.WebElement.Size);
            }
        }

        public PropertyExpressionCollection SearchProperties
        {
            get
            {
                if (this.searchProperties == null)
                {
                    this.searchProperties = new PropertyExpressionCollection();
                }

                return this.searchProperties;
            }
        }

        public PropertyExpressionCollection FilterProperties
        {
            get
            {
                if (this.filterProperties == null)
                {
                    this.filterProperties = new PropertyExpressionCollection();
                }

                return this.filterProperties;
            }
        }

        protected ISearchContext Parent { get; set; }

        protected virtual List<string> PropertyNamesToIgnoreByCssSelector
        {
            get
            {
                if (this.propertyNamesToIgnore == null)
                {
                    this.propertyNamesToIgnore = new List<string>()
                    {
                        UITestControl.PropertyNames.InnerText,
                        UITestControl.PropertyNames.TagName,
                        UITestControl.PropertyNames.TagInstance,
                        UITestControl.PropertyNames.Instance,
                    };
                }

                return this.propertyNamesToIgnore;
            }
        }

        protected virtual IWebElement WebElement
        {
            get
            {
                if (this.InternalWebElement == null)
                {
                    string message =
                        "Unable to find ui control control matching search criteria:\r\n" +
                        "\tSearchProperties: " + this.SearchProperties.ToString();

                    if (this.filterProperties != null && this.FilterProperties.Count != 0)
                    {
                        message += "\tFilterProperties: " + this.FilterProperties.ToString();
                    }

                    throw new UITestControlNotFoundException(message);
                }

                return this.InternalWebElement;
            }
        }

        private IWebElement InternalWebElement
        {
            get
            {
                if (this.privateWebElement != null)
                {
                    return this.privateWebElement;
                }

                IEnumerable<IWebElement> webElements = this.FindMatchingWebElements();
                return webElements.FirstOrDefault();
            }

            set
            {
                this.privateWebElement = this.WebElement;
            }
        }

        public void DrawHighlight()
        {
        }

        public bool TryFind()
        {
            return this.WebElement.Displayed;
        }

        public UITestControl GetParent()
        {
            return new UITestControl(this.WebElement.FindElement(By.XPath("..")));
        }

        public virtual UITestControlCollection GetChildren()
        {
            ReadOnlyCollection<IWebElement> descendants = this.WebElement.FindElements(By.XPath("*"));
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

            foreach (IWebElement webElement in this.FindMatchingWebElements())
            {
                collection.Add(new UITestControl(webElement));
            }

            return collection;
        }

        public virtual void CopyFrom(UITestControl controlToCopy)
        {
            this.Parent = controlToCopy.Parent;
            this.privateWebElement = controlToCopy.privateWebElement;
            this.searchProperties = controlToCopy.searchProperties;
            this.filterProperties = controlToCopy.filterProperties;
        }

        public virtual void Click()
        {
            this.WebElement.Click();
        }

        protected virtual string GetSelector(PropertyExpressionCollection propertyExpressionCollection)
        {
            string tagName = string.Empty;
            List<string> contentFilters = new List<string>();
            List<string> functionFilters = new List<string>();
            List<string> attributes = new List<string>();

            foreach (PropertyExpression p in propertyExpressionCollection)
            {
                switch (p.PropertyName)
                {
                    case UITestControl.PropertyNames.TagName:
                        tagName = p.PropertyValue;
                        break;

                    case UITestControl.PropertyNames.InnerText:
                        if (p.PropertyOperator == PropertyExpressionOperator.Contains)
                        {
                            contentFilters.Add(string.Format(":contains({0})", p.PropertyValue));
                        }
                        else
                        {
                            functionFilters.Add(string.Format("($(this).text() === \"{0}\")", p.PropertyValue));
                        }
                        break;

                    case UITestControl.PropertyNames.TagInstance:
                        contentFilters.Add(string.Format(":nth-child({0})", p.PropertyValue));
                        break;

                    case UITestControl.PropertyNames.Instance:
                        contentFilters.Add(string.Format(":eq({0})", p.PropertyValue));
                        break;

                    default:
                        string containsSign = p.PropertyOperator == PropertyExpressionOperator.Contains ? "*" : string.Empty;
                        attributes.Add(string.Format("[{0}=\"{1}\"]", p.PropertyName, containsSign, p.PropertyValue));
                        break;
                }
            }

            string selector = string.Format(
                "jQuery({0}{1}{2})",
                tagName,
                string.Join(string.Empty, attributes),
                string.Join(string.Empty, contentFilters));

            if (functionFilters.Count != 0)
            {
                string functionTemplate = ".filter(function() { return {0};})";
                selector += string.Format(functionTemplate, string.Join(" && ", functionFilters));
            }

            return selector;
        }

        private IEnumerable<IWebElement> FindMatchingWebElements()
        {
            string searchPropertySelector = this.GetSelector(this.SearchProperties);
            string filterPropertySelector = string.Empty;

            if (FilterProperties.Count > 0)
            {
                PropertyExpressionCollection searchAndFilterProperties = this.SearchProperties;
                searchAndFilterProperties.AddRange(this.FilterProperties);
                filterPropertySelector = this.GetSelector(searchAndFilterProperties);
            }

            return webElements;
        }

        public abstract class PropertyNames
        {
            public const string Class = "class";
            public const string HelpText = "helptext";
            public const string Id = "id";
            public const string InnerText = "innertext";
            public const string TagInstance = "taginstance";
            public const string Title = "title";
            public const string Type = "type";
            public const string ValueAttribute = "value";
            public const string TagName = "tagname";
            public const string Instance = "instance";

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
