using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WisaApi
{
    static public class Connector
    {
        public static Dictionary<string, string> ReplaceInstNumber = new Dictionary<string, string>();

        private static WISA.TWISAAPICredentials credentials;
        private static WISA.WisaAPIService service;
        
        internal static ILog Log;

        public static void Init(string url, int port, string accountName, string password, string database, ILog log = null)
        {
            Log = log;

            credentials = new WISA.TWISAAPICredentials();
            credentials.Username = accountName;
            credentials.Password = password;
            credentials.Database = database;

            service = new WISA.WisaAPIServiceClient("Endpoint", "http://" + url + ":" + port.ToString() + "/SOAP/");
        }

        static public async Task<string> PerformQuery(string name, WISA.TWISAAPIParamValue[] values)
        {
            try
            {
                var data = await service.GetCSVDataAsync(new WISA.GetCSVDataRequest(credentials, name, values, true, ",", ""));
                return Encoding.Default.GetString(data.Result);
                
            } catch(Exception e)
            {
                Log?.Add("Wisa Query: " + e.Message, true);
                return String.Empty;
            }
        }
    }
}
