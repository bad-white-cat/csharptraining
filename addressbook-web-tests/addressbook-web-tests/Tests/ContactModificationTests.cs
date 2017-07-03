using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : ContactTestBase
    {
        [Test, TestCaseSource(typeof(TestBase), "ContactDataFromXmlFile")]
        public void ContactModificationTest(ContactData newData)
        {
            int ContactLineNumber = 0; //contact line number to modify (starting from 0);

            app.Contacts.CreateIfNotExists(ContactLineNumber);//checking if such contact exists 

            List<ContactData> oldContacts = ContactData.GetAllContacts(); //old contact list recording

            foreach (ContactData c in oldContacts)
            {
                Console.Out.Write("Old contact 1 = " + c.Fullname);
            };

            ContactData oldData = oldContacts[ContactLineNumber]; //saving state of modified contact 

            app.Contacts.ModifyByObject(oldData, newData);//сontact modification

            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactsCount()); //number of contacts hasn't changed

            List<ContactData> newContacts = ContactData.GetAllContacts(); //new contact list recording

            oldContacts[ContactLineNumber].Firstname = newData.Firstname; //modify contact in collection
            oldContacts[ContactLineNumber].Lastname = newData.Lastname; //modify contact in collection
            /*oldData.Address = newData.Address;
            oldData.EMail = newData.EMail;
            oldData.EMail2 = newData.EMail2;
            oldData.EMail3 = newData.EMail3;
            oldData.Mobile = newData.Mobile;
            oldData.WorkPhone = newData.WorkPhone;
            oldData.HomePhone = newData.HomePhone;*/

            oldContacts.Sort(); 
            foreach (ContactData c in oldContacts)
            {
                Console.Out.Write("Old contact 2 = " + c.Fullname);
            };

            newContacts.Sort();
            foreach (ContactData c in newContacts)
            {
                Console.Out.Write("New contact = " + c.Fullname);
            };

            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                if (contact.Id == oldData.Id)
                {
                    Assert.AreEqual(newData.Firstname, contact.Firstname);
                    Assert.AreEqual(newData.Lastname, contact.Lastname);
                }
            }
        }
    }
}
