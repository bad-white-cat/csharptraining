using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
   public class ContactData : IEquatable<ContactData>, IComparable<ContactData> // objects of GroupData type can be compared 
    {
        private string firstname;
        private string middlename = "";
        private string lastname;
        private string nickname = "";
        private string company = "";
        private string address = "";
 

        public ContactData(string firstname, string lastname)
        {
            this.firstname = firstname;
            this.middlename = lastname;
        }

        public bool Equals(ContactData other) //"other" stands to object to compare current object 
        {
            if (Object.ReferenceEquals(other, null)) //object exists
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other)) //object isn't compared with itself
            {
                return true;
            }
            return (Fullname == other.Fullname); //contacts comparison by first and last names
        }

        public override int GetHashCode()
        {
          return Fullname.GetHashCode();
        }

        public override string ToString()
        {
            return "name = " + Fullname;
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return Fullname.CompareTo(other.Fullname);
        }

        public string Firstname
        {
            get
            {
                return firstname;
            }

            set
            {
                firstname = value;
            }
        }

        public string Middlename
        {
            get
            {
                return middlename;
            }

            set
            {
                middlename = value;
            }
        }
        public string Lastname
        {
            get
            {
                return lastname;
            }

            set
            {
                lastname = value;
            }
        }

        public string Fullname
        {
            get
            {
                return firstname + " " + lastname;
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
