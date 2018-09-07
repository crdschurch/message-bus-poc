using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WebhookLoad
{
    class WebhookLoad
    {
        static HttpClient client = new HttpClient();

        static void Main()
        {
            string endPoint = "http://localhost:5005/api/widget";
            RunAsync(endPoint).GetAwaiter().GetResult();
        }

        static async Task RunAsync(string endPoint)
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri(endPoint);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                for (int i = 0; i < 1000; ++i)
                {

                    var jsonParm = GetJson();

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, endPoint);
                    request.Content = new StringContent(jsonParm, System.Text.Encoding.UTF8, "application/json");

                    var response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("success"); //response.Content.ReadAsStringAsync().Result);
                    }
                    else
                    {
                        Console.WriteLine("Couldn't hit end point");
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

//            Console.ReadLine();
        }

        private static string GetJson()
        {
            return "Hi there";
        }
    }
}

