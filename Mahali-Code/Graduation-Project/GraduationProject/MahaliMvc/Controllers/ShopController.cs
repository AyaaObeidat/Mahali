using MahaliDtos;
using MahaliMvc.Models.Enum;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace MahaliMvc.Controllers
{
    public class ShopController : BaseController
    {
        public async Task<IActionResult> Index()
        {

            List<ProductListItems> shops = new List<ProductListItems>();
            var req = new ShopGetByParameter
            {
                ShopId = Guid.Parse(HttpContext.Session.GetString("ShopId")),
             };
            var json = JsonConvert.SerializeObject(req);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/Shop/GetAllShopProducts", content);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                shops = JsonConvert.DeserializeObject<List<ProductListItems>>(data);
            }
            return View(shops);
        }


        [HttpGet]
        public async Task<IActionResult> EditShopDetails()
        {

            ShopUpdateParameters shops = new ShopUpdateParameters();
            var req = new ShopGetByParameter
            {
                ShopId = Guid.Parse(HttpContext.Session.GetString("ShopId")),
            };
            var json = JsonConvert.SerializeObject(req);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/Shop/GetShopByIdAsync", content);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                shops = JsonConvert.DeserializeObject<ShopUpdateParameters>(data);
            }
            return View(shops);
        }


        // GET: Shop/GetAll
        public async Task<IActionResult> GetAll()
        {
            List<ShopDetails> shops = new List<ShopDetails>();
            HttpResponseMessage response = await _client.GetAsync("/Shop/GetAll");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                shops = JsonConvert.DeserializeObject<List<ShopDetails>>(data);
            }

            return View(shops);
        }



        // GET: Shop/GetShopOrders
        [HttpGet]
        public async Task<IActionResult> GetShopOrders()
        {
            List<OrderDetails> orders = new List<OrderDetails>();
            Guid shopId = Guid.Parse(HttpContext.Session.GetString("ShopId"));


            var parameter = new ShopGetByParameter { ShopId = shopId };
            var json = JsonConvert.SerializeObject(parameter);
            var content = new StringContent(json, Encoding.UTF8, "application/json");


            HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/Shop/GetAllShopOrders", content);


            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                orders = JsonConvert.DeserializeObject<List<OrderDetails>>(data);
            }

            return View(orders);
        }



        // GET: Shop/GetAllReviews
        [HttpGet]
        public async Task<IActionResult> GetAllReviews()
        {
            List<ReviewRequestDetails> reviews = new List<ReviewRequestDetails>();
            HttpResponseMessage response = await _client.GetAsync("/Shop/GetAllReviewsList");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                reviews = JsonConvert.DeserializeObject<List<ReviewRequestDetails>>(data);
            }

            return View(reviews);
        }

        // Other actions like ModifyShopName, ModifyShopPassword, etc.

        [HttpPost]
        public async Task<IActionResult> EditShopName(ShopUpdateParameters parameters)
        {
            var json = JsonConvert.SerializeObject(parameters);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PatchAsync("/Shop/ModifyShopName", content);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Failed to update shop name.");
                return View("EditShopDetails", parameters);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditShopPassword(ShopUpdateParameters parameters)
        {
            var json = JsonConvert.SerializeObject(parameters);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PatchAsync("/Shop/ModifyShopPassword", content);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Failed to update shop password.");
                return View("EditShopDetails", parameters);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditShopPhoneNumber(ShopUpdateParameters parameters)
        {
            var json = JsonConvert.SerializeObject(parameters);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PatchAsync("/Shop/ModifyShopPhoneNumber", content);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Failed to update shop phone number.");
                return View("EditShopDetails", parameters);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditShopDescription(ShopUpdateParameters parameters)
        {
            var json = JsonConvert.SerializeObject(parameters);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PatchAsync("/Shop/ModifyShopDescription", content);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Failed to update shop description.");
                return View("EditShopDetails", parameters);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllShops()
        {
            // Fetch all shops
            List<ShopListItems> shops = new List<ShopListItems>();
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/Shop/GetAll");
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                shops = JsonConvert.DeserializeObject<List<ShopListItems>>(data);
            }
            return View(shops);
        }

        [HttpGet]
        public async Task<IActionResult> ViewShopProducts(Guid shopId)
        {
           
            var parameter = new ShopGetByParameter { ShopId = shopId };
            HttpResponseMessage response = await _client.PostAsJsonAsync(_client.BaseAddress + "/Shop/GetAllShopProducts", parameter);

  
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<List<ProductListItems>>(data);
                return View(products);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


    }
}
