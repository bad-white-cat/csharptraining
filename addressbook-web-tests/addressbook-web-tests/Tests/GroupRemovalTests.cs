using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : TestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            app.Navigator.GoToGroupPage();
            app.Groups
                .SelectGroup(2)
                .RemoveGroup()
                .ReturnToGroupPage();
            //app.Auth.LogOut();
        }
    }
}
