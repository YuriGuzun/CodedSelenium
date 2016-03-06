using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodedSelenium
{
    public class TestControl
    {
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
    }
}
