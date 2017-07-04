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
            app.Groups.CreateIfNotExists(0);

            GroupData group = GroupData.GetAll()[0];
            
            
            List<ContactData> oldContactsListInTheGroup = group.GetContacts(); //taking list of contacts in that group

            ContactData contactToRemove = null;

            if (oldContactsListInTheGroup.Count > 0) //if there are some contacts in the group
            {
                contactToRemove = oldContactsListInTheGroup[0];
            }
            else if (ContactData.GetAllContacts().Count > 0) //if there are contacts in the address book, but not in the group
            {
                contactToRemove = ContactData.GetAllContacts().First();
                app.Contacts.AddContactToGroup(contactToRemove, group);
                oldContactsListInTheGroup = group.GetContacts();
            }
            else //there are no contacts in the address book 
            {
                app.Contacts.CreateIfNotExists(0);//checking if such contact exists 
                contactToRemove = ContactData.GetAllContacts().First();
                app.Contacts.AddContactToGroup(contactToRemove, group);
                oldContactsListInTheGroup = group.GetContacts();
            }

            app.Contacts.RemoveContactFromGroup(contactToRemove, group);

            List<ContactData> newContactsListInTheGroup = group.GetContacts();
            oldContactsListInTheGroup.Remove(contactToRemove);
            oldContactsListInTheGroup.Sort();
            newContactsListInTheGroup.Sort();
            Assert.AreEqual(oldContactsListInTheGroup, oldContactsListInTheGroup);
        }
    }

}
