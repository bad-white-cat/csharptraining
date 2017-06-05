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
            GroupData group = new GroupData("gr_name");
            group.Footer = "gr_comment";
            group.Header = "gr_logo";

            List<GroupData> oldGroups = app.Groups.GetGroupList();//groups count before creation new one

            app.Groups.Create(group);//creation new group

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
            GroupData group = new GroupData("");
            group.Footer = "";
            group.Header = "";
                        
            List<GroupData> oldGroups = app.Groups.GetGroupList();//groups count before creation new one
                     
            app.Groups.Create(group);//creation new group
                        
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
            GroupData group = new GroupData("a 'a");
            group.Footer = "";
            group.Header = "";

            List<GroupData> oldGroups = app.Groups.GetGroupList();//groups count before creation new one

            app.Groups.Create(group);//creation new group

            List<GroupData> newGroups = app.Groups.GetGroupList();//groups count after creation new one

            oldGroups.Add(group); //adding new group data to old list
            oldGroups.Sort();
            newGroups.Sort();

            Assert.AreEqual(oldGroups, newGroups); //new and old lists comparison
        }
    }
}
