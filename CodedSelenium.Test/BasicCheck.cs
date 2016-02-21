using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodedSelenium.HtmlControls;
using FluentAssert;
using System.IO;

namespace CodedSelenium.Test
{
    [TestClass]
    public class BasicCheck
    {
        [TestMethod]
        public void BasicCheck_HtmlEdit()
        {
            string pathToPage = Directory.GetCurrentDirectory() + "\\TestPages\\TestPage.html";
            BrowserWindow bw = BrowserWindow.Launch(pathToPage);

            HtmlDiv div = new HtmlDiv(bw);
            div.SearchProperties.Add(HtmlControl.PropertyNames.Id, "loginFields");
            HtmlEdit edit = new HtmlEdit(div);

            string value = "banana";
            edit.Text = value;
            edit.Text.ShouldBeEqualTo(value);
        }
    }
}
