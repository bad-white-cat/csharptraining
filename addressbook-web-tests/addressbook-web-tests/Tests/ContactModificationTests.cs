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

            app.Contact.CreateIfNotExists(ContactLineNumber);//checking if such contact exists 

            List<ContactData> oldContacts = app.Contact.GetContactsList(); //old contact list recording
            app.Contact.Modify(ContactLineNumber, newData);//сontact modification
            List<ContactData> newContacts = app.Contact.GetContactsList(); //new contact list recording

            oldContacts[ContactLineNumber].Firstname = newData.Firstname; //modify contact in collection
            oldContacts[ContactLineNumber].Lastname = newData.Lastname; //modify contact in collection

            oldContacts.Sort(); 
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
