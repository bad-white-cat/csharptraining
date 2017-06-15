using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : AuthTestBase
    {

        [Test, TestCaseSource(typeof(TestBase), "RandomGroupDataProvider")]
        public void GroupModificationTest(GroupData newData)
        {
            int GroupLineNumber = 3;

            app.Groups.CreateIfNotExists(GroupLineNumber);//check if group of needed number exists 

            List<GroupData> oldGroups = app.Groups.GetGroupList();//groups count before modification
            GroupData oldData = oldGroups[GroupLineNumber];
            
            app.Groups.Modify(GroupLineNumber, newData);//group modification

            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();//groups count after modification

            oldGroups[GroupLineNumber].Name = newData.Name; //contact in old collection modification
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups); //new and old lists comparison

            foreach (GroupData gr in newGroups)
            {
                if (gr.Id == oldData.Id)
                {
                    Assert.AreEqual(newData.Name, gr.Name);
                }
            }
        }
    }
}
