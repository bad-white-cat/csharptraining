using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : AuthTestBase
    {
        [Test]
        public void GroupCreationTest()
        {
            //creation test data
            GroupData group = new GroupData("gr_name1")
            {
                Footer = "gr_comment1",
                Header = "gr_logo1"
            };

            List<GroupData> oldGroups = app.Groups.GetGroupList();//groups count before creation new one

            app.Groups.Create(group);//creation new group

            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount()); //new and old lists length comparison

            List<GroupData> newGroups = app.Groups.GetGroupList();//groups count after creation new one

            oldGroups.Add(group); //adding new group data to old list
            oldGroups.Sort();
            newGroups.Sort();

            Assert.AreEqual(oldGroups, newGroups); //new and old lists comparison
        }

        [Test]
        public void EmptyGroupCreationTest()
        {
            //creation test data
            GroupData group = new GroupData("")
            {
                Footer = "",
                Header = ""
            };

            List<GroupData> oldGroups = app.Groups.GetGroupList();//groups count before creation new one
            
            app.Groups.Create(group);//creation new group

            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount()); //new and old lists length comparison
                                    
            List<GroupData> newGroups = app.Groups.GetGroupList();//groups count after creation new one

            oldGroups.Add(group); //adding new group data to old list
            oldGroups.Sort();
            newGroups.Sort();

            Assert.AreEqual(oldGroups, newGroups); //new and old lists comparison
        }

        [Test]
        public void BadNameCreationTest()
        {
            //creation test data
            GroupData group = new GroupData("a'a")
            {
                Footer = "",
                Header = ""
            };

            List<GroupData> oldGroups = app.Groups.GetGroupList();//groups count before creation new one

            app.Groups.Create(group);//creation new group

            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount()); //new and old lists length comparison

            List<GroupData> newGroups = app.Groups.GetGroupList();//groups count after creation new one

            oldGroups.Add(group); //adding new group data to old list
            oldGroups.Sort();
            newGroups.Sort();

            Assert.AreEqual(oldGroups, newGroups); //new and old lists comparison
        }
    }
}
