using AbstractAccountApi;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AbstractAccountApi.ObservableProperties;

namespace WisaApi.Rules
{
    public class DontImportClass : IRule
    {
        private Rule rule;
        public Rule Rule => rule;

        private RuleType ruleType;
        public RuleType RuleType => ruleType;

        private RuleAction ruleAction;
        public RuleAction RuleAction => ruleAction;

        private string header;
        public string Header => header;

        private string description;
        public string Description => description;

        public Prop<string> ShortInfo { get; set; } = new Prop<string> { Value = "Klas: " };

        public bool Enabled { get; set; }

        public string getConfig(int ID)
        {
            return className;
        }

        public void Modify(object obj)
        {
            
        }

        public void setConfig(int ID, string data)
        {
            className = data;
            this.ShortInfo.Value = "Klas: " + className;
        }

        public bool ShouldApply(object obj)
        {
            var group = obj as WisaApi.ClassGroup;
            return group.Name.Equals(className); 
        }

        public JObject ToJson()
        {
            var result = new JObject();
            result["Rule"] = rule.ToString();
            result["ClassName"] = className;
            return result;
        }

        public DontImportClass() {
            setDefaults();
        }

        public DontImportClass(JObject obj)
        {
            setDefaults();
            this.className = obj.ContainsKey("ClassName") ? obj["ClassName"].ToString() : "";
            this.ShortInfo.Value = "Klas: " + className;
        }

        private void setDefaults()
        {
            rule = Rule.WI_DontImportClass;
            ruleType = RuleType.WISA_Import;
            ruleAction = RuleAction.Discard;
            header = "Klas niet Importeren";
            description = "Sla deze klas over bij het importeren uit Wisa.";
        }

        private string className;
    }
}
