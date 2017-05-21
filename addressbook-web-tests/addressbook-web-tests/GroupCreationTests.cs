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
            GoToHomePage();
            Login(new AccountData("admin", "secret"));
            GoToGroupPage();
            InitNewGroupCreation();
            GroupData group = new GroupData("gr_name");
            group.Group_comment = "gr_comment";
            group.Group_logo = "gr_logo";
            FillGroupForm(group);
            SubmitGroupCreation();
            ReturnToGroupPage();
            LogOut();
        }
    }
}
