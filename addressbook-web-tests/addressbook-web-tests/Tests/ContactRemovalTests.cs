using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests

{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            int ContactLineNumber = 0; //contact line number to remove (starting from 0);
            
            app.Contacts.CreateIfNotExists(ContactLineNumber); //checking if selected contact exists 

            List<ContactData> oldContacts = app.Contacts.GetContactsList(); 

            app.Contacts.Remove(ContactLineNumber);//removing requested contact
            Assert.AreEqual(oldContacts.Count - 1, app.Contacts.GetContactsCount());

            List<ContactData> newContacts = app.Contacts.GetContactsList();
            ContactData toBeRemoved = oldContacts[ContactLineNumber];

            oldContacts.RemoveAt(ContactLineNumber);
            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                Assert.AreNotEqual(toBeRemoved.Id, contact.Id);
            }
        }
    }
}
