using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : AuthTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            //preparing data to replace
            GroupData newData = new GroupData("gr_name1");
            newData.Footer = "gr_comment1";
            newData.Header = "gr_logo1";
            int GroupLineNumber = 8; //group line number to modify
            
            //check if group of needed number exists 
            app.Groups.CreateIfNotExists(GroupLineNumber);
            //group modification
            app.Groups.Modify(GroupLineNumber, newData);
        }
    }
}
