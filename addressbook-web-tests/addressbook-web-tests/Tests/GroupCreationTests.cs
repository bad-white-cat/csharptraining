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

        [Test, TestCaseSource(typeof(TestBase), "GroupDataFromExcelFile")]
        public void GroupCreationTest(GroupData group)
        {
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
