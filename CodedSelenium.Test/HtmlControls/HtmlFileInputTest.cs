using CodedSelenium.HtmlControls;
using FluentAssertions;
using NUnit.Framework;

namespace CodedSelenium.Test.HtmlControls
{
    [TestFixture]
    public class HtmlFileInputTest : BasicTest
    {
        [Test]
        public void HtmlFileInputTest_SelectedItem()
        {
            HtmlFileInput fileInput = new HtmlFileInput(BasicTestPage);
            fileInput.FileName = HtmlFileInputTest.PathToPage;
            fileInput.FileName.Should().EndWith(HtmlFileInputTest.PageName);
            AssertResult("uploadHere", "change");
        }
    }
}
