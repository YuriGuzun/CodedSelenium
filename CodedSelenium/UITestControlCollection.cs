using System.Collections;
using System.Collections.Generic;

namespace CodedSelenium
{
    public class UITestControlCollection : ICollection<UITestControl>, IEnumerable<UITestControl>, IEnumerable
    {
        private List<UITestControl> testControls = new List<UITestControl>();

        public int Count
        {
            get
            {
                return this.testControls.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public UITestControl this[int index]
        {
            get
            {
                return this.testControls[index];
            }

            set
            {
                this.testControls[index] = value;
            }
        }

        public string[] GetValuesOfControls()
        {
            List<string> values = new List<string>();
            foreach (UITestControl control in this.testControls)
            {
                values.Add(control.InnerText);
            }

            return values.ToArray();
        }

        public IEnumerator<UITestControl> GetEnumerator()
        {
            return this.testControls.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Clear()
        {
            this.testControls.Clear();
        }

        public bool Contains(UITestControl testControl)
        {
            return this.testControls.Contains(testControl);
        }

        public void CopyTo(UITestControl[] array, int arrayIndex)
        {
            this.testControls.CopyTo(array, arrayIndex);
        }

        public bool Remove(UITestControl item)
        {
            return this.testControls.Remove(item);
        }

        public void Add(UITestControl item)
        {
            this.testControls.Add(item);
        }

        public void AddRange(UITestControlCollection collectionToAdd)
        {
            foreach (UITestControl testControl in collectionToAdd)
            {
                this.Add(testControl);
            }
        }
    }
}
