using Microsoft.AspNetCore.Mvc;
using NetCoreAI.Project02_ApiConsumeUI.Dtos;
using Newtonsoft.Json;

namespace NetCoreAI.Project02_ApiConsumeUI.Controllers
{
    public class CustomerController : Controller
    {
        // Api'tarafında istek atılıp bu istekler çözümlenmeye çalışırken kullanılan bir tane interface var
        private readonly IHttpClientFactory _httpClientFactory;

        public CustomerController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory; // HTTP GET POST PUT DELETE için kullanacağız.
        }

        // Veriler bize json formatında gelecek. Bunları eşleştirmeliyiz.
        // API'deki isimler ve veri türleri nasılsa 
        // buradaki çekilecek olan isimler ve veri türleri birebir aynı olmak zorunda.
        // Bunu Dto(Data Transfer Object) ile çözebiliriz.
        // API Methodları asenkron olarak çalışır. Methodları asenkrona çevirmeliyiz.
        public async Task<IActionResult> CustomerList() 
        {
            var client = _httpClientFactory.CreateClient();
            // GetAsyn ile bir adrese istekte bulunacağız.
            var responseMessage = await client.GetAsync("https://localhost:7258/api/Customers");
            if(responseMessage.IsSuccessStatusCode)
            {
                // responseMessage'ın içindeki değeri okuyup, jsonData'nın içine atılacak.
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                // Bu değişken json formatındaki değeri deserilize edecek. (normal metne dönüşecek)
                var values = JsonConvert.DeserializeObject<List<ResultCustomerDto>>(jsonData);
                return View(values);
            }

            return View();
        }
    }
}
