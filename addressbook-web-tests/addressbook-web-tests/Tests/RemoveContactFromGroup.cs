using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class RemoveContactFromGroupTests : AuthTestBase
    {
        [Test]
        public void RemoveContactFromGroup()
        {
            GroupData group = GroupData.GetAll()[0]; //taking the group
            //Console.Out.Write("Group to work = " + group.Name);
            
            List<ContactData> oldContactsListInTheGroup = group.GetContacts(); //taking list of contacts in that group
            /*foreach (ContactData cont in oldContactsListInTheGroup)
            {
                Console.Out.Write("Contacts to work = " + cont.Fullname);
            }*/

            ContactData contactToRemove = oldContactsListInTheGroup[0]; //contact to remove from group

            //Console.Out.Write("Contact to remove = " + contactToRemove.Fullname);

            app.Contacts.RemoveContactFromGroup(contactToRemove, group);

            List<ContactData> newContactsListInTheGroup = group.GetContacts();
            oldContactsListInTheGroup.Remove(contactToRemove);
            oldContactsListInTheGroup.Sort();
            /*foreach (ContactData cont in oldContactsListInTheGroup)
            {
                Console.Out.Write("Contacts in old group = " + cont.Fullname);
            }*/
            newContactsListInTheGroup.Sort();
            /*foreach (ContactData cont in oldContactsListInTheGroup)
            {
                Console.Out.Write("Contacts in new group = " + cont.Fullname);
            }*/
            Assert.AreEqual(oldContactsListInTheGroup, oldContactsListInTheGroup);
        }
    }

}
