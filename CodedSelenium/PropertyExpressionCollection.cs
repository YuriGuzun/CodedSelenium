using CodedSelenium.Selectors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CodedSelenium
{
    public class PropertyExpressionCollection : ICollection<PropertyExpression>, IEnumerable<PropertyExpression>, IEnumerable
    {
        private List<PropertyExpression> propertyExpressions = new List<PropertyExpression>();

        public int Count
        {
            get
            {
                return this.propertyExpressions.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public string this[string propertyName]
        {
            get
            {
                return this.propertyExpressions.FirstOrDefault(item => item.PropertyName == propertyName).PropertyValue;
            }

            set
            {
                this.Add(new PropertyExpression(propertyName, value));
            }
        }

        public IEnumerator<PropertyExpression> GetEnumerator()
        {
            return this.propertyExpressions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Clear()
        {
            this.propertyExpressions.Clear();
        }

        public bool Contains(PropertyExpression propertyExpression)
        {
            return this.propertyExpressions.Contains(propertyExpression);
        }

        public void CopyTo(PropertyExpression[] array, int arrayIndex)
        {
            this.propertyExpressions.CopyTo(array, arrayIndex);
        }

        public bool Remove(PropertyExpression propertyExpression)
        {
            PropertyExpression existingItem = this.propertyExpressions
                .Find(item => item.PropertyName.Equals(propertyExpression.PropertyName));
            return this.propertyExpressions.Remove(existingItem);
        }

        public void Add(params string[] nameValuePairs)
        {
            int count = nameValuePairs.Count();
            if ((count % 2) != 0)
            {
                throw new ArgumentOutOfRangeException("nameValuePairs");
            }

            for (int i = 0; i < count; i += 2)
            {
                this.Add(nameValuePairs[i], nameValuePairs[i + 1]);
            }
        }

        public void Add(PropertyExpression propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }

            this.Remove(propertyExpression);
            this.propertyExpressions.Add(propertyExpression);
        }

        public void Add(string propertyName, string propertyValue)
        {
            this.Add(new PropertyExpression(propertyName, propertyValue));
        }

        public void Add(string propertyName, string propertyValue, PropertyExpressionOperator conditionOperator)
        {
            this.Add(new PropertyExpression(propertyName, propertyValue, conditionOperator));
        }

        public void AddRange(params PropertyExpression[] propertyExpressions)
        {
            foreach (PropertyExpression propertyExpression in propertyExpressions)
            {
                this.Add(propertyExpression);
            }
        }

        public void AddRange(PropertyExpressionCollection collectionToAdd)
        {
            this.AddRange(collectionToAdd.ToArray());
        }

        public override string ToString()
        {
            return string.Join(" and ", this.propertyExpressions.Select(item => item.ToString()));
        }

        public JQuerySelector GetSelector()
        {
            string tagName = string.Empty;
            List<string> contentFilters = new List<string>();
            List<string> functionFilters = new List<string>();
            List<string> attributes = new List<string>();

            foreach (PropertyExpression p in this.propertyExpressions)
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
                        int tagInstance = 0;
                        if (int.TryParse(p.PropertyValue, out tagInstance))
                        {
                            contentFilters.Add(string.Format(":nth-child({0})", tagInstance - 1));
                        }

                        break;

                    case UITestControl.PropertyNames.Instance:
                        int instance = 0;
                        if (int.TryParse(p.PropertyValue, out instance))
                        {
                            contentFilters.Add(string.Format(":eq({0})", instance - 1));
                        }

                        break;

                    default:
                        string containsSign = p.PropertyOperator == PropertyExpressionOperator.Contains ? "*" : string.Empty;
                        attributes.Add(string.Format("[{0}{1}={2}]", p.PropertyName, containsSign, p.PropertyValue));
                        break;
                }
            }

            JQuerySelector selector = new JQuerySelector(tagName, contentFilters, functionFilters, attributes);

            return selector;
        }
    }
}
