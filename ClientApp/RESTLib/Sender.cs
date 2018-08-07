using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
            String url = Address + "/" + method.ToLower() + "/";
            try
            {
                data = Newtonsoft.Json.JsonConvert.SerializeObject(dataToSend);

                HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
                if (!string.IsNullOrEmpty(data))
                {
                    request.ContentType = "application/json";
                    request.Method = "POST";

                    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        streamWriter.Write(data);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                }

                using (HttpWebResponse webresponse = request.GetResponse() as HttpWebResponse)
                {
                    using (StreamReader reader = new StreamReader(webresponse.GetResponseStream()))
                    {
                        string response = reader.ReadToEnd();
                        return bool.Parse(response);
                    }
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
