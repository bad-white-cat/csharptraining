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
        [Test]
        public void GroupModificationTest()
        {
            //preparing data to replace
            GroupData newData = new GroupData("gr_name1")
            {
                Footer = "gr_comment1",
                Header = "gr_logo1"
            };

            int GroupLineNumber = 0; //group line number to modify starting from 0;
                        
            app.Groups.CreateIfNotExists(GroupLineNumber);//check if group of needed number exists 

            List<GroupData> oldGroups = app.Groups.GetGroupList();//groups count before modification
            GroupData oldData = oldGroups[GroupLineNumber];
            
            app.Groups.Modify(GroupLineNumber, newData);//group modification
            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();//groups count after modification

            oldGroups[GroupLineNumber].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups); //new and old lists comparison

            foreach (GroupData group in newGroups)
            {
                if (group.Id == oldData.Id)
                {
                    Assert.AreEqual(newData.Name, group.Name);
                }
            }
        }
    }
}
