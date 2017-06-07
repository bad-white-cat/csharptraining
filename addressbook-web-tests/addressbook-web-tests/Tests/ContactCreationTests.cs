using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;


namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
        {
        [Test]
        public void ContactCreationTest()
        {
            //Data creation for new contact, the rest are empty
            ContactData contact = new ContactData("Mur1", "Murmur")
                {
                Middlename = "Murmurych",
                Nickname = "The Cat",
                Company = "The Cat Company",
                Address = "113 Cat Street, Moortown"
                };

            List<ContactData> oldContacts = app.Contact.GetContactsList();
            app.Contact.Create(contact);//Contact data creation
            List<ContactData> newContacts = app.Contact.GetContactsList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);
        }

        [Test]
        public void EmptyContactCreationTest()
        {
            //Data creation for new contact, the rest are empty
            ContactData contact = new ContactData("", "");

            List<ContactData> oldContacts = app.Contact.GetContactsList();
            app.Contact.Create(contact);//Contact data creation
            List<ContactData> newContacts = app.Contact.GetContactsList();
            
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();

           Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
