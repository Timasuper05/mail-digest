using System.Net.Http.Headers;
using System.Text.Json;

Console.WriteLine("Hello, World!");
Console.ReadKey();
HttpClient httpClient = new HttpClient();
httpClient.DefaultRequestHeaders.Accept.Clear();
httpClient.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
httpClient.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

await GetGeoLocationAsync(httpClient);
static async Task GetGeoLocationAsync(HttpClient client)
{
    string geocodingUrl = "https://geocoding-api.open-meteo.com/v1/search?name={Makhachkala}&count=1&language=en";
    var json = await client.GetStringAsync(geocodingUrl);

    Console.Write(json);
}