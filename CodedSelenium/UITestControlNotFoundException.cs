using System;

namespace CodedSelenium
{
    public class UITestControlNotFoundException : Exception
    {
        public UITestControlNotFoundException(string message)
            : base(message)
        {
        }
    }
}
