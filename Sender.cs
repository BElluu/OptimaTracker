using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace OptimaTracker
{
    public class Sender
    {
        public static async Task<string> PostAsync(string jsonData)
        {
            using (var client = new HttpClient())
            {
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                try
                {
                    var response = await client.PostAsync(
                        "https://localhost:5001/api/events",
                        new StringContent(jsonData, Encoding.UTF8, "application/json"));

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return "OK";
                    }
                    else
                    {
                        throw new HttpResponseException(new HttpResponseMessage(response.StatusCode) { Content = new StringContent(response.ReasonPhrase) });
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return ex.Message;
                }
            }
        }
    }
}
