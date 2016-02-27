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
        private IWebElement webElement;
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
            this.webElement = webElement;
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
                return this.WebElement.Displayed;
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
            this.webElement = controlToCopy.webElement;
            this.searchProperties = controlToCopy.searchProperties;
            this.filterProperties = controlToCopy.filterProperties;
        }

        public virtual void Click()
        {
            this.WebElement.Click();
        }

        protected virtual string GetCssSelector(Dictionary<string, string> dictionary)
        {
            IEnumerable<string> attributes = dictionary
                .Where(item => !this.PropertyNamesToIgnoreByCssSelector.Contains(item.Key.Trim(ContainsSufix)))
                .Select(item => string.Format(AttributeTemplate, item.Key, item.Value));

            return dictionary[PropertyNames.TagName] + string.Join(string.Empty, attributes);
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
            IEnumerable<IWebElement> webElements = this.Parent.FindElements(
                By.CssSelector(this.GetCssSelector(dictionary)));

            KeyValuePair<string, string> innerTextPair = dictionary.FirstOrDefault(item => item.Key.Contains(PropertyNames.InnerText));

            if (!innerTextPair.Equals(default(KeyValuePair<string, string>)))
            {
                if (innerTextPair.Key.Contains(ContainsSufix))
                {
                    webElements = webElements.Where(item => item.Text.Contains(innerTextPair.Value));
                }
                else
                {
                    webElements = webElements.Where(item => item.Text.Equals(innerTextPair.Value));
                }
            }
            else if (dictionary.ContainsKey(PropertyNames.TagInstance))
            {
                webElements = this.GetWebElementsByTagInstance(dictionary);
            }

            if (dictionary.ContainsKey(PropertyNames.Instance))
            {
                int instance = int.Parse(dictionary[PropertyNames.Instance]);
                return new List<IWebElement>() { webElements.ElementAt(instance - 1) };
            }

            return webElements;
        }

        private IEnumerable<IWebElement> GetWebElementsByTagInstance(Dictionary<string, string> dictionary)
        {
            int tagInstance = int.Parse(dictionary[PropertyNames.TagInstance]);
            ReadOnlyCollection<IWebElement> webElements = this.Parent.FindElements(
                By.CssSelector(this.GetCssSelector(dictionary) + string.Format(":nth-of-type({0})", tagInstance)));

            return webElements;
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
            public static readonly string Class = "class";
            public static readonly string HelpText = "helptext";
            public static readonly string Id = "id";
            public static readonly string InnerText = "innertext";
            public static readonly string TagInstance = "taginstance";
            public static readonly string Title = "title";
            public static readonly string Type = "type";
            public static readonly string ValueAttribute = "value";
            public static readonly string TagName = "tagname";
            public static readonly string Instance = "instance";

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
