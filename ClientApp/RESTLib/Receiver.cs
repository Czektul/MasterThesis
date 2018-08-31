using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RESTLib
{
    public class Receiver
    {
        public string Address { get; }

        /// <summary>
        /// Basic constructor. Address is API address.
        /// </summary>
        /// <param name="adress"></param>
        public Receiver(string address)
        {
            Address = address;
        }

        /// <summary>
        /// Generic method that receive data from API. Returns array of expected data. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method"></param>
        /// <param name="parameters"></param>
        /// <param name="isArray"></param>
        /// <returns></returns>
        public T[] ReceiveData<T>(string method, string[] parameters, bool isArray)
        {

            Stopwatch sw = new Stopwatch();
            sw.Start();
            List<T> values;
            T value;
            String url = Address + "/" + method.ToLower() + "/";

            foreach (string param in parameters)
                url = url + param + "/";
            url = url.Remove(url.Length - 1);
            using (WebClient w = new WebClient())
            {
                w.Encoding = Encoding.UTF8;
                var json_data = string.Empty;
                try
                {
                    json_data = w.DownloadString(url);
                    json_data = json_data.Remove(json_data.Length - 1);
                    json_data = json_data.Remove(0,1);
                    json_data = json_data.Replace(@"\", "");
                }
                catch (Exception) { }
                sw.Stop();
                if (isArray)
                {
                    values = ((T[])Newtonsoft.Json.JsonConvert.DeserializeObject<T[]>(json_data)).ToList();
                    return values.ToArray();
                }
                else
                {
                    value = ((T)Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json_data));
                    return new T[1] { value };
                }
            }
        }
    }
}
