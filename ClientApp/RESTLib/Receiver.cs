using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RESTLib
{
    public class Receiver
    {
        public string Address { get; }


        public Receiver(string adress)
        {
            Address = adress;
        }

        public T[] ReceiveData<T>(string method, string[] parameters, bool isArray)
        {
            List<T> values;
            T value;
            String url = Address + "/" + method.ToLower() + "/";

            foreach (string param in parameters)
                url = url + param + "/";
            using (WebClient w = new WebClient())
            {
                w.Encoding = Encoding.UTF8;
                var json_data = string.Empty;
                try
                {
                    json_data = w.DownloadString(url);
                }
                catch (Exception) { }
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
