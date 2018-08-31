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
    public class Deleter
    {
        public string Address { get; }

        /// <summary>
        /// Basic constructor. Address is API address.
        /// </summary>
        /// <param name="adress"></param>
        public Deleter(string adress)
        {
            Address = adress;
        }

        /// <summary>
        /// Delete existing data from server by DELETE API method. Returns true if everything is ok.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method"></param>
        /// <param name="dataToDelete"></param>
        /// <returns></returns>
        public bool DeleteData(string method, string[] parameters)
        {

            Stopwatch sw = new Stopwatch();
            sw.Start();
            String url = Address + "/" + method.ToLower() + "/";

            foreach (string param in parameters)
                url = url + param + "/";
            url = url.Remove(url.Length - 1);
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    if (!string.IsNullOrEmpty(url))
                    {
                        var response = client.DeleteAsync(url).Result;
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
