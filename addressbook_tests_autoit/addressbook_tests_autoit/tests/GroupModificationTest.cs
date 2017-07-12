using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace addressbook_tests_autoit
{
    [TestFixture]
    public class GroupModificationTest : TestBase
    {
        [Test]
        public void TestCroupModification()
        {

            GroupData newGr = new GroupData() {
                Name = "newname"
            };

            List<GroupData> oldGroups = app.Groups.CreateIfNotExists(1);

            System.Console.Out.Write(oldGroups.Count);
            
            foreach (GroupData group in oldGroups)
            {
                System.Console.Out.Write("Old group = " + group.Name);
            }

            app.Groups.Modify(1, newGr);
            List<GroupData> newGroups = app.Groups.GetGroupsList();
            oldGroups[1].Name = newGr.Name;

            foreach (GroupData group in newGroups)
            {
                System.Console.Out.Write("New group = " + group.Name);
            }

            oldGroups.Sort();
            newGroups.Sort();

            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}