using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoItX3Lib;

namespace addressbook_tests_autoit
{
    public class ApplicationManager
    {
        public static string winTitle = "Free Address Book";
        public AutoItX3 autx;
        private GroupHelper groupHelper;
        public ApplicationManager()
        {
            autx = new AutoItX3();
            autx.Run(@"C:\Program Files (x86)\GAS Softwares\Free Address Book\AddressBook.exe", "", autx.SW_SHOW);
            autx.WinWait(winTitle);
            autx.WinActivate(winTitle);
            autx.WinWaitActive(winTitle);
            groupHelper = new GroupHelper(this);
        }

        public void Stop()
        {
            autx.ControlClick(winTitle, "", "WindowsForms10.BUTTON.app.0.2c908d510");
        }

        public AutoItX3 Autx
        {
            get
            {
                return autx;
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
