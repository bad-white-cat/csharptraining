using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;



namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        private const string deleteOneContactPattern = "^Delete 1 addresses[\\s\\S]$"; //alert text for deleting one contact

       public ContactHelper(ApplicationManager manager)
            : base(manager)
        {
        }


        //high-level methods
        public ContactHelper Create(ContactData contact)
        {
            InitNewContactCreation();
            FillContactData(contact);
            SubmitContactCreation();
            manager.Navigator.ReturnToHomepage();
            return this;
        }

        internal ContactHelper ModifyByObject(ContactData oldData, ContactData newData)
        {
            InitContactModification(oldData.Id);
            FillContactData(newData);
            SubmitContactModification();
            manager.Navigator.ReturnToHomepage();
            return this;
        }

        public ContactHelper Modify(int p, ContactData newdata)
        {
            InitContactModification(p);
            FillContactData(newdata);
            SubmitContactModification();
            manager.Navigator.ReturnToHomepage();
            return this;
        }
            

        public ContactHelper Remove(int v)
        {
            SelectContact(v);
            RemoveContact();
            ConfirmAction();
            manager.Navigator.ReturnToHomepage();
            return this;
        }

        public ContactHelper RemoveByObject(ContactData contact)
        {
            SelectContact(contact.Id);
            RemoveContact();
            ConfirmAction();
            manager.Navigator.ReturnToHomepage();
            return this;
        }

        public ContactHelper AddContactToGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.GoToHomePage();
            ClearGroupFilter();
            SelectContactById(contact.Id);
            SelectGroupToAdd(group.Name);
            CommitAddingContactToGroup();
            return this;
        }

        //low-level methods

        private void CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        private void SelectGroupToAdd(string name)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
        }

        private void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[ALL]");
        }

        public ContactHelper InitNewContactCreation()
        {
            driver.FindElement(By.LinkText("ADD_NEW")).Click();
            return this;
        }

        public ContactHelper InitContactModification(int v)
        {
            driver.FindElement(By.XPath("(//img[@alt='EDIT'])[" + (v+1) + "]")).Click();
            return this;
        }

        public ContactHelper InitContactModification(string contactId)
        {
            driver.FindElement(By.XPath("//a[@href='edit.php?id=" + contactId + "']")).Click();
            return this;
        }

        public ContactHelper OpenContactSummary(int v)
        {
            driver.FindElement(By.XPath("(//img[@alt='DETAILS'])[" + (v + 1) + "]")).Click();
            return this;
        }

        public ContactHelper FillContactData(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("middlename"), contact.Middlename);
            Type(By.Name("lastname"), contact.Lastname);
            Type(By.Name("nickname"), contact.Nickname);
            Type(By.Name("company"), contact.Company);
            Type(By.Name("address"), contact.Address);
            Type(By.Name("home"), contact.HomePhone);
            Type(By.Name("mobile"), contact.Mobile);
            Type(By.Name("work"), contact.WorkPhone);
            Type(By.Name("email"), contact.EMail);
            Type(By.Name("email2"), contact.EMail2);
            Type(By.Name("email3"), contact.EMail3);
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index+1) + "]")).Click();
            return this;
        }

        public ContactHelper SelectContact(string contactId)
        {
            driver.FindElement(By.Id(contactId)).Click();
            return this;
        }

        public ContactHelper SelectContactById(string id)
        {
            driver.FindElement(By.Id(id)).Click();
            return this;
        }

        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='DELETE']")).Click();
            return this;
        }
        public ContactHelper ConfirmAction()
        {
            Assert.IsTrue(Regex.IsMatch(CloseAlertAndGetItsText(), deleteOneContactPattern));
            contactCache = null;
            return this;
        }
        
        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            contactCache = null;
            return this;
        }
        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }

        //Additional methods for different checks
        private List<ContactData> contactCache = null; //contact cache initialization

        public void CreateIfNotExists(int index)
        {
            while (!IsElementPresent(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")))
            {
                ContactData contact = new ContactData("Cassandra", "Penthagast")
                {
                    Middlename = "Nevarra",
                    Address = "931 Mur Mur",
                    Mobile = "+375(29)1234567",
                    HomePhone = "+375(29)1234568",
                    WorkPhone = "+375(29)1234569",
                    EMail = "1@example.com",
                    EMail2 = "2@example.com",
                    EMail3 = "3@example.com"
                };
                Create(contact);
             }
        }

        public List<ContactData> GetContactsList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                ICollection<IWebElement> rows = driver.FindElements(By.XPath("//tr[@name='entry']"));
                foreach (IWebElement row in rows)
                {
                    var firstName = row.FindElement(By.XPath("td[3]")).Text;
                    var lastName = row.FindElement(By.XPath("td[2]")).Text;
                    contactCache.Add(new ContactData(firstName, lastName)
                    {
                        Id = row.FindElement(By.TagName("input")).GetAttribute("value")
                    });
                }
            }
            return new List<ContactData>(contactCache);
        }

        internal int GetContactsCount()
        {
            return driver.FindElements(By.XPath("//tr[@name='entry']")).Count;
        }

        public ContactData GetContactInformationEditForm(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactModification(index);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string mobile = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");
            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string phone2 = driver.FindElement(By.Name("phone2")).GetAttribute("value");

            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            return new ContactData(firstName, lastName)
            {
                Address = address,
                Mobile = mobile,
                HomePhone = homePhone,
                WorkPhone = workPhone,
                Phone2 = phone2,
                EMail = email,
                EMail2 = email2,
                EMail3 = email3
            };
        }

        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allEmails = cells[4].Text;
            string allPhones = cells[5].Text;

            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllPhones = allPhones,
                AllEmails = allEmails
            };
        }

        public string GetContactInformationFromSummary(int contactNumber)
        {
            manager.Navigator.GoToHomePage();
            OpenContactSummary(contactNumber);
            string summary = driver.FindElement(By.Id("content")).Text;
            string summaryShort = summary.Replace("\n", "").Replace("\r", "");
            return summaryShort;   
                }

        public string GetContactInformationFormToString(int index)
        {
            manager.Navigator.GoToHomePage();
            ContactData mainContactInformation = GetContactInformationEditForm(index);
            mainContactInformation.Nickname = driver.FindElement(By.Name("nickname")).GetAttribute("value");
            mainContactInformation.Company = driver.FindElement(By.Name("company")).GetAttribute("value");
            mainContactInformation.Middlename = driver.FindElement(By.Name("middlename")).GetAttribute("value");

            string firstname = "";
            if (mainContactInformation.Firstname != "")
            {
                firstname = mainContactInformation.Firstname + " ";
            }

            string middlename = "";
            if (mainContactInformation.Middlename != "")
            {
                middlename = mainContactInformation.Middlename + " ";
            }

            string title = driver.FindElement(By.Name("title")).GetAttribute("value");

            string homepage = "";
            if (driver.FindElement(By.Name("homepage")).GetAttribute("value") != "")
            {
                homepage = "Homepage:" + driver.FindElement(By.Name("homepage")).GetAttribute("value");
            }
            //phones handling 
            string homePhone = "";
            if (mainContactInformation.HomePhone != "")
            {
                homePhone = "H: " + mainContactInformation.HomePhone;
            }

            string mobile = "";
            if (mainContactInformation.Mobile != "")
            {
                mobile = "M: " + mainContactInformation.Mobile;
            }

            string workPhone = "";
            if (mainContactInformation.WorkPhone != "")
            {
                workPhone = "W: " + mainContactInformation.WorkPhone;
            }

            string fax = "";
            if (driver.FindElement(By.Name("fax")).GetAttribute("value") != "")
            {
                fax = "F: " + driver.FindElement(By.Name("fax")).GetAttribute("value");
            }

            string phone2 = "";
            if (mainContactInformation.Phone2 != "")
            {
                phone2 = "P: " + mainContactInformation.Phone2;
            }

            //birthday handling
            string bday = "";
            if (driver.FindElement(By.Name("bday")).FindElement(By.CssSelector("[selected]")).GetAttribute("value") != "0")
            {
                bday = driver.FindElement(By.Name("bday")).FindElement(By.CssSelector("[selected]")).GetAttribute("value");
            }

            string bmonth = "";
            if (driver.FindElement(By.Name("bmonth")).FindElement(By.CssSelector("[selected]")).GetAttribute("value") != "-")
            {
                bmonth = driver.FindElement(By.Name("bmonth")).FindElement(By.CssSelector("[selected]")).GetAttribute("value");
            }

            string byear = "";
               if (driver.FindElement(By.Name("byear")).GetAttribute("value") != "")
            {
               byear = driver.FindElement(By.Name("byear")).GetAttribute("value");
            }

            string birthday = "";
            if ((bday != "")||(bmonth != "")||(byear != ""))
            {
                if (bday != "")
                {
                    bday = bday + ". ";
                }

                if (byear != "")
                {
                    byear = byear + " (" + (DateTime.Now.Year - Int32.Parse(byear)) + ")";
                }

                birthday = "BIRTHDAY " + bday + bmonth.ToUpper() + byear;
            }

            //anniversary handling
            string aday = "";
            if (driver.FindElement(By.Name("aday")).FindElement(By.CssSelector("[selected]")).GetAttribute("value") != "0")
            {
                aday = driver.FindElement(By.Name("aday")).FindElement(By.CssSelector("[selected]")).GetAttribute("value");
            }

            string amonth = "";
            if (driver.FindElement(By.Name("amonth")).FindElement(By.CssSelector("[selected]")).GetAttribute("value") != "-")
            {
                amonth = driver.FindElement(By.Name("amonth")).FindElement(By.CssSelector("[selected]")).GetAttribute("value");
            }

            string ayear = "";
            if (driver.FindElement(By.Name("ayear")).GetAttribute("value") != "")
            {
                ayear = driver.FindElement(By.Name("ayear")).GetAttribute("value");
            }

            string anniversary = "";
            if ((aday != "") || (amonth != "") || (ayear != ""))
            {
                if (aday != "")
                {
                    aday = aday + ". ";
                }

                if (ayear != "")
                {
                    ayear = ayear + " (" + (DateTime.Now.Year - Int32.Parse(ayear)) + ")";
                }

                anniversary = "ANNIVERSARY "+ aday + amonth.ToUpper() + ayear;
            }

            //the rest
            string address2 = driver.FindElement(By.Name("address2")).GetAttribute("value");

            string notes = driver.FindElement(By.Name("notes")).GetAttribute("value");

            string summary = firstname + middlename + mainContactInformation.Lastname + mainContactInformation.Nickname +
                title + mainContactInformation.Company + mainContactInformation.Address + homePhone + mobile + workPhone
                + fax + mainContactInformation.EMail + mainContactInformation.EMail2 + mainContactInformation.EMail3 + homepage +
                birthday + anniversary + address2 + phone2 + notes;

            string summaryShort = summary;

            return summaryShort;
        }

        public int GetNumberOfResults()
        {
            manager.Navigator.GoToHomePage();
            string text = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d+").Match(text);
            return Int32.Parse(m.Value);
        }

    }
}
