using Newtonsoft.Json;
using System.Text;

class Program
{
    private static async Task Main(string[] args)
    {
        Console.Write("Lütfen Çevirmek İstediğiniz Cümleyi Giriniz: ");
        string inputText = Console.ReadLine();

        string apiKey = "Buraya API Key Gelecek.";

        string translatedText = await TranslateTextToEnglish(inputText, apiKey);

        if (!string.IsNullOrEmpty(translatedText))
        {
            Console.ReadLine();
            Console.Write($"İngilizce Çeviri: {translatedText}");
            Console.ReadLine();
        }
        else
        {
            Console.Write("Beklenmeyen bir hata oluştu.");
        }
    }

    private static async Task<String> TranslateTextToEnglish(string text, string apiKey)
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new {role = "system", content = "You are helpful translator."},
                    new {role = "user", content= $"Translate the entered text into English: {text}"}
                }
            };

            string jsonBody = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
                string responseString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"API Hatası Aldık: {response.StatusCode}");
                    Console.WriteLine($"Detay: {responseString}"); // Hatanın nedenini burada göreceksin
                    return null;
                }

                dynamic responseObject = JsonConvert.DeserializeObject(responseString);
                string translation = responseObject.choices[0].message.content;

                return translation;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bir Hata Oluştu: {ex.Message}");
                return null;
            }
        }
    }
}