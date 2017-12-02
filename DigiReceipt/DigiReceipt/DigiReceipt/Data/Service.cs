using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DigiReceipt.Data
{
    public class Service
    {
        private const string URL_BASE = "https://digireceipt.azurewebsites.net/api";
        private const string URL_RECEIPT = URL_BASE + "/{0}/receipt";

        /// <summary>
        /// Send the given receipt to the web service to be saved.
        /// </summary>
        /// <param name="receipt"></param>
        /// <returns></returns>
        public static async Task<dynamic> Write(Receipt receipt)
        {
            HttpClient client = new HttpClient();

            // Get the current users id to determine who the receipt belongs to.
            string id = AuthenticationManager.DefaultAuthenticationManager.CurrentUser.UserId.Split(':')[1];
            
            // This dictionary will be converted to JSON and will be sent in the request body.
            var body = new Dictionary<string, string> {
                            { "issuedOn", receipt.IssuedOn.ToString() },
                            { "image", receipt.ImageAsBase64() }
                        };
            
            HttpContent content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

            // Post the data and wait for a response from the server.
            var response = await client.PostAsync(String.Format(URL_RECEIPT, id), content);

            dynamic data = null;

            if (response != null)
            {
                data = response.Content.ReadAsStringAsync().Result;
            }

            return data;
        }
    }
}
