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
                return testControls.Count;
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
                return testControls[index];
            }

            set
            {
                testControls[index] = value;
            }
        }

        public string[] GetValuesOfControls()
        {
            List<string> values = new List<string>();
            foreach (UITestControl control in testControls)
            {
                values.Add(control.InnerText);
            }

            return values.ToArray();
        }

        public IEnumerator<UITestControl> GetEnumerator()
        {
            return testControls.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Clear()
        {
            testControls.Clear();
        }

        public bool Contains(UITestControl testControl)
        {
            return testControls.Contains(testControl);
        }

        public void CopyTo(UITestControl[] array, int arrayIndex)
        {
            testControls.CopyTo(array, arrayIndex);
        }

        public bool Remove(UITestControl item)
        {
            return testControls.Remove(item);
        }

        public void Add(UITestControl item)
        {
            testControls.Add(item);
        }

        public void AddRange(UITestControlCollection collectionToAdd)
        {
            foreach (UITestControl testControl in collectionToAdd)
            {
                Add(testControl);
            }
        }
    }
}
