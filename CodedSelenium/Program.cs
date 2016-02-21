using CodedSelenium.HtmlControls;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace CodedSelenium
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            BrowserWindow bw = BrowserWindow.Launch("https://s1-site06-stackatf.rxnova.com/en/Website-Sign-Up/Login-Form/");

            HtmlDiv div = new HtmlDiv(bw);
            div.SearchProperties.Add(HtmlControl.PropertyNames.Id, "loginFields");
            HtmlEdit b = new HtmlEdit(div);

            b.Text = "banana";
        }
    }
}