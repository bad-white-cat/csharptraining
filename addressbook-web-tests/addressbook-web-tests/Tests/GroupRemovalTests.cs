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
            int GroupLineNumber = 6; //group line number to remove (starting from 0);

            //check if group of this number exists 
            app.Groups.CreateIfNotExists(GroupLineNumber);
            
            List<GroupData> oldGroups = app.Groups.GetGroupList(); //getting group names list
            app.Groups.Remove(GroupLineNumber); //removing group of given number
            List<GroupData> newGroups = app.Groups.GetGroupList(); //getting group names list again
            oldGroups.RemoveAt(GroupLineNumber); //removing group of the same position from initial list 
            oldGroups.Sort();
            newGroups.Sort();

            /*int i = 0;
            foreach (GroupData element in oldGroups)
            {
                Console.Out.Write("old group" + i + "= " + element.Name);
                i++;
            }

            int j = 0;
            foreach (GroupData element in newGroups)
            {
                Console.Out.Write("new group" + j + "= " + element.Name);
                j++;
            }*/

            Assert.AreEqual(oldGroups, newGroups); //group lists comparison
        }
    }
}
