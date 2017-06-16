using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace WebAddressbookTests
{

    public class TestBase
    {
        protected ApplicationManager app;
        [SetUp]
        public void SetupApplicationManager()
        {
            app = ApplicationManager.GetInstance();
        }


        public static Random rnd = new Random();

        public static string RandomString(int maxLength)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < RandomInt(maxLength); i++)
            {
                builder.Append(Convert.ToChar(Convert.ToInt32(rnd.NextDouble() * 223 + 32)));
            }
            return builder.ToString();
        }

        public static string RandomAlphaNumericString(int maxLength)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, RandomInt(maxLength))
              .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }

        public static int RandomInt(int maxNumber)
        {
            return Convert.ToInt32(rnd.NextDouble() * maxNumber);
        }
        public static IEnumerable<GroupData> RandomGroupDataProvider()
        {
            List<GroupData> groups = new List<GroupData>();
            for (int i = 0; i < 5; i++)
            {
                groups.Add(new GroupData(RandomString(30))
                {
                    Header = RandomString(100),
                    Footer = RandomString(100)
                });
            }
            return groups;
        }
        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < 5; i++)
            {
                contacts.Add(new ContactData(RandomString(30), RandomString(30))
                {
                    Middlename = RandomString(30),
                    Address = RandomString(100),
                    EMail = RandomAlphaNumericString(30) + @"@" + RandomAlphaNumericString(30) + "." + RandomAlphaNumericString(3),
                    EMail2 = RandomAlphaNumericString(30) + @"@" + RandomAlphaNumericString(30) + "." + RandomAlphaNumericString(3),
                    EMail3 = RandomAlphaNumericString(30) + @"@" + RandomAlphaNumericString(30) + "." + RandomAlphaNumericString(3),
                    HomePhone = "+" + RandomInt(999) + "(" + RandomInt(99) + ")" + RandomInt(999) + "-" + RandomInt(99) + "-" + RandomInt(99),
                    Mobile = "+" + RandomInt(999) + "(" + RandomInt(99) + ")" + RandomInt(999) + "-" + RandomInt(99) + "-" + RandomInt(99),
                    WorkPhone = "+" + RandomInt(999) + "(" + RandomInt(99) + ")" + RandomInt(999) + "-" + RandomInt(99) + "-" + RandomInt(99),
                });
            }
            return contacts;
        }

    }
}
