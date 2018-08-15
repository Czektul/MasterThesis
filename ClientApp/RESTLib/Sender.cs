using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RESTLib
{
    public class Sender
    {
        public string Address { get; }

        public Sender(string adress)
        {
            Address = adress;
        }
        public bool SendData<T>(string method, T dataToSend, bool isArray)
        {
            string data;
            StringContent request;
            String url = Address + "/" + method.ToLower() + "/";
            try
            {
                data = Newtonsoft.Json.JsonConvert.SerializeObject(dataToSend);
                using (HttpClient client = new HttpClient())
                {
                    // HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
                    if (!string.IsNullOrEmpty(data))
                    {
                        //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        request = new StringContent(data, Encoding.UTF8, "application/json");
                        var response = client.PostAsync(url, request).Result;
                        var result = response.Content.ReadAsStringAsync();
                    }
                }
            }
            catch(Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
