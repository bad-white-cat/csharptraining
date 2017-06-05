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
            GroupData newData = new GroupData("gr_name1");
            newData.Footer = "gr_comment1";
            newData.Header = "gr_logo1";
            int GroupLineNumber = 0; //group line number to modify starting from 0;
                        
            app.Groups.CreateIfNotExists(GroupLineNumber);//check if group of needed number exists 

            List<GroupData> oldGroups = app.Groups.GetGroupList();//groups count before modification
            
            app.Groups.Modify(GroupLineNumber, newData);//group modification

            List<GroupData> newGroups = app.Groups.GetGroupList();//groups count after modification

            oldGroups[GroupLineNumber].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups); //new and old lists comparison
        }
    }
}
