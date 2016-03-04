﻿using OpenQA.Selenium;
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
    public partial class UITestControl
    {
        private PropertyExpressionCollection searchProperties;
        private PropertyExpressionCollection filterProperties;
        private IWebElement privateWebElement;
        private List<string> propertyNamesToIgnore;

        public UITestControl()
        {
        }

        public UITestControl(UITestControl parent)
        {
            this.ParentTestControl = parent;

            if (parent.WebElement == null)
            {
                this.ParentSearchContext = parent.ParentSearchContext;
            }
            else
            {
                this.ParentSearchContext = parent.WebElement;
            }
        }

        public UITestControl(ISearchContext parent)
        {
            this.ParentSearchContext = parent;
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
            ReadOnlyCollection<IWebElement> matchingElements = this.FindMatchingWebElements();

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
            this.ParentSearchContext = controlToCopy.ParentSearchContext;
            this.privateWebElement = controlToCopy.privateWebElement;
            this.searchProperties = controlToCopy.searchProperties;
            this.filterProperties = controlToCopy.filterProperties;
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
