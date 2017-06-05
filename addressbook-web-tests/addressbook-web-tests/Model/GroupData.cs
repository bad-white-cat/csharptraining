using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class GroupData : IEquatable<GroupData>, IComparable<GroupData> // objects of GroupData type can be compared 
    {
        private string group_name;
        private string group_logo = "";
        private string group_comment = "";

        public GroupData(string group_name)
        {
            this.group_name = group_name;
        }

        public bool Equals(GroupData other) //"other" stands to object to compare current object 
        {
            if (Object.ReferenceEquals(other, null)) //object exists
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other)) //object isn't compared with itself
            {
                return true;
            }
            return Name == other.Name; //group comparison by name
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return "name = "+Name;
        }

        public int CompareTo(GroupData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return Name.CompareTo(other.Name);
        }

        public string Name
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

        public string Header
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

        public string Footer
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
