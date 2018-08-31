using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RESTLib
{
    public class Updater
    {
        public string Address { get; }

        /// <summary>
        /// Basic constructor. Address is API address.
        /// </summary>
        /// <param name="adress"></param>
        public Updater(string adress)
        {
            Address = adress;
        }

        /// <summary>
        /// Update existing data by sending it by API method. Returns true if everything is ok.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method"></param>
        /// <param name="dataToSend"></param>
        /// <returns></returns>
        public bool UpdateData<T>(string method, T dataToSend)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            string data;
            StringContent request;
            String url = Address + "/" + method.ToLower() + "/";
            try
            {
                data = Newtonsoft.Json.JsonConvert.SerializeObject(dataToSend);
                using (HttpClient client = new HttpClient())
                {
                    if (!string.IsNullOrEmpty(data))
                    {
                        request = new StringContent(data, Encoding.UTF8, "application/json");
                        var response = client.PutAsync(url, request).Result;
                        var result = response.Content.ReadAsStringAsync();
                    }
                }
            }
            catch(Exception ex)
            {
                return false;
            }
            sw.Stop();
            return true;
        }
    }
}
