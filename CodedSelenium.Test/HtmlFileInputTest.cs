using CodedSelenium.HtmlControls;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodedSelenium.Test
{
    [TestClass]
    public class HtmlFileInputTest : BasicTest
    {
        [TestMethod]
        public void HtmlFileInputTest_SelectedItem()
        {
            HtmlFileInput fileInput = new HtmlFileInput(BrowserWindow);
            fileInput.FileName = HtmlFileInputTest.PathToPage;
            fileInput.FileName.Should().EndWith(HtmlFileInputTest.PageName);
            this.AssertResult("uploadHere", "change");
        }
    }
}
