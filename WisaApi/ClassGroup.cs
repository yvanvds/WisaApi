using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WisaApi
{
    public class ClassGroup
    {
        private string name;
        private string description;
        private string adminCode;
        private string schoolCode;

        public ClassGroup(string data, int schoolID)
        {
            string[] values = data.Split(',');
            name = values[0];
            description = values[1];
            adminCode = values[2];
            schoolCode = values[3];
            SchoolID = schoolID;

            if (Connector.ReplaceInstNumber.ContainsKey(schoolCode))
            {
                schoolCode = Connector.ReplaceInstNumber[schoolCode];
            }
        }

        public string Name { get => name; }
        public string Description { get => description; }
        public string AdminCode { get => adminCode; }
        public string SchoolCode { get => schoolCode; }

        public int SchoolID { get; }

        public JObject ToJson()
        {
            JObject result = new JObject();
            result["Name"] = Name;
            result["Description"] = Description;
            result["AdminCode"] = AdminCode;
            result["SchoolCode"] = SchoolCode;
            result["SchoolID"] = SchoolID;
            return result;
        }

        public ClassGroup(JObject obj)
        {
            name = obj["Name"].ToString();
            description = obj["Description"].ToString();
            adminCode = obj["AdminCode"].ToString();
            schoolCode = obj["SchoolCode"].ToString();
            SchoolID = Convert.ToInt32(obj["SchoolID"]);
        }
    }
}
