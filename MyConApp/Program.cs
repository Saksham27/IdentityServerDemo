using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyConApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Main().Wait();
        }
        static async Task Main()
        { 
            var client = new HttpClient();
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = "http://localhost:5000/connect/token",
                ClientId = "client",
                ClientSecret = "secret",
                Scope = "api1.read"
            });


            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("https://localhost:5101/weatherforecast/");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
        }

    }
}

