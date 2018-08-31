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
    public class Sender
    {
        public string Address { get; }

        /// <summary>
        /// Basic constructor. Address is API address.
        /// </summary>
        /// <param name="adress"></param>
        public Sender(string adress)
        {
            Address = adress;
        }
        /// <summary>
        /// Send data to server by API POST method. Returns true if everything is ok.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method"></param>
        /// <param name="dataToSend"></param>
        /// <returns></returns>
        public bool SendData<T>(string method, T dataToSend)
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
                    // HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
                    if (!string.IsNullOrEmpty(data))
                    {
                        //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        request = new StringContent(data, Encoding.UTF8, "application/json");
                        var response = client.PostAsync(url, request).Result;
                        if (!response.IsSuccessStatusCode)
                            throw new Exception(string.Format("Błąd wysyłania danych - Kod statusu: {0}", response.StatusCode.ToString()));
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

        public bool SendFileAsStream(string method, Stream stream, string filename)
        {

            Stopwatch sw = new Stopwatch();
            sw.Start();
            int maxData = 0;
            byte[] bytearray;
            bool messageEnd = false;
            byte[] currentBytes;
            string data = string.Empty;
            StringContent request;
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    bytearray = ms.ToArray();
                }
                for (int i = 0; i < bytearray.Length; i=i+1000)
                {

                    if (i + 1000 > bytearray.Length)
                    {
                        maxData = bytearray.Length - i;
                        messageEnd = true;
                    }
                    else
                    {
                        maxData = 1000;
                        messageEnd = false;
                    }

                    currentBytes = new byte[maxData];
                    Array.Copy(bytearray, i, currentBytes, 0, maxData);
                    data = Newtonsoft.Json.JsonConvert.SerializeObject(currentBytes);
                    string url = Address + "/" + method.ToLower() + "/" + filename+ "/" + messageEnd.ToString();
                    using (HttpClient client = new HttpClient())
                    {
                        if (!string.IsNullOrEmpty(data))
                        {
                            request = new StringContent(data, Encoding.UTF8, "application/json");
                            var response = client.PostAsync(url, request).Result;
                            if (!response.IsSuccessStatusCode)
                                throw new Exception(string.Format("Błąd wysyłania danych - Kod statusu: {0}", response.StatusCode.ToString()));
                            var result = response.Content.ReadAsStringAsync();
                        }
                    }

                }
                sw.Stop();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
