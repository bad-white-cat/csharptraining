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
    
        public void TestContactInformationTableAndForm()
        {
            int contactNumber = 6;

            app.Contacts.CreateIfNotExists(contactNumber);//checking if such contact exists 

            ContactData fromTable = app.Contacts.GetContactInformationFromTable(contactNumber);
            ContactData fromForm = app.Contacts.GetContactInformationEditForm(contactNumber);

            Assert.AreEqual(fromTable.Firstname, fromForm.Firstname);
            Assert.AreEqual(fromTable.Lastname, fromForm.Lastname);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
            Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);
        }

        [Test]
        public void TestContactInformationSummaryAndForm()
        {
            int contactNumber = 6;

            app.Contacts.CreateIfNotExists(contactNumber);//checking if such contact exists 

            string fromSummary = app.Contacts.GetContactInformationFromSummary(contactNumber);
            string fromForm = app.Contacts.GetContactInformationFormToString(contactNumber);

            Console.Out.Write("Summary = " + fromSummary);
            Console.Out.Write("Form = " + fromForm);

            Assert.AreEqual(fromForm, fromSummary);

            /*Assert.AreEqual(fromTable.Firstname, fromForm.Firstname);
            Assert.AreEqual(fromTable.Lastname, fromForm.Lastname);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
            Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);*/
        }
    }
}
