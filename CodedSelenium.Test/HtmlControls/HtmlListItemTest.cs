using CodedSelenium.HtmlControls;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace CodedSelenium.Test.HtmlControls
{
    [TestFixture]
    public class HtmlListItemTest : BasicTest
    {
        private HtmlList _list;

        private HtmlListItem _listItem;

        private HtmlList List
        {
            get
            {
                if (_list == null)
                {
                    _list = new HtmlList(BasicTestPage);
                    _list.SearchProperties.Add(HtmlList.PropertyNames.Id, "htmlList");
                }

                return _list;
            }
        }

        private HtmlListItem ListItem
        {
            get
            {
                if (_listItem == null)
                {
                    _listItem = new HtmlListItem(List);
                    _listItem.SearchProperties.Add(HtmlList.PropertyNames.Instance, "1");
                }

                return _listItem;
            }
        }

        [Test]
        public void HtmlListItemTest_SelectItem()
        {
            ListItem.Should().NotBeNull();

            ListItem.Select();

            ListItem.Selected.Should().BeTrue();

            ListItem.DisplayText.Should().Be(ListItem.InnerText);

            List.SelectedItems.Should().Equal(new string[] { ListItem.InnerText });
        }
    }
}
