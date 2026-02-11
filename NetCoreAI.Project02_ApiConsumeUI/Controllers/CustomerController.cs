using Microsoft.AspNetCore.Mvc;
using NetCoreAI.Project02_ApiConsumeUI.Dtos;
using Newtonsoft.Json;
using System.Text;

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
            // GetAsync ile bir adrese istekte bulunacağız.
            var responseMessage = await client.GetAsync("https://localhost:7258/api/Customers");
            if (responseMessage.IsSuccessStatusCode)
            {
                // responseMessage'ın içindeki değeri okuyup, jsonData'nın içine atılacak.
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                // Bu değişken json formatındaki değeri deserilize edecek. (normal metne dönüşecek)
                var values = JsonConvert.DeserializeObject<List<ResultCustomerDto>>(jsonData);
                return View(values);
            }

            return View();
        }

        [HttpGet]
        public IActionResult CreateCustomer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDto createCustomerDto)
        {
            var client = _httpClientFactory.CreateClient();
            // Serialize normal gelen veriyi json formatına dönüştürür.
            var jsonData = JsonConvert.SerializeObject(createCustomerDto);
            // Gönderilecek data, data için kullanılacak karakter dizini, gönderilecek datanın formatı
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            // PostAsync ile bir adrese veri göndereceğiz.
            var responseMessage = await client.PostAsync("https://localhost:7258/api/Customers", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("CustomerList");
            }
            return View();
        }

        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var client = _httpClientFactory.CreateClient();
            // ?id= ile dışarıdan gelen id'yi Request URL'nin içine entegre etmiş olacağız.
            var responseMessage = await client.DeleteAsync($"https://localhost:7258/api/Customers?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("CustomerList");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCustomer(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7258/api/Customers/GetCustomer?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                // Serialize normal gelen veriyi json formatına dönüştürür.
                var values = JsonConvert.DeserializeObject<GetByIdCustomerDto>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerDto updateCustomerDto)
        {
            var client = _httpClientFactory.CreateClient();
            // metni jsona dönüştürüyoruz.
            var jsonData = JsonConvert.SerializeObject(updateCustomerDto);
            // Gönderilecek data, data için kullanılacak karakter dizini, gönderilecek datanın formatı
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7258/api/Customers", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("CustomerList");
            }
            return View();
        }
    }
}
