using CodedSelenium.Selectors;
using OpenQA.Selenium;
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
        private JQuerySelector selector;

        public UITestControl ParentTestControl { get; private set; }

        protected ISearchContext ParentSearchContext { get; set; }

        protected virtual JQuerySelector Selector
        {
            get
            {
                if (this.selector == null)
                {
                    this.selector = new JQuerySelector(this.SearchProperties, this.FilterProperties);
                }

                return this.selector;
            }
        }

        protected virtual IWebElement WebElement
        {
            get
            {
                IWebElement webElement = this.InternalWebElement;
                if (webElement == null)
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

                return webElement;
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
                if (webElements == null)
                {
                    return null;
                }

                return webElements.FirstOrDefault();
            }

            set
            {
                this.privateWebElement = this.WebElement;
            }
        }

        public virtual void Click()
        {
            this.WebElement.Click();
        }

        protected virtual string GetSelector()
        {
            string thisElementSelector = this.BuildSelector(this.SearchProperties);

            if (this.FilterProperties.Count > 0)
            {
                PropertyExpressionCollection searchAndFilterProperties = new PropertyExpressionCollection();
                searchAndFilterProperties.AddRange(this.SearchProperties);
                searchAndFilterProperties.AddRange(this.FilterProperties);
                thisElementSelector = string.Format(
                    "{0}.length > 1 ? {1} : {0}", thisElementSelector, this.BuildSelector(searchAndFilterProperties));
            }

            return thisElementSelector;
        }

        private ReadOnlyCollection<IWebElement> FindMatchingWebElements()
        {
            string thisElementSelector = this.GetSelector();

            UITestControl parentControl = this.ParentTestControl;
            ISearchContext searchContext = this.ParentSearchContext;
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
