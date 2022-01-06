using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace OptimaTracker
{
    public class Sender
    {
        public async Task<string> PostAsync(string jsonData)
        {
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(
                    "127.0.0.1/events",
                    new StringContent(jsonData, Encoding.UTF8, "application/json"));

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return "OK";
                }
                else
                {
                    throw new HttpResponseException(new HttpResponseMessage(response.StatusCode) { Content = new StringContent(response.ReasonPhrase) });
                }
            }
        }

    }
}
