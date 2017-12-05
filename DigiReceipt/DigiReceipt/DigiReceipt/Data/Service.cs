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
        private const string URL_USER_RECEIPT = URL_BASE + "/{0}/receipt";
        private const string URL_USER_RECEIPTS_ISSUED_ON = URL_BASE + "/{0}/receipts/{1}";
        private const string URL_USER_RECEIPT_ID = URL_BASE + "/{0}/receipt/{1}";

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
            var body = new Dictionary<string, Object> {
                            { "issuedOn", receipt.IssuedOn.ToString() },
                            { "image", receipt.ImageAsBase64() },
                            { "price", receipt.Price },
                            { "timestamp", receipt.Timestamp }
                        };
            
            HttpContent content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

            // Post the data and wait for a response from the server.
            var response = await client.PostAsync(String.Format(URL_USER_RECEIPT, id), content);

            dynamic data = null;

            if (response != null)
            {
                data = response.Content.ReadAsStringAsync().Result;
            }

            return data;
        }

        /// <summary>
        /// Retrieve a collection of receipts issued after the given date from the web service.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Receipt>> Retrieve(long lastTimestamp)
        {
            HttpClient client = new HttpClient();

            // Get the current users id to determine who the receipt belongs to.
            string id = AuthenticationManager.DefaultAuthenticationManager.CurrentUser.UserId.Split(':')[1];
            
            // Get the data and wait for a response from the server.
            var response = await client.GetAsync(String.Format(URL_USER_RECEIPTS_ISSUED_ON, id, lastTimestamp));

            List<Receipt> data = null;

            if (response != null)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                Dictionary<string, string> dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                data = JsonConvert.DeserializeObject<List<Receipt>>(dictionary["receipts"]);
            }

            return data;
        }

        /// <summary>
        /// Send a request to the web service to delete the receipt with the given id.
        /// </summary>
        /// <returns></returns>
        public static async Task<dynamic> Delete(string ReceiptID)
        {
            HttpClient client = new HttpClient();

            // Get the current users id to determine who the receipt belongs to.
            string id = AuthenticationManager.DefaultAuthenticationManager.CurrentUser.UserId.Split(':')[1];

            // Get the data and wait for a response from the server.
            var response = await client.DeleteAsync(String.Format(URL_USER_RECEIPT_ID, id, ReceiptID));

            dynamic data = null;

            if (response != null)
            {
                data = response.Content.ReadAsStringAsync().Result;
            }

            return data;
        }
    }
}
