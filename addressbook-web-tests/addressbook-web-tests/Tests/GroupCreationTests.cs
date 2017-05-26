using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : TestBase
    {
        [Test]
        public void GroupCreationTest()
        {
            //creation test data
            GroupData group = new GroupData("gr_name");
            group.Footer = "gr_comment";
            group.Header = "gr_logo";
            //creation new group
            app.Groups.Create(group);
            app.Auth.LogOut();
        }

        [Test]
        public void EmptyGroupCreationTest()
        {
            //creation test data
            GroupData group = new GroupData("");
            group.Footer = "";
            group.Header = "";
            //creation new group
            app.Groups.Create(group);
            app.Auth.LogOut();
        }
    }
}
