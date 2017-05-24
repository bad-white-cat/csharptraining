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
            group.Group_comment = "gr_comment";
            group.Group_logo = "gr_logo";
            //creation new group
            app.Groups.Create(group);
            //app.Auth.LogOut();
        }

        [Test]
        public void EmptyGroupCreationTest()
        {
            //creation test data
            GroupData group = new GroupData("");
            group.Group_comment = "";
            group.Group_logo = "";
            //creation new group
            app.Groups.Create(group);
            //app.Auth.LogOut();
        }
    }
}
