using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WisaApi
{
    public class School
    {
        private int id;
        private string name, description;

        public School(int ID, string Name, string Description)
        {
            id = ID;
            name = Name;
            description = Description;
        }

        public int ID { get => id; }
        public string Name { get => name; }
        public string Description { get => description; }

        public bool IsActive { get; set; } = false;

        public JObject ToJson()
        {
            JObject result = new JObject();
            result["ID"] = ID;
            result["Name"] = Name;
            result["Description"] = Description;
            result["IsActive"] = IsActive;
            return result;
        }

        public School(JObject obj)
        {
            id = Convert.ToInt32(obj["ID"]);
            name = obj["Name"].ToString();
            description = obj["Description"].ToString();
            IsActive = Convert.ToBoolean(obj["IsActive"]);
        }
    }
}
