using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WisaApi
{
    static public class Connector
    {
        public static Dictionary<string, string> ReplaceInstNumber = new Dictionary<string, string>();

        private static WISA.TWISAAPICredentials credentials;
        private static WISA.WisaAPIServiceService service;
        
        internal static ILog Log;

        public static void Init(string url, int port, string accountName, string password, string database, ILog log = null)
        {
            Log = log;

            credentials = new WISA.TWISAAPICredentials();
            credentials.Username = accountName;
            credentials.Password = password;
            credentials.Database = database;

            service = new WISA.WisaAPIServiceService();
            service.Url = "http://" + url + ":" + port.ToString() + "/SOAP/";
        }

        static public async Task<string> PerformQuery(string name, WISA.TWISAAPIParamValue[] values)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var data = service.GetCSVData(credentials, name, values, true, ",", "");
                    return Encoding.Default.GetString(data);
                }
                catch (Exception e)
                {
                    Log?.Add("Wisa Query: " + e.Message, true);
                    return String.Empty;
                }
            });
        }

        static public void GetClassList(string IDListCSV)
        {
            List<WISA.TWISAAPIParamValue> values = new List<WISA.TWISAAPIParamValue>();
            values.Add(new WISA.TWISAAPIParamValue());
            values.Last().Name = "IS_ID";
            values.Last().Value = IDListCSV;
            values.Add(new WISA.TWISAAPIParamValue());
            values.Last().Name = "Werkdatum";
            values.Last().Value = "22/05/2018";

            try
            {
                var result = service.GetCSVData(credentials, "SMATestQ", values.ToArray(), true, ",", "");
                var str = System.Text.Encoding.Default.GetString(result);
                Debug.Write(str);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
    }
}

