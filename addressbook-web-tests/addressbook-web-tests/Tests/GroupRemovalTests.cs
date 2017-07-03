using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            int GroupNumberToRemove = 0; //group number to remove (starting from 0);

            //check if group of this number exists 
            app.Groups.CreateIfNotExists(GroupNumberToRemove);
            
            List<GroupData> oldGroups = GroupData.GetAll(); //getting groups massive from db

            /*foreach (GroupData gr in oldGroups)
            {
                Console.Out.WriteLine("Old group name = '{0}'", gr.Name);
            }*/

            GroupData toBeRemoved = oldGroups[GroupNumberToRemove]; //selecting group for deletion

            //Console.Out.WriteLine("To be removed name = '{0}'", toBeRemoved.Name);
                     
            app.Groups.RemoveByObject(toBeRemoved); //removing group of given number via UI

            Assert.AreEqual(oldGroups.Count - 1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = GroupData.GetAll(); //getting groups massive again from db

            oldGroups.RemoveAt(GroupNumberToRemove); //removing group of the same position from initial list 

            Assert.AreEqual(oldGroups, newGroups); //group lists comparison

            foreach (GroupData gr in newGroups)
            {
                Assert.AreNotEqual(gr.Id, toBeRemoved.Id);
            }
        }
    }
}
