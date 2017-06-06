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
            int ContactLineNumber = 6; //contact line number to remove (starting from 0);
            
            app.Contact.CreateIfNotExists(ContactLineNumber); //checking if selected contact exists 

            List<ContactData> oldContacts = app.Contact.GetContactsList(); 
            app.Contact.Remove(ContactLineNumber);//removing requested contact
            List<ContactData> newContacts = app.Contact.GetContactsList();
            oldContacts.RemoveAt(ContactLineNumber);
            oldContacts.Sort();
            newContacts.Sort();

            /*int i = 0;
            foreach (ContactData element in oldContacts)
            {
                Console.Out.Write("old contact" + i + "= " + element.Fullname);
                i++;
            }

            int j = 0;
            foreach (ContactData element in newContacts)
            {
                Console.Out.Write("new contact" + j + "= " + element.Fullname);
                j++;
            }*/

            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
