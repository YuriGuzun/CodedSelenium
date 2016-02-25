using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodedSelenium
{
    public class UITestControlCollection : ICollection<UITestControl>, IEnumerable<UITestControl>, IEnumerable
    {
        private List<UITestControl> testConrols = new List<UITestControl>();

        public int Count
        {
            get
            {
                return this.testConrols.Count;
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
                return this.testConrols[index];
            }

            set
            {
                this.testConrols[index] = value;
            }
        }

        public IEnumerator<UITestControl> GetEnumerator()
        {
            return this.testConrols.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Clear()
        {
            this.testConrols.Clear();
        }

        public bool Contains(UITestControl testControl)
        {
            return this.testConrols.Contains(testControl);
        }

        public void CopyTo(UITestControl[] array, int arrayIndex)
        {
            this.testConrols.CopyTo(array, arrayIndex);
        }

        public bool Remove(UITestControl item)
        {
            return this.testConrols.Remove(item);
        }

        public void Add(UITestControl item)
        {
            this.testConrols.Add(item);
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