using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WebhookLoad
{
    class WebhookLoad
    {
        static HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            int numMessages;
            if (args.Length == 0 || !Int32.TryParse(args[0], out numMessages))
                numMessages = 1;

            string endPoint = "http://localhost:5005/api/widget";
            RunAsync(endPoint, numMessages).GetAwaiter().GetResult();
        }

        static async Task RunAsync(string endPoint, int numMessages)
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri(endPoint);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                for (int i = 0; i < numMessages; ++i)
                {

                    var jsonParm = GetJson();

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, endPoint);
                    request.Content = new StringContent(jsonParm, System.Text.Encoding.UTF8, "application/json");

                    var response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        if (i % 10000 == 0)
                        {
                            Console.WriteLine($"{i}: success"); //response.Content.ReadAsStringAsync().Result);
                        }
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

