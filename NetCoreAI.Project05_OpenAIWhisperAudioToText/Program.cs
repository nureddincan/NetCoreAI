using System.Net.Http.Headers;

class Program
{
    static async Task Main(string[] args)
    {
        string apiKey = "Buraya API Keyiniz Gelecek.";
        string audioFilePath = "Audio1.mp3";

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            // Dosyayla işlem yapmamızı sağlayacak sınıf.
            var form = new MultipartFormDataContent();
            // Ses dosyasını yükleme işlemi, byte formatıyla ses dosyasını okuyacağız.
            var audioContent = new ByteArrayContent(File.ReadAllBytes(audioFilePath));
            // Ses dosyası için kullanacağımız çıkış formatı
            audioContent.Headers.ContentType = MediaTypeHeaderValue.Parse("audio/mpeg");
            // İşleyeceğimiz sesin nerden geleceğini uygulamaya tanıttık.
            form.Add(audioContent, "file", Path.GetFileName(audioFilePath));

            form.Add(new StringContent("whisper-1"), "model");

            Console.WriteLine("Ses Dosyası İşleniyor, Lütfen Bekleyiniz...");

            // hafızaya alıp okuttuğumuz dosya için OpenAI API'ye post isteği göndereceğiz.
            // form ses dosyası için gönderdiğimiz content
            var response = await client.PostAsync("https://api.openai.com/v1/audio/transcriptions", form);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Transkript: ");
                Console.WriteLine(result);
            }
            else
            {
                Console.WriteLine($"Hata: {response.StatusCode}");
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
        }
    }
}