using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WisaTest
{
    class Log : WisaApi.ILog
    {
        public void Add(string message, bool error = false)
        {
            string output = error ? "Error: " : "Message: ";
            output += message;

            Debug.WriteLine(output);
        }
    }

    [TestClass]
    public class WisaConnector
    {
        public WisaConnector()
        {
            WisaApi.Connector.Init(
                Properties.Settings.Default.WisaUrl,
                Properties.Settings.Default.WisaPort,
                Properties.Settings.Default.WisaAccount,
                Properties.Settings.Default.WisaPassword,
                Properties.Settings.Default.WisaDatabase,
                new Log()
            );
        }

        [TestMethod]
        public void LoadClasses()
        {
           
        }

        [TestMethod]
        public async Task LoadSchools()
        {
            bool result = await WisaApi.Schools.Load();
            Assert.IsTrue(result);
            Assert.IsTrue(WisaApi.Schools.All.Count > 0);

            // load again to see if the previous list is cleared
            int count = WisaApi.Schools.All.Count;
            result = await WisaApi.Schools.Load();
            Assert.IsTrue(result);
            Assert.IsTrue(WisaApi.Schools.All.Count == count);

            // set some schools to active
            bool value = true;
            foreach (var school in WisaApi.Schools.All)
            {
                school.IsActive = value;
                value = !value;
            }

            string list = WisaApi.Schools.ActiveSchoolsToString();
            Assert.IsTrue(list.Length > 0);

            Assert.IsTrue(WisaApi.Schools.ActiveSchoolsFromString(list));

            // odd entries should still be false
            value = true;
            foreach (var school in WisaApi.Schools.All)
            {
                Assert.IsTrue(school.IsActive == value);
                value = !value;
            }
        }

        [TestMethod]
        public async Task LoadStudents()
        {
            WisaApi.School school = new WisaApi.School(
              25,
              "TEST",
              "Does not matter"
            );

            bool result = await WisaApi.Students.Add(school);
            Assert.IsTrue(result);
            Assert.IsTrue(WisaApi.Students.All.Count > 0);

            WisaApi.Students.Clear();
            Assert.IsTrue(WisaApi.Students.All.Count == 0);
        }

        [TestMethod]
        public async Task LoadStaff()
        {
            WisaApi.School school = new WisaApi.School(
             25,
             "TEST",
             "Does not matter"
            );

            bool result = await WisaApi.StaffMembers.Add(school);
            Assert.IsTrue(result, "StaffMembers.Add should return True");
            Assert.IsTrue(WisaApi.StaffMembers.All.Count > 0, "StaffMembers.All should contain data");

            WisaApi.StaffMembers.Clear();
            Assert.IsTrue(WisaApi.StaffMembers.All.Count == 0, "StaffMembers.All should be empty");
        }

        [TestMethod]
        public async Task LoadClassGroups()
        {
            WisaApi.School school = new WisaApi.School(
              25,
              "TEST",
              "Does not matter"
            );

            WisaApi.ClassGroups.Clear();
            bool result = await WisaApi.ClassGroups.AddSchool(school);
            Assert.IsTrue(result);
            Assert.IsTrue(WisaApi.ClassGroups.All.Count > 0);

            WisaApi.ClassGroups.Clear();
            Assert.IsTrue(WisaApi.ClassGroups.All.Count == 0);
        }
    }
}
