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
            url = url.Remove(url.Length - 1);
            using (WebClient w = new WebClient())
            {
                w.Encoding = Encoding.UTF8;
                var json_data = string.Empty;
                try
                {
                    json_data = w.DownloadString(url);
                    json_data = ClearString(json_data);
                }
                catch (Exception) { }
                if (isArray)
                {
                    json_data = json_data.Remove(json_data.Length - 2);
                    values = ((T[])Newtonsoft.Json.JsonConvert.DeserializeObject<T[]>(json_data)).ToList();
                    return values.ToArray();
                }
                else
                {
                    json_data = json_data.Remove(json_data.Length - 2);
                    value = ((T)Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json_data));
                    return new T[1] { value };
                }
            }
        }

        private string ClearString(string data)
        {
            string[] separator = new string[1];
            separator[0] = "\":\"";
            string[] newData = new string[0];
            string jsonData = string.Empty;
            try
            {
                newData = data.Split(separator, StringSplitOptions.None);
                newData[1] = newData[1].Replace(@"\", "");
                jsonData = newData[1].Remove(0, 1);
                jsonData = jsonData.Remove(jsonData.Length - 2);
                jsonData = jsonData.Replace(@"\", ""); 
            }
            catch (Exception ex) { }
            return newData[1];
        }


    }
}
