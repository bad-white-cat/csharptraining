using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            //preparing data to replace
            ContactData newData = new ContactData("Дядя", "Степа")
            {
                Middlename = "Милиционер",
                Address = "у заставы Ильича",
                Nickname = "Каланча"
            };

            int ContactLineNumber = 2; //contact line number to modify (starting from 0);

            app.Contacts.CreateIfNotExists(ContactLineNumber);//checking if such contact exists 

            List<ContactData> oldContacts = app.Contacts.GetContactsList(); //old contact list recording
            ContactData oldData = oldContacts[ContactLineNumber]; //saving state of modified contact 

            app.Contacts.Modify(ContactLineNumber, newData);//сontact modification

            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactsCount()); //number of contacts hasn't changed

            List<ContactData> newContacts = app.Contacts.GetContactsList(); //new contact list recording

            oldContacts[ContactLineNumber].Firstname = newData.Firstname; //modify contact in collection
            oldContacts[ContactLineNumber].Lastname = newData.Lastname; //modify contact in collection
            oldContacts.Sort(); 
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                if (contact.Id == oldData.Id)
                {
                    Assert.AreEqual(newData.Fullname, contact.Fullname);
                }
            }
        }
    }
}
