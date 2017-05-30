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
            //checking if any contact exists 
            app.Contact.CreateIfNotExists(1);
            //removing the first contact
            app.Contact.Remove(1);
        }
    }
}
