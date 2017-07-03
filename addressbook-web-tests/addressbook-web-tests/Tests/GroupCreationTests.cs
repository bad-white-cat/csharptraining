using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : GroupTestBase
    {

        [Test, TestCaseSource(typeof(TestBase), "GroupDataFromExcelFile")]
        public void GroupCreationTest(GroupData group)
        {
           List<GroupData> oldGroups = GroupData.GetAll(); ;//groups count before creation new one

            app.Groups.Create(group);//creation new group

            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount()); //new and old lists length comparison

            List<GroupData> newGroups = GroupData.GetAll(); //groups count after creation new one

            oldGroups.Add(group); //adding new group data to old list
            oldGroups.Sort();
            newGroups.Sort();

            Assert.AreEqual(oldGroups, newGroups); //new and old lists comparison
        }

        [Test]
        public void TestDBConnectivity()
        {
            foreach (ContactData contact in GroupData.GetAll()[1].GetContacts())
            {
                System.Console.Out.WriteLine(contact.Deprecated);
            }
        }
    }
}
