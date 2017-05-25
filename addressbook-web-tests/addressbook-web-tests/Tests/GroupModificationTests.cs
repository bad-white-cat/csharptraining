using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : TestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            //preparing data to replace
            GroupData newData = new GroupData("gr_name1");
            newData.Group_comment = "gr_comment1";
            newData.Group_logo = "gr_logo1";
            //group modification
            app.Groups.Modify(1, newData) 
            {

            }
        }
    }
}
