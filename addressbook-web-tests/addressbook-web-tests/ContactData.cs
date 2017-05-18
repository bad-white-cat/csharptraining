using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    class ContactData
    {
        private string name1;
        private string name2 = "";
        private string name3;
        private string nickname = "";
        private string company = "";
        private string address = "";

        public ContactData(string name1, string name2)
        {
            this.name1 = name1;
            this.name2 = name2;
        }

        public string Name1
        {
            get
            {
                return name1;
            }

            set
            {
                name1 = value;
            }
        }

        public string Name2
        {
            get
            {
                return name2;
            }

            set
            {
                name2 = value;
            }
        }
        public string Name3
        {
            get
            {
                return name3;
            }

            set
            {
                name3 = value;
            }
        }

        public string Address
        {
            get
            {
                return address;
            }

            set
            {
                address = value;
            }
        }
        public string Nickname
        {
            get
            {
                return nickname;
            }

            set
            {
                nickname = value;
            }
        }

        public string Company
        {
            get
            {
                return company;
            }

            set
            {
                company = value;
            }
        }

    }
}
