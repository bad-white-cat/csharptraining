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


        //low-level methods
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
            string middleName = driver.FindElement(By.Name("firstname")).GetAttribute("value");

            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string mobile = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");
            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");

            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            return new ContactData(firstName, lastName)
            {
                Address = address,
                Mobile = mobile,
                HomePhone = homePhone,
                WorkPhone = workPhone,
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

        public int GetNumberOfResults()
        {
            manager.Navigator.GoToHomePage();
            string text = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d+").Match(text);
            return Int32.Parse(m.Value);
        }
    }
}
