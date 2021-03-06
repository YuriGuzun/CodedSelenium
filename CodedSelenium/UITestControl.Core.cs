﻿using CodedSelenium.Selectors;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;

namespace CodedSelenium
{
    /// <summary>
    /// Non Coded UI content
    /// </summary>
    public partial class UITestControl : SelectorBasedControl
    {
        private static Dictionary<ModifierKeys, string> _modifierKeysDictionary = new Dictionary<ModifierKeys, string>()
        {
            { ModifierKeys.Alt, OpenQA.Selenium.Keys.Alt },
            { ModifierKeys.Control, OpenQA.Selenium.Keys.LeftControl },
            { ModifierKeys.Shift, OpenQA.Selenium.Keys.Shift },
            { ModifierKeys.None, string.Empty },
        };

        private WebDriverWait _webDriverWait;

        public UITestControl ParentTestControl { get; private set; }

        public long ScrollWheelPosition
        {
            get
            {
                BrowserWindow browserWindow = TopParent as BrowserWindow;
                long currentPosition = (long)browserWindow.Driver.ExecuteScript(
                    string.Format("return {0}.scrollTop({1})", AbsolutePathSelector.Selector, string.Empty));

                return currentPosition;
            }

            set
            {
                BrowserWindow browserWindow = TopParent as BrowserWindow;
                browserWindow.Driver.ExecuteScript(
                    string.Format("{0}.scrollTop({1})", AbsolutePathSelector.Selector, value));
            }
        }

        public virtual IWebElement WebElement
        {
            get
            {
                IWebElement webElement = InternalWebElement;
                if (webElement == null)
                {
                    string message =
                        "Unable to find ui control control matching search criteria:\r\n" +
                        "\tSearchProperties: " + SearchProperties.ToString();

                    if (_filterProperties != null && FilterProperties.Count != 0)
                    {
                        message += "\tFilterProperties: " + FilterProperties.ToString();
                    }

                    throw new UITestControlNotFoundException(message);
                }

                return webElement;
            }
        }

        protected ISearchContext ParentSearchContext { get; set; }

        protected WebDriverWait Wait
        {
            get
            {
                if (_webDriverWait == null)
                {
                    _webDriverWait = new WebDriverWait((TopParent as BrowserWindow).Driver, TimeSpan.FromSeconds(2));
                    _webDriverWait.PollingInterval = TimeSpan.FromMilliseconds(0);
                }

                return _webDriverWait;
            }
        }

        private IWebElement InternalWebElement
        {
            get
            {
                if (_privateWebElement != null)
                {
                    return _privateWebElement;
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
                _privateWebElement = WebElement;
            }
        }

        private AbsolutePathSelector AbsolutePathSelector
        {
            get
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

                return new AbsolutePathSelector(searchContext as IWebDriver, thisElementSelector);
            }
        }

        internal void DragAndDropTo(UITestControl control)
        {
            BrowserWindow browserWindow = TopParent as BrowserWindow;
            new Actions(browserWindow.Driver).DragAndDrop(this.WebElement, control.WebElement).Perform();
        }

        internal void DragAndDropTo(int moveByX, int moveByY)
        {
            BrowserWindow browserWindow = TopParent as BrowserWindow;
            new Actions(browserWindow.Driver).DragAndDropToOffset(this.WebElement, moveByX, moveByY).Perform();
        }

        internal virtual void MoveToElement(Point? relativeCoordinate)
        {
            BrowserWindow browserWindow = TopParent as BrowserWindow;
            Actions actions = new Actions(browserWindow.Driver);

            if (relativeCoordinate.HasValue)
            {
                if (relativeCoordinate.Value.X < 0 || relativeCoordinate.Value.Y < 0)
                {
                    throw new ArgumentOutOfRangeException("relativeCoordinate", relativeCoordinate.Value.ToString());
                }

                actions.MoveToElement(WebElement, relativeCoordinate.Value.X, relativeCoordinate.Value.Y);
            }
            else
            {
                actions.MoveToElement(WebElement);
            }

            actions.Perform();
        }

        internal virtual void Click(MouseButtons button, ModifierKeys modifierKeys, Point? relativeCoordinate)
        {
            BrowserWindow browserWindow = TopParent as BrowserWindow;

            if (button == MouseButtons.Left && modifierKeys == ModifierKeys.None && WebElement != null && !relativeCoordinate.HasValue)
                WebElement.Click();
            else
            {
                Actions actions = new Actions(browserWindow.Driver);
                MoveToElement(relativeCoordinate);
                actions = ApplyModifiers(actions, button, modifierKeys);
                actions.Perform();
            }
        }

        internal void DoubleClick(ModifierKeys modifierKeys, Point? relativeCoordinate)
        {
            BrowserWindow browserWindow = TopParent as BrowserWindow;

            Actions actions = new Actions(browserWindow.Driver);
            MoveToElement(relativeCoordinate);

            if (!_modifierKeysDictionary.ContainsKey(modifierKeys))
                throw new NotImplementedException(string.Format("'ModifierKeys.{0}' is not supported", modifierKeys.ToString()));

            string keyToPress = _modifierKeysDictionary[modifierKeys];
            if (modifierKeys == ModifierKeys.None)
                actions.DoubleClick().Perform();
            else
                actions.KeyDown(keyToPress).DoubleClick().KeyUp(keyToPress).Perform();
        }

        internal virtual string GetSelector()
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
            IJavaScriptExecutor driver = AbsolutePathSelector.Driver as IJavaScriptExecutor;

            ReadOnlyCollection<IWebElement> webElements = null;
            object queryResponse = driver.ExecuteScript("return " + AbsolutePathSelector.Selector);

            if (queryResponse is ReadOnlyCollection<IWebElement>)
            {
                webElements = (ReadOnlyCollection<IWebElement>)queryResponse;
            }

            return webElements;
        }

        private Actions ApplyModifiers(Actions actions, MouseButtons button, ModifierKeys modifierKeys)
        {
            if (!_modifierKeysDictionary.ContainsKey(modifierKeys))
                throw new NotImplementedException(string.Format("'ModifierKeys.{0}' is not supported", modifierKeys.ToString()));

            string keyToPress = _modifierKeysDictionary[modifierKeys];

            if (modifierKeys != ModifierKeys.None)
                actions = actions.KeyDown(keyToPress);

            switch (button)
            {
                case MouseButtons.Left:
                    actions = actions.Click();
                    break;

                case MouseButtons.Right:
                    actions = actions.ContextClick();
                    break;

                default:
                    throw new NotImplementedException(string.Format("'MouseButtons.{0}' is not supported", button.ToString()));
            }

            if (modifierKeys != ModifierKeys.None)
                actions = actions.KeyUp(keyToPress);

            return actions;
        }
    }
}
