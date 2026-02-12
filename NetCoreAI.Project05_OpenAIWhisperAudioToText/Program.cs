using System.Net.Http.Headers;

class Program
{
    static async Task Main(string[] args)
    {
        string apiKey = "Buraya API Keyiniz Gelecek.";
        string audioFilePath = "Audio1.mp3";

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer ", apiKey);
        }
    }
}