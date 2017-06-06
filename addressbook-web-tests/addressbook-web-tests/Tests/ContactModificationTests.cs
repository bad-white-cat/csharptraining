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
            ContactData newData = new ContactData("Alistair2", "Theirin2");
            newData.Middlename = "";
            newData.Address = "Thedas2";
            newData.Nickname = "Templar";

            int ContactLineNumber = 6; //contact line number to remove (starting from 0);

            app.Contact.CreateIfNotExists(ContactLineNumber);//checking if any contact exists 

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
