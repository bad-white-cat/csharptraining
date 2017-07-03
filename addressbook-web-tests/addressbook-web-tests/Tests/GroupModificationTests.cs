using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : GroupTestBase
    {

        [Test, TestCaseSource(typeof(TestBase), "GroupDataFromExcelFile")]
        public void GroupModificationTest(GroupData newData)
        {
            int GroupNumberToModify = 3;

            app.Groups.CreateIfNotExists(GroupNumberToModify);//check if group of needed number exists 

            List<GroupData> oldGroups = GroupData.GetAll();//groups count before modification from database

            GroupData toBeModified = oldGroups[GroupNumberToModify]; //group object to be modified in the given massive
            
            app.Groups.ModifyByObject(toBeModified, newData);//group modification on UI by given object

            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());

            List<GroupData> newGroups = GroupData.GetAll();//groups massive after modification

            toBeModified.Name = newData.Name; //group in old collection modification
            toBeModified.Header = newData.Header;
            toBeModified.Footer = newData.Header; 

            Assert.AreEqual(oldGroups, newGroups); //new and old lists comparison

            foreach (GroupData gr in newGroups)
            {
                if (gr.Id == toBeModified.Id)
                {
                    Assert.AreEqual(newData.Name, gr.Name);
                    Assert.AreEqual(newData.Header, gr.Header);
                    Assert.AreEqual(newData.Footer, gr.Footer);
                }
            }
        }
    }
}
