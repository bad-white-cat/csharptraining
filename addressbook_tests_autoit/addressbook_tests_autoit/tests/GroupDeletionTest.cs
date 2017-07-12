using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace addressbook_tests_autoit
{
    [TestFixture]
    public class GroupDeletionTest : TestBase
    {
        [Test]
        public void TestGroupDeletion()
        {
            List<GroupData> oldGroups = app.Groups.CreateIfNotExists(1);

            app.Groups.Remove(1);
            List<GroupData> newGroups = app.Groups.GetGroupsList();
            oldGroups.RemoveAt(1);
            oldGroups.Sort();
            newGroups.Sort();

            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
