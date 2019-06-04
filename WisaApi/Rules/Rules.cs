using AbstractAccountApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WisaApi.Rules
{
    public static class Rules
    {
        public static Dictionary<Rule, string> ImportRules = new Dictionary<Rule, string>()
        {
            { Rule.WI_ReplaceInstitution, "Wijzig Instellingsnummer"},
            { Rule.WI_DontImportClass, "Klas niet Importeren" },
        };
    }
}
