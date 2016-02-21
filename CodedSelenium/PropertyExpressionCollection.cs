using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public bool Remove(PropertyExpression item)
        {
            return this.propertyExpressions.Remove(item);
        }

        public void Add(params string[] nameValuePairs)
        {
            int count = nameValuePairs.Count();
            if ((count % 2) != 0)
            {
                throw new Exception("Incomplete pair. Number of items passed - " + count);
            }

            for (int i = 0; i < count; i += 2)
            {
                this.Add(nameValuePairs[i], nameValuePairs[i + 1]);
            }
        }

        public void Add(PropertyExpression propertyExpression)
        {
            this.Add(propertyExpression);
        }

        public void Add(string propertyName, string propertyValue)
        {
            this.propertyExpressions.Add(new PropertyExpression(propertyName, propertyValue));
        }

        public void Add(string propertyName, string propertyValue, PropertyExpressionOperator conditionOperator)
        {
            this.propertyExpressions.Add(new PropertyExpression(propertyName, propertyValue, conditionOperator));
        }

        public void AddRange(params PropertyExpression[] propertyExpressions)
        {
            this.propertyExpressions.AddRange(propertyExpressions);
        }

        public void AddRange(PropertyExpressionCollection collectionToAdd)
        {
            this.propertyExpressions.AddRange(collectionToAdd.Select(item => item));
        }
    }
}