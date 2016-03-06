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
                return propertyExpressions.Count;
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
                return propertyExpressions.FirstOrDefault(item => item.PropertyName == propertyName).PropertyValue;
            }

            set
            {
                Add(new PropertyExpression(propertyName, value));
            }
        }

        public IEnumerator<PropertyExpression> GetEnumerator()
        {
            return propertyExpressions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Clear()
        {
            propertyExpressions.Clear();
        }

        public bool Contains(PropertyExpression propertyExpression)
        {
            return propertyExpressions.Contains(propertyExpression);
        }

        public void CopyTo(PropertyExpression[] array, int arrayIndex)
        {
            propertyExpressions.CopyTo(array, arrayIndex);
        }

        public bool Remove(PropertyExpression propertyExpression)
        {
            PropertyExpression existingItem = propertyExpressions
                .Find(item => item.PropertyName.Equals(propertyExpression.PropertyName));
            return propertyExpressions.Remove(existingItem);
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
                Add(nameValuePairs[i], nameValuePairs[i + 1]);
            }
        }

        public void Add(PropertyExpression propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }

            Remove(propertyExpression);
            propertyExpressions.Add(propertyExpression);
        }

        public void Add(string propertyName, string propertyValue)
        {
            Add(new PropertyExpression(propertyName, propertyValue));
        }

        public void Add(string propertyName, string propertyValue, PropertyExpressionOperator conditionOperator)
        {
            Add(new PropertyExpression(propertyName, propertyValue, conditionOperator));
        }

        public void AddRange(params PropertyExpression[] propertyExpressions)
        {
            foreach (PropertyExpression propertyExpression in propertyExpressions)
            {
                Add(propertyExpression);
            }
        }

        public void AddRange(PropertyExpressionCollection collectionToAdd)
        {
            AddRange(collectionToAdd.ToArray());
        }

        public override string ToString()
        {
            return string.Join(" and ", propertyExpressions.Select(item => item.ToString()));
        }
    }
}
