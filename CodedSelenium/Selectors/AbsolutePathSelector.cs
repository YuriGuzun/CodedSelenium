using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodedSelenium.Selectors
{
    public class AbsolutePathSelector
    {
        public AbsolutePathSelector(IWebDriver driver, string selector)
        {
            Driver = driver;
            Selector = selector;
        }

        public IWebDriver Driver { get; private set; }

        public string Selector { get; private set; }
    }
}
