using System.Net.Http;
using System.Threading;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Garavana.DataParser
{
    internal class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static void Main(string[] args)
        {

            // https://zagorapi.yemek.com/post/recent?limit=50&format=json&Start=0&Rows=15
            int limit = 50;
            int start = 0;


            for (int i = 0; i < 10; i++)
            {
                start = i * limit;
                string url = "https://zagorapi.yemek.com/post/recent?limit=" + limit + "&format=json&Start=" + start + "&Rows=15";
                var response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;
                    string responseString = responseContent.ReadAsStringAsync().Result;
                    JsonDocument document = JsonDocument.Parse(responseString);
                    JsonElement root = document.RootElement;

                    //Console.WriteLine(responseString);

                    for (int ii = 0; ii < root.GetProperty("Data").GetArrayLength(); ii++)
                    {
                        Console.WriteLine(root.GetProperty("Data")[ii].GetProperty("Title").ToString());
                    }
                    Console.WriteLine("--------------------------------------------------");


                    Thread.Sleep(1000);
                }

            }

        }
    }
}