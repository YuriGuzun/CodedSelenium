using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodedSelenium
{
    public static class Mouse
    {
        public static void Click(UITestControl control)
        {
            control.Click();
        }
    }
}