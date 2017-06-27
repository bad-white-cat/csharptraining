using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using WebAddressbookTests;
using Excel = Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;

namespace addressbook_web_testdata_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            string entity = args[0];
            int count = Convert.ToInt32(args[1]);
            string filename = args[2];
            string format = args[3];

            if (entity == "groups")
            {
                List<GroupData> groups = new List<GroupData>();
                for (int i = 0; i < count; i++)
                {
                    groups.Add(new GroupData(TestBase.RandomString(10))
                    {
                        Header = TestBase.RandomString(10),
                        Footer = TestBase.RandomString(10)
                    });
                }
                if (format == "excel")
                {
                    WriteGroupsToExcelFile(groups, filename);
                }
                else
                {
                    StreamWriter writer = new StreamWriter(filename);
                    if (format == "csv")
                    {
                        WriteGroupsToCsvFile(groups, writer);
                    }
                    else if (format == "xml")
                    {
                        WriteGroupsToXmlFile(groups, writer);
                    }

                    else if (format == "json")
                    {
                        WriteGroupsToJsonFile(groups, writer);
                    }
                    else
                    {
                        Console.Out.Write("Unrecognized format: " + format);
                    }
                    writer.Close();
                }
            }

            else if (entity == "contacts")
            {
                List<ContactData> contacts = new List<ContactData>();
                for (int i = 0; i < count; i++)
                {
                    contacts.Add(new ContactData(TestBase.RandomString(10), TestBase.RandomString(10))
                    {
                        Address = TestBase.RandomString(100),
                        EMail = TestBase.RandomAlphaNumericString(30) + @"@" + TestBase.RandomAlphaNumericString(30) + "." + TestBase.RandomAlphaNumericString(3),
                        EMail2 = TestBase.RandomAlphaNumericString(30) + @"@" + TestBase.RandomAlphaNumericString(30) + "." + TestBase.RandomAlphaNumericString(3),
                        EMail3 = TestBase.RandomAlphaNumericString(30) + @"@" + TestBase.RandomAlphaNumericString(30) + "." + TestBase.RandomAlphaNumericString(3),
                        HomePhone = "+" + TestBase.RandomInt(999) + "(" + TestBase.RandomInt(99) + ")" + TestBase.RandomInt(999) + "-" + TestBase.RandomInt(99) + "-" + TestBase.RandomInt(99),
                        Mobile = "+" + TestBase.RandomInt(999) + "(" + TestBase.RandomInt(99) + ")" + TestBase.RandomInt(999) + "-" + TestBase.RandomInt(99) + "-" + TestBase.RandomInt(99),
                        WorkPhone = "+" + TestBase.RandomInt(999) + "(" + TestBase.RandomInt(99) + ")" + TestBase.RandomInt(999) + "-" + TestBase.RandomInt(99) + "-" + TestBase.RandomInt(99),
                    });
                }
                if (format == "excel")
                {
                    WriteContactsToExcelFile(contacts, filename);
                }
                else
                {
                    StreamWriter writer = new StreamWriter(filename);
                    if (format == "csv")
                    {
                        WriteContactsToCsvFile(contacts, writer);
                    }
                    else if (format == "xml")
                    {
                        WriteContactsToXmlFile(contacts, writer);
                    }

                    else if (format == "json")
                    {
                        WriteContactsToJsonFile(contacts, writer);
                    }
                    else
                    {
                        Console.Out.Write("Unrecognized format: " + format);
                    }
                    writer.Close();
                }
            }
            else
            {
                Console.Out.Write("Unrecognized type of test data: " + entity);
            }       
        }

        //Contacts 
        private static void WriteContactsToXmlFile(List<ContactData> contacts, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<ContactData>)).Serialize(writer, contacts);
        }

        private static void WriteContactsToJsonFile(List<ContactData> contacts, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented));
        }

        static void WriteContactsToCsvFile(List<ContactData> contacts, StreamWriter writer)
        {
            foreach (ContactData contact in contacts)
            {
                writer.WriteLine(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}",
                contact.Firstname,
                contact.Lastname,
                contact.Address,
                contact.EMail,
                contact.EMail2,
                contact.EMail3,
                contact.HomePhone,
                contact.Mobile,
                contact.WorkPhone));
            }
        }

        static void WriteContactsToExcelFile(List<ContactData> contacts, string filename)
        {
            Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook wb = app.Workbooks.Add();
            Excel.Worksheet sheet = wb.ActiveSheet;

            int row = 1;
            foreach (ContactData contact in contacts)
            {
                sheet.Cells[row, 1] = contact.Firstname;
                sheet.Cells[row, 2] = contact.Lastname;
                sheet.Cells[row, 3] = contact.Address;
                sheet.Cells[row, 4] = contact.EMail;
                sheet.Cells[row, 5] = contact.EMail2;
                sheet.Cells[row, 6] = contact.EMail3;
                sheet.Cells[row, 8] = contact.HomePhone;
                sheet.Cells[row, 9] = contact.Mobile;
                sheet.Cells[row, 10] = contact.WorkPhone;
                row++;
            }
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), filename);
            File.Delete(fullPath);
            wb.SaveAs(fullPath);
            wb.Close();
            app.Visible = false;
            app.Quit();
        }

        
        //Groups
        static void WriteGroupsToExcelFile(List<GroupData> groups, string filename)
        {
            Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook wb = app.Workbooks.Add();
            Excel.Worksheet sheet = wb.ActiveSheet;

            int row = 1;
            foreach (GroupData group in groups)
            {
                sheet.Cells[row, 1] = group.Name;
                sheet.Cells[row, 2] = group.Header;
                sheet.Cells[row, 3] = group.Footer;
                row++;
            }
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), filename);
            File.Delete(fullPath);
            wb.SaveAs(fullPath);
            wb.Close();
            app.Visible = false;
            app.Quit();
        }

        static void WriteGroupsToCsvFile(List<GroupData> groups, StreamWriter writer)
        {
            foreach (GroupData group in groups)
            {
                writer.WriteLine(String.Format("{0},{1},{2}",
                group.Name,
                group.Header,
                group.Footer));
            }
        }

    static void WriteGroupsToXmlFile(List<GroupData> groups, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
        }

    static void WriteGroupsToJsonFile(List<GroupData> groups, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(groups, Newtonsoft.Json.Formatting.Indented));
        }
    }
}
