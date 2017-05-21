using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class TestBase
    {
        protected IWebDriver driver;
        protected StringBuilder verificationErrors;
        protected string baseURL;

        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver(new FirefoxBinary("C:\\01-Programs\\ff\\firefox-sdk\\bin\\firefox.exe"), new FirefoxProfile());
            baseURL = "http://localhost:8080/";
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        //opening application and log in
        protected void GoToHomePage()
        {
            driver.Navigate().GoToUrl(baseURL + "addressbook/");
        }
        protected void Login(AccountData account)
        {
            driver.FindElement(By.Name("user")).Clear();
            driver.FindElement(By.Name("user")).SendKeys(account.Username);
            driver.FindElement(By.Name("pass")).Clear();
            driver.FindElement(By.Name("pass")).SendKeys(account.Password);
            driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();
        }
        //working with contacts

        //new contact creation
        protected void InitNewContactCreation()
        {
            driver.FindElement(By.LinkText("ADD_NEW")).Click();
        }
        protected void ContactCreation(ContactData contact)
        {
            driver.FindElement(By.Name("firstname")).Clear();
            driver.FindElement(By.Name("firstname")).SendKeys(contact.Name1);
            driver.FindElement(By.Name("middlename")).Clear();
            driver.FindElement(By.Name("middlename")).SendKeys(contact.Name2);
            driver.FindElement(By.Name("lastname")).Clear();
            driver.FindElement(By.Name("lastname")).SendKeys(contact.Name2);
            driver.FindElement(By.Name("nickname")).Clear();
            driver.FindElement(By.Name("nickname")).SendKeys(contact.Nickname);
            driver.FindElement(By.Name("company")).Clear();
            driver.FindElement(By.Name("company")).SendKeys(contact.Company);
            driver.FindElement(By.Name("address")).Clear();
            driver.FindElement(By.Name("address")).SendKeys(contact.Address);

        }

        //contact submitting
        protected void SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
        }

        protected void ReturnToHomepage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
        }

        //working with groups
        protected void GoToGroupPage()
        {
            driver.FindElement(By.LinkText("GROUPS")).Click();
        }
        //New group creation methods
        protected void InitNewGroupCreation()
        {
            driver.FindElement(By.Name("new")).Click();
        }
        protected void FillGroupForm(GroupData group)
        {
            driver.FindElement(By.Name("group_name")).Clear();
            driver.FindElement(By.Name("group_name")).SendKeys(group.Group_name);
            driver.FindElement(By.Name("group_header")).Clear();
            driver.FindElement(By.Name("group_header")).SendKeys(group.Group_logo);
            driver.FindElement(By.Name("group_footer")).Clear();
            driver.FindElement(By.Name("group_footer")).SendKeys(group.Group_comment);
        }
        protected void SubmitGroupCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
        }

        //selecting a group
        protected void SelectGroup(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]")).Click();
        }

        //removing group
        protected void RemoveGroup()
        {
            driver.FindElement(By.XPath("(//input[@name='delete'])[2]")).Click();
        }
    
        //returning to group page
        protected void ReturnToGroupPage()
        {
            driver.FindElement(By.LinkText("group page")).Click();
        }

        //logout 
        protected void LogOut()
        {
            driver.FindElement(By.LinkText("LOGOUT")).Click();
        }

    }
}
