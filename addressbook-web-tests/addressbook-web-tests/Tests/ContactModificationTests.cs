using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : TestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            //preparing data to replace
            ContactData newData = new ContactData("Alistair", "Theirin");
            newData.Middlename = "";
            newData.Address = "Thedas";
            newData.Nickname = "Templar";
            //group modification
            app.Contact.Modify(4, newData);
        }
    }
}
