using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData> // objects of GroupData type can be compared 
    {

        public string allPhones;
        public string allEmails;
        public ContactData()
        {

        }
        public ContactData(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }

        [Column(Name = "firstname")]
        public string Firstname { get; set; }

        [Column(Name = "middlename")]
        public string Middlename { get; set; }

        [Column(Name = "lastname")]
        public string Lastname { get; set; }

        public string Fullname
        {
            get
            {
                return Firstname + Lastname;
            } 
        }

        [Column(Name = "address")]
        public string Address { get; set; }

        [Column(Name = "nickname")]
        public string Nickname { get; set; }

        [Column(Name = "company")]
        public string Company { get; set; }

        [Column(Name = "mobile")]
        public string Mobile { get; set; }

        [Column(Name = "work")]
        public string WorkPhone { get; set; }

        [Column(Name = "home")]
        public string HomePhone { get; set; }

        [Column(Name = "phone2")]
        public string Phone2 { get; set; }

        [Column(Name = "email")]
        public string EMail { get; set; }

        [Column(Name = "email2")]
        public string EMail2 { get; set; }

        [Column(Name = "email3")]
        public string EMail3 { get; set; }

        [Column(Name = "id"), PrimaryKey, Identity]
        public string Id { get; set; }

        public static List<ContactData> GetAllContacts()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts select c).ToList();
            }
        }

        public string AllPhones {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUp(HomePhone) + CleanUp(Mobile) + CleanUp(WorkPhone) + CleanUp(Phone2).Trim());
                }
            }
            set
            {
                allPhones = value;
            }
        }

        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    return (CleanUp(EMail) + CleanUp(EMail2) + CleanUp(EMail3)).Trim();
                }
            }

            set
            {
                allEmails = value;
            }
        }

        private string CleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, "[ -()]","") + "\r\n";
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
            return "name = " + Fullname + "\nAddress " + Address + "\nEmail " + EMail + "\nEmail2 " + EMail2 + "\nEmai3 " + EMail3 + "\nMobile + " + Mobile + "\n Workphone" + WorkPhone
                + "\nHomephone " + HomePhone;
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return Fullname.CompareTo(other.Fullname);
        }
    
    }
}
