using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodedSelenium.Selectors
{
    public class SelectorPart
    {
        public SelectorPart(string selector, FilterType filter)
        {
            Value = selector;
            Filter = filter;
        }

        public enum FilterType
        {
            ContentFilter,
            FunctionFilter,
            Method,
            ExplicitParent
        }

        public string Value { get; private set; }

        public FilterType Filter { get; private set; }
    }
}
