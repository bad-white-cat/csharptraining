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
    public class NavigationHelper : HelperBase
    {
        private string baseURL;
        public NavigationHelper(ApplicationManager manager, string baseURL)
            : base(manager)
        {
            this.baseURL = baseURL;
        }

        public void GoToHomePage()
        {
            if (driver.Url == baseURL + "addressbook/")
            {
                return;
            }
            driver.Navigate().GoToUrl(baseURL + "addressbook/");
        }
        public void ReturnToHomepage()
        {
            if (driver.Url == baseURL + "addressbook/")
            {
                return;
            }

            else if (IsElementPresent(By.LinkText("home page")))
                {
                driver.FindElement(By.LinkText("home page")).Click();
                }

            else
            {
                while (!IsElementPresent(By.LinkText("LASTNAME")))
                {
                    System.Threading.Thread.Sleep(1000);
                }
            }
        
        }
        public void GoToGroupPage()
        {
            if (driver.Url == baseURL + "addressbook/group.php"
                && IsElementPresent(By.Name("new")))
            {
                return;
            }
            driver.FindElement(By.LinkText("GROUPS")).Click();
        }
    }
}
