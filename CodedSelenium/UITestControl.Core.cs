using CodedSelenium.Selectors;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodedSelenium
{
    /// <summary>
    /// Non Coded UI content
    /// </summary>
    public partial class UITestControl : SelectorBasedControl
    {
        public UITestControl ParentTestControl { get; private set; }

        protected ISearchContext ParentSearchContext { get; set; }

        protected virtual IWebElement WebElement
        {
            get
            {
                IWebElement webElement = InternalWebElement;
                if (webElement == null)
                {
                    string message =
                        "Unable to find ui control control matching search criteria:\r\n" +
                        "\tSearchProperties: " + SearchProperties.ToString();

                    if (filterProperties != null && FilterProperties.Count != 0)
                    {
                        message += "\tFilterProperties: " + FilterProperties.ToString();
                    }

                    throw new UITestControlNotFoundException(message);
                }

                return webElement;
            }
        }

        private IWebElement InternalWebElement
        {
            get
            {
                if (privateWebElement != null)
                {
                    return privateWebElement;
                }

                IEnumerable<IWebElement> webElements = FindMatchingWebElements();
                if (webElements == null)
                {
                    return null;
                }

                return webElements.FirstOrDefault();
            }

            set
            {
                privateWebElement = WebElement;
            }
        }

        public virtual void Click()
        {
            WebElement.Click();
        }

        protected virtual string GetSelector()
        {
            string thisElementSelector = BuildSelector(SearchProperties);

            if (FilterProperties.Count > 0)
            {
                PropertyExpressionCollection searchAndFilterProperties = new PropertyExpressionCollection();
                searchAndFilterProperties.AddRange(SearchProperties);
                searchAndFilterProperties.AddRange(FilterProperties);
                thisElementSelector = string.Format(
                    "{0}.length > 1 ? {1} : {0}", thisElementSelector, BuildSelector(searchAndFilterProperties));
            }

            return thisElementSelector;
        }

        private ReadOnlyCollection<IWebElement> FindMatchingWebElements()
        {
            string thisElementSelector = GetSelector();

            UITestControl parentControl = ParentTestControl;
            ISearchContext searchContext = ParentSearchContext;
            while (!(searchContext is IWebDriver))
            {
                thisElementSelector = thisElementSelector.Replace("%parent%", ", " + parentControl.GetSelector());
                parentControl = parentControl.ParentTestControl;
                searchContext = parentControl.ParentSearchContext;
            }

            thisElementSelector = thisElementSelector.Replace("%parent%", string.Empty);
            Debug.WriteLine(thisElementSelector);

            IJavaScriptExecutor driver = searchContext as IJavaScriptExecutor;

            ReadOnlyCollection<IWebElement> webElements = null;
            object queryResponse = driver.ExecuteScript("return " + thisElementSelector);

            if (queryResponse is ReadOnlyCollection<IWebElement>)
            {
                webElements = (ReadOnlyCollection<IWebElement>)queryResponse;
            }

            return webElements;
        }
    }
}
