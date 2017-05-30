using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            int GroupLineNumber = 3; //group line number to remove

            //check if group of this number exists 
            app.Groups.CreateIfNotExists(GroupLineNumber);
            //removing needed group
            app.Groups.Remove(GroupLineNumber);
        }
    }
}
