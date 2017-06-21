using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class GroupData : IEquatable<GroupData>, IComparable<GroupData> // objects of GroupData type can be compared 
    {

        public GroupData()
        {

        }
        public GroupData(string group_name)
        {
            Name = group_name;
        }

        public string Name { get; set; }

        public string Header { get; set; }

        public string Footer { get; set; }

        public string Id { get; set; }

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
            return "name = "+Name + "\nheader = " + Header + "\nfooter = " + Footer;
        }

        public int CompareTo(GroupData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return Name.CompareTo(other.Name);
        }
        
    }
}
