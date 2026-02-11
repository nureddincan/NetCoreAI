using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreAI.Project01_ApiDemo.Context;
using NetCoreAI.Project01_ApiDemo.Entities;

namespace NetCoreAI.Project01_ApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        // DI(Dependency Injection) Bildirimi program.cs de yapılmalı
        private readonly ApiContext _context;
        public CustomersController(ApiContext context)
        {
            _context = context;
        }

        // Api'de istek atılırken o isteğin türü mutlaka bildirilmeli
        [HttpGet] // Veri listeme ve getirmede kullanılan istek türü
        public IActionResult CustomerList()
        {
            var value = _context.Customers.ToList(); // Bütün Customer'lar getirilir.
            // Swagger üzerindeki veriler bir mesaj kutusu üstünde gözükecek. Mesaj kutusu Ok methodu ile getirilir.
            return Ok(value); 
        }

        [HttpPost] // Ekleme işlemi gerçekleştirelim
        public IActionResult CreateCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return Ok("Müşteri ekleme işlemi başarılı.");
        }

        [HttpDelete] // Silme işlemi yapalım.
        public IActionResult DeleteCustomer(int id)
        {
            var value = _context.Customers.Find(id);
            _context.Customers.Remove(value);
            _context.SaveChanges();

            return Ok("Müşteri başarıyla silindi.");
        }


        // id'ye göre getirme işlemi yapalım, birden fazla aynı methodu kullanacaksak 
        // bu methodun yanına harici bir parametre veya isim eklemek gerekiyor.
        [HttpGet("GetCustomer")] 
        public IActionResult GetCustomer(int id)
        {
            var value = _context.Customers.Find(id);
            return Ok(value);
        }

        [HttpPut] // Güncelleme işlemi yapalım
        public IActionResult UpdateCustomer(Customer customer)
        {
            _context.Customers.Update(customer);
            _context.SaveChanges();
            return Ok("Müşteri başarıyla güncellendi.");
        }
    }
}
