using System;
using System.Collections.Generic;
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
            return this;
        }
        public ContactHelper Remove(string v)
        {
            SelectContact("v");
            RemoveContact();
            ConfirmAction();
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
            driver.FindElement(By.XPath("(//img[@alt='EDIT'])["+ v +"]")).Click();
            return this;
        }

        public ContactHelper FillContactData(ContactData contact)
        {
            driver.FindElement(By.Name("firstname")).Clear();
            driver.FindElement(By.Name("firstname")).SendKeys(contact.Firstname);
            driver.FindElement(By.Name("middlename")).Clear();
            driver.FindElement(By.Name("middlename")).SendKeys(contact.Middlename);
            driver.FindElement(By.Name("lastname")).Clear();
            driver.FindElement(By.Name("lastname")).SendKeys(contact.Middlename);
            driver.FindElement(By.Name("nickname")).Clear();
            driver.FindElement(By.Name("nickname")).SendKeys(contact.Nickname);
            driver.FindElement(By.Name("company")).Clear();
            driver.FindElement(By.Name("company")).SendKeys(contact.Company);
            driver.FindElement(By.Name("address")).Clear();
            driver.FindElement(By.Name("address")).SendKeys(contact.Address);
            return this;

        }

        public ContactHelper SelectContact(string index)
        {
            driver.FindElement(By.Id(index)).Click();
            return this;
        }

        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='DELETE']")).Click();
            return this;
        }
        public ContactHelper ConfirmAction()
        {
            driver.SwitchTo().Alert().Accept();
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
    }
}
