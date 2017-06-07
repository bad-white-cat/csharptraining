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
            return this;
        }
        
        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            return this;
        }
        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }

        //Additional methods for different checks
        public void CreateIfNotExists(int index)
        {
            while (!IsElementPresent(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")))
            {
                ContactData contact = new ContactData("Cassandra", "Penthagast");
                Create(contact);
                //Console.Out.Write("Created Firstname " + contact.Firstname + " Middlename "+ contact.Middlename + " Lastname " + contact.Lastname);
            }
        }

        internal List<ContactData> GetContactsList()
        {
            List<ContactData> contacts = new List<ContactData>();
            ICollection<IWebElement> rows = driver.FindElements(By.XPath("//tr[@name='entry']"));
            foreach (IWebElement row in rows)
            {
                var firstName = row.FindElement(By.XPath("td[3]")).Text;
                var lastName = row.FindElement(By.XPath("td[2]")).Text;
                contacts.Add(new ContactData(firstName, lastName));
            }
            return contacts;
        }
    }
}
