using CodedSelenium.Selectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodedSelenium.Selectors
{
    public class SelectorBasedControl
    {
        private Dictionary<string, Func<PropertyExpression, SelectorPart>> rulesDictionary;

        protected virtual Dictionary<string, Func<PropertyExpression, SelectorPart>> RulesDictionary
        {
            get
            {
                if (this.rulesDictionary == null)
                {
                    this.rulesDictionary = new Dictionary<string, Func<PropertyExpression, SelectorPart>>()
                    {
                        { UITestControl.PropertyNames.Instance, this.ByInstance },
                        { UITestControl.PropertyNames.TagInstance, this.ByTagInstance },
                        { UITestControl.PropertyNames.InnerText, this.ByInnerText }
                    };
                }

                return this.rulesDictionary;
            }
        }

        protected string BuildSelector(PropertyExpressionCollection propertyExpressions)
        {
            string tagName = string.Empty;
            string explicitParent = string.Empty;
            List<string> contentFilters = new List<string>();
            List<string> functionFilters = new List<string>();
            List<string> methods = new List<string>();
            List<string> attributes = new List<string>();

            foreach (PropertyExpression propertyExpression in propertyExpressions)
            {
                if (propertyExpression.PropertyName.Equals(UITestControl.PropertyNames.TagName))
                {
                    tagName = propertyExpression.PropertyValue;
                    continue;
                }

                if (this.RulesDictionary.ContainsKey(propertyExpression.PropertyName))
                {
                    SelectorPart selectorPart = this.RulesDictionary[propertyExpression.PropertyName].Invoke(propertyExpression);

                    switch (selectorPart.Filter)
                    {
                        case SelectorPart.FilterType.ContentFilter:
                            contentFilters.Add(selectorPart.Value);
                            break;

                        case SelectorPart.FilterType.FunctionFilter:
                            functionFilters.Add(selectorPart.Value);
                            break;

                        case SelectorPart.FilterType.Method:
                            methods.Add(selectorPart.Value);
                            break;

                        case SelectorPart.FilterType.ExplicitParent:
                            explicitParent = selectorPart.Value;
                            break;
                    }
                }
                else
                {
                    string containsSign = propertyExpression.PropertyOperator == PropertyExpressionOperator.Contains ? "*" : string.Empty;
                    attributes.Add(string.Format("[{0}{1}={2}]", propertyExpression.PropertyName, containsSign, propertyExpression.PropertyValue));
                }
            }

            string filter = string.Empty;
            if (functionFilters.Count != 0)
            {
                string functionTemplate = ".filter(function() { return %scriptStatements%;})";
                filter = functionTemplate.Replace("%scriptStatements%", string.Join(" && ", functionFilters));
            }

            string selector = string.Format(
                "jQuery(\"{0}{1}{2}\"%parent%){3}{4}",
                tagName,
                string.Join(string.Empty, attributes),
                string.Join(string.Empty, contentFilters),
                filter,
                string.Join(string.Empty, methods));

            if (!string.IsNullOrEmpty(explicitParent))
            {
                selector = selector.Replace("%parent%", explicitParent);
            }

            return selector;
        }

        private SelectorPart ByInstance(PropertyExpression propertyExpression)
        {
            int instance = 0;
            if (int.TryParse(propertyExpression.PropertyValue, out instance))
            {
                return new SelectorPart(string.Format(":eq({0})", instance - 1), SelectorPart.FilterType.ContentFilter);
            }

            throw new ArgumentOutOfRangeException("PropertyNames.Instance");
        }

        private SelectorPart ByTagInstance(PropertyExpression propertyExpression)
        {
            int tagInstance = 0;
            if (int.TryParse(propertyExpression.PropertyValue, out tagInstance))
            {
                return new SelectorPart(string.Format(":nth-of-type(({0})", tagInstance - 1), SelectorPart.FilterType.ContentFilter);
            }

            throw new ArgumentOutOfRangeException("PropertyNames.TagInstance");
        }

        private SelectorPart ByInnerText(PropertyExpression propertyExpression)
        {
            if (propertyExpression.PropertyOperator == PropertyExpressionOperator.Contains)
            {
                string selector = string.Format(":contains({0})", propertyExpression.PropertyValue);
                return new SelectorPart(selector, SelectorPart.FilterType.ContentFilter);
            }
            else
            {
                string selector = string.Format("($(this).text() === \"{0}\")", propertyExpression.PropertyValue);
                return new SelectorPart(selector, SelectorPart.FilterType.FunctionFilter);
            }
        }
    }
}
