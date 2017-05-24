using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class ApplicationManager
    {

        
        protected IWebDriver driver;
        protected StringBuilder verificationErrors;
        protected string baseURL;

        protected NavigationHelper navigator;
        protected GroupHelper groupHelper;
        protected ContactHelper contactHelper;
        protected LoginHelper loginHelper;

        public ApplicationManager()
        {

            driver = new FirefoxDriver(new FirefoxBinary("C:\\01-Programs\\ff\\firefox-sdk\\bin\\firefox.exe"), new FirefoxProfile());
            baseURL = "http://localhost:8080/";

            loginHelper = new LoginHelper(this);
            navigator = new NavigationHelper(this, baseURL);
            contactHelper = new ContactHelper(this);
            groupHelper = new GroupHelper(this);
        }

        public IWebDriver Driver
        {
            get
            {
                return driver;
            }
        }

        public void Stop()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }
        public LoginHelper Auth
        {
            get
            {
                return loginHelper;
            }
        }
        public ContactHelper Contact
        {
            get
            {
                return contactHelper;
            }
        }

        public NavigationHelper Navigator
        {
            get
            {
                return navigator;
            }
        }

       public GroupHelper Groups
        {
            get
            {
                return groupHelper;
            }
        }
    }
}
