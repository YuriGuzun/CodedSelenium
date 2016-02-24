using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace CodedSelenium
{
    public class UITestControl
    {
        private const string ContainsSufix = "*";
        private PropertyExpressionCollection searchProperties;
        private PropertyExpressionCollection filterProperties;
        private IWebElement webElement;

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

        private UITestControl(ISearchContext parent)
        {
            this.Parent = parent;
        }

        private UITestControl(IWebElement webElement)
        {
            this.webElement = webElement;
        }

        public virtual bool Exists
        {
            get
            {
                return this.WebElement.Displayed;
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

        protected virtual IWebElement WebElement
        {
            get
            {
                if (this.webElement != null)
                {
                    return this.webElement;
                }

                IEnumerable<IWebElement> webElements = this.FindMatchingWebElements();
                return webElements.FirstOrDefault();
            }

            private set
            {
                this.webElement = this.WebElement;
            }
        }

        public UITestControl GetParent()
        {
            return new UITestControl(this.Parent);
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
            this.webElement = controlToCopy.webElement;
        }

        public virtual void Click()
        {
            this.WebElement.Click();
        }

        private IEnumerable<IWebElement> FindMatchingWebElements()
        {
            Dictionary<string, string> dictionary = this.GetSearchPropertiesDictionary(this.SearchProperties);
            IEnumerable<IWebElement> webElements = this.GetWebElements(dictionary);

            if (webElements.Count() > 1 && (this.filterProperties != null || this.FilterProperties.Count != 0))
            {
                PropertyExpressionCollection searchAndFilterProperties = this.SearchProperties;
                searchAndFilterProperties.AddRange(this.FilterProperties);
                webElements = this.GetWebElements(this.GetSearchPropertiesDictionary(searchAndFilterProperties));
            }

            return webElements;
        }

        private IEnumerable<IWebElement> GetWebElements(Dictionary<string, string> dictionary)
        {
            if (dictionary.ContainsKey(PropertyNames.InnerText))
            {
                return this.GetWebElementsByInnerText(dictionary);
            }
            else
            {
                return this.Parent.FindElements(
                    By.CssSelector(this.GetCssSelector(dictionary)));
            }
        }

        private IEnumerable<IWebElement> GetWebElementsByInnerText(Dictionary<string, string> dictionary)
        {
            ReadOnlyCollection<IWebElement> matchingElements = this.Parent.FindElements(
                By.CssSelector(this.GetCssSelector(dictionary)));

            if (dictionary[PropertyNames.InnerText].Contains(ContainsSufix))
            {
                return matchingElements.Where(item => item.Text.Contains(dictionary[PropertyNames.InnerText]));
            }
            else
            {
                return matchingElements.Where(item => item.Text.Equals(dictionary[PropertyNames.InnerText]));
            }
        }

        private string GetCssSelector(Dictionary<string, string> dictionary)
        {
            string attributeTemplate = "[{0}=\"{1}\"]";
            IEnumerable<string> attributes = dictionary
                .Where(item => item.Key != PropertyNames.TagName)
                .Where(item => item.Key != PropertyNames.InnerText)
                .Select(item => string.Format(attributeTemplate, item.Key, item.Value));

            return dictionary[PropertyNames.TagName] + string.Join(string.Empty, attributes);
        }

        private Dictionary<string, string> GetSearchPropertiesDictionary(PropertyExpressionCollection searchProperties)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            foreach (PropertyExpression searchProperty in searchProperties)
            {
                string key = searchProperty.PropertyName.ToLowerInvariant();

                if (searchProperty.PropertyOperator == PropertyExpressionOperator.Contains)
                {
                    key += ContainsSufix;
                }

                dictionary.Add(key, searchProperty.PropertyValue);
            }

            return dictionary;
        }

        public abstract class PropertyNames
        {
            public static readonly string AccessKey = "accesskey";
            public static readonly string Class = "class";
            public static readonly string ControlDefinition = "controldefinition";
            public static readonly string HelpText = "helptext";
            public static readonly string Id = "id";
            public static readonly string InnerText = "innertext";
            public static readonly string TagInstance = "taginstance";
            public static readonly string Title = "title";
            public static readonly string Type = "type";
            public static readonly string ValueAttribute = "value";
            public static readonly string TagName = "tagname";

            [EditorBrowsable(EditorBrowsableState.Never)]
            public static new bool ReferenceEquals(object objA, object objB)
            {
                return true;
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            public static new bool Equals(object objA, object objB)
            {
                return true;
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            public abstract override bool Equals(object obj);

            [EditorBrowsable(EditorBrowsableState.Never)]
            public abstract override int GetHashCode();

            [EditorBrowsable(EditorBrowsableState.Never)]
            public abstract new Type GetType();

            [EditorBrowsable(EditorBrowsableState.Never)]
            public abstract override string ToString();
        }
    }
}