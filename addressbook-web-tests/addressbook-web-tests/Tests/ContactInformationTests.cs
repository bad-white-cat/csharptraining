using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactInformationTests : AuthTestBase
    {
        [Test]
    
        public void TestContactInformation()
        {
            int contactNumber = 0;

            app.Contacts.CreateIfNotExists(contactNumber);//checking if such contact exists 

            ContactData fromTable = app.Contacts.GetContactInformationFromTable(contactNumber);
            ContactData fromForm = app.Contacts.GetContactInformationEditForm(contactNumber);

            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
            Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);
        }
   }
}
