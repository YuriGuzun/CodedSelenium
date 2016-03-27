using System.Collections;
using System.Collections.Generic;

namespace CodedSelenium
{
    public class UITestControlCollection : ICollection<UITestControl>, IEnumerable<UITestControl>, IEnumerable
    {
        private List<UITestControl> _testControls = new List<UITestControl>();

        public int Count
        {
            get
            {
                return _testControls.Count;
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
                return _testControls[index];
            }

            set
            {
                _testControls[index] = value;
            }
        }

        public string[] GetValuesOfControls()
        {
            List<string> values = new List<string>();
            foreach (UITestControl control in _testControls)
            {
                values.Add(control.InnerText);
            }

            return values.ToArray();
        }

        public IEnumerator<UITestControl> GetEnumerator()
        {
            return _testControls.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Clear()
        {
            _testControls.Clear();
        }

        public bool Contains(UITestControl testControl)
        {
            return _testControls.Contains(testControl);
        }

        public void CopyTo(UITestControl[] array, int arrayIndex)
        {
            _testControls.CopyTo(array, arrayIndex);
        }

        public bool Remove(UITestControl item)
        {
            return _testControls.Remove(item);
        }

        public void Add(UITestControl item)
        {
            _testControls.Add(item);
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
