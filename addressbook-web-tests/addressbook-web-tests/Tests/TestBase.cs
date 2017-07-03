using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;
using System.Linq;

namespace WebAddressbookTests
{

    public class TestBase
    {
        protected ApplicationManager app;
        public static bool PERFORM_LONG_UI_CHECKS = true;
        [SetUp]
        public void SetupApplicationManager()
        {
            app = ApplicationManager.GetInstance();
        }

    //RANDOM DATA GENERATION 
        public static Random rnd = new Random();

        public static string RandomString(int maxLength)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < RandomInt(maxLength); i++)
            {
                builder.Append(Convert.ToChar(Convert.ToInt32(rnd.NextDouble() * 65 + 32)));
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

        //READING GROUP DATA FROM FILES 
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

        public static IEnumerable<GroupData> GroupDataFromCsvFile()
        {
            List<GroupData> groups = new List<GroupData>();
            string[] lines = File.ReadAllLines(@"groups.csv");
            foreach (string l in lines)
            {
                string[] parts = l.Split(',');
                groups.Add(new GroupData(parts[0])
                {
                    Header = parts[1],
                    Footer = parts[2]
                });
            }
            return groups;
        }

        public static IEnumerable<GroupData> GroupDataFromXmlFile()
        {
            List<GroupData> groups = new List<GroupData>();
            return (List<GroupData>) new XmlSerializer(typeof(List<GroupData>)).Deserialize(new StreamReader(@"groups.xml"));
        }

        public static IEnumerable<GroupData> GroupDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<GroupData>>(File.ReadAllText(@"groups.json"));
        }

        public static IEnumerable<GroupData> GroupDataFromExcelFile()
        {
            List<GroupData> groups = new List<GroupData>();
            Excel.Application app = new Excel.Application();
            Excel.Workbook wb = app.Workbooks.Open(Path.Combine(Directory.GetCurrentDirectory(), @"groups.xlsx"));
            Excel.Worksheet sheet = wb.ActiveSheet;
            Excel.Range range = sheet.UsedRange;
            for (int i = 1; i <=range.Rows.Count; i++)
            {
                groups.Add(new GroupData()
                {
                    Name = range.Cells[i, 1].Value,
                    Header = range.Cells[i, 2].Value,
                    Footer = range.Cells[i, 3].Value
                });
            }
            wb.Close();
            app.Visible = false;
            app.Quit();
            return groups;
        }

        //RANDOM DATA FOR CONTACTS GENERATION 
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

        public static IEnumerable<ContactData> ContactDataFromCsvFile()
        {
            List<ContactData> contacts = new List<ContactData>();
            string[] lines = File.ReadAllLines(@"contacts.csv");
            foreach (string l in lines)
            {
                string[] parts = l.Split(',');
                contacts.Add(new ContactData(parts[0],parts[1])
                {
                    Address = parts[2],
                    EMail = parts[3],
                    EMail2 = parts[4],
                    EMail3 = parts[5],
                    WorkPhone = parts[6],
                    Mobile = parts[7],
                    HomePhone = parts[8]
                });
            }
            return contacts;
        }

        public static IEnumerable<ContactData> ContactDataFromXmlFile()
        {
            List<ContactData> contacts = new List<ContactData>();
            return (List<ContactData>)new XmlSerializer(typeof(List<ContactData>)).Deserialize(new StreamReader(@"contacts.xml"));
        }

        public static IEnumerable<ContactData> ContactDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<ContactData>>(File.ReadAllText(@"contacts.json"));
        }

        public static IEnumerable<ContactData> ContactDataFromExcelFile()
        {
            List<ContactData> contacts = new List<ContactData>();
            Excel.Application app = new Excel.Application();
            Excel.Workbook wb = app.Workbooks.Open(Path.Combine(Directory.GetCurrentDirectory(), @"contacts.xlsx"));
            Excel.Worksheet sheet = wb.ActiveSheet;
            Excel.Range range = sheet.UsedRange;
            for (int i = 1; i <= range.Rows.Count; i++)
            {
                contacts.Add(new ContactData()
                {
                    Firstname = range.Cells[i, 1].Value,
                    Lastname = range.Cells[i, 2].Value,
                    Address = range.Cells[i, 3].Value,
                    EMail = range.Cells[i, 4].Value,
                    EMail2 = range.Cells[i, 5].Value,
                    EMail3 = range.Cells[i, 6].Value,
                    WorkPhone = range.Cells[i, 7].Value,
                    Mobile = range.Cells[i, 8].Value,
                    HomePhone = range.Cells[i, 9].Value
                });
            }
            wb.Close();
            app.Visible = false;
            app.Quit();
            return contacts;
        }
    }
}
