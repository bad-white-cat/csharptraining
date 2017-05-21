using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
        { 
        [Test]
        public void ContactCreationTest()
        {
            GoToHomePage();
            Login(new AccountData("admin", "secret"));
            InitNewContactCreation();

            //Data creation for new contact, the rest are empty
            ContactData contact = new ContactData("Mur1", "Murmur");
            contact.Name2 = "Murmurych";
            contact.Nickname = "The Cat";
            contact.Company = "The Cat Company";
            contact.Address = "113 Cat Street, Moortown";
            //Filling contact details and submitting
            ContactCreation(contact);
            SubmitContactCreation();
            ReturnToHomepage();
            LogOut();
        }
    }
}
