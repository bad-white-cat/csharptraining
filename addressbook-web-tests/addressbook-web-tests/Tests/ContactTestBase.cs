using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class ContactTestBase : AuthTestBase
    {
        [TearDown]
        public void CompareContactUI_Db()
        {
            if (PERFORM_LONG_UI_CHECKS)
            {
                List<ContactData> fromUi = app.Contacts.GetContactsList();
                List<ContactData> fromDb = ContactData.GetAllContacts();
                fromUi.Sort();
                fromDb.Sort();
                Assert.AreEqual(fromUi, fromDb);
            }

        }
    }
}
