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

            //checking if any contact exists 
            app.Contact.CreateIfNotExists(1);

            //сontact modification
            app.Contact.Modify(1, newData);
        }
    }
}
