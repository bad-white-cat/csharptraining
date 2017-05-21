using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class GroupData
    {
        private string group_name;
        private string group_logo = "";
        private string group_comment = "";

        public GroupData(string group_name)
        {
            this.group_name = group_name;
        }

        public string Group_name
        {
            get
            {
                return group_name;
            }

            set
            {
                group_name = value;
            }
        }

        public string Group_logo
        {
            get
            {
                return group_logo;
            }

            set
            {
                group_logo = value;
            }
        }

        public string Group_comment
        {
            get
            {
                return group_comment;
            }

            set
            {
                group_comment = value;
            }
        }
    }
}
