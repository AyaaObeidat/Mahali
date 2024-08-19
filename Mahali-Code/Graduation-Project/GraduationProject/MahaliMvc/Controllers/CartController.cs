using MahaliDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace MahaliMvc.Controllers
{
    public class CartController : BaseController
    {
        public IActionResult Cart()
        {
            return View();
        }

        private async Task<List<CartListItems>> GetAllCartItemsAsync()
        {
            var response = await _client.GetAsync(_client.BaseAddress + "/Cart/GetAll");
            if (response.IsSuccessStatusCode)
            {
                var cartItems = await response.Content.ReadFromJsonAsync<List<CartListItems>>();
                return cartItems ?? new List<CartListItems>();
            }
            else
            {
                // Handle the error
                throw new Exception("Failed to load cart items");
            }
        }

        // Get cart by ID
        [HttpPost]
        public async Task<IActionResult> GetById(CartGetByParameters parameters)
        {
            var json = JsonConvert.SerializeObject(parameters);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_client.BaseAddress + "/Cart/GetById", content);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var cart = JsonConvert.DeserializeObject<CartListItems>(data);
                return View(cart);
            }
            return View();
        }

        // Add product to cart
        [HttpPost]
        public async Task<IActionResult> AddProduct(CartProductsCreateParameters parameters)
        {
            var json = JsonConvert.SerializeObject(parameters);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_client.BaseAddress + "/Cart/AddProduct", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        // Delete product from cart
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(CartProductsDeleteParameters parameters)
        {
            var json = JsonConvert.SerializeObject(parameters);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_client.BaseAddress + "/Cart/DeleteProduct", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        // Update product quantity in cart
        [HttpPatch]
        public async Task<IActionResult> UpdateCartProductQuantity(CartProductsUpdateParameters parameters)
        {
            var json = JsonConvert.SerializeObject(parameters);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PatchAsync(_client.BaseAddress + "/Cart/UpdateCartProductQuantity", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        // Update product color in cart
        [HttpPatch]
        public async Task<IActionResult> UpdateCartProductColor(CartProductsUpdateParameters parameters)
        {
            var json = JsonConvert.SerializeObject(parameters);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PatchAsync(_client.BaseAddress + "/Cart/UpdateCartProductColor", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        // Update product size in cart
        [HttpPatch]
        public async Task<IActionResult> UpdateCartProductSize(CartProductsUpdateParameters parameters)
        {
            var json = JsonConvert.SerializeObject(parameters);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PatchAsync(_client.BaseAddress + "/Cart/UpdateCartProductSize", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        // Get all products in a cart
        [HttpPost]
        public async Task<IActionResult> GetAllCartProducts(CartGetByParameters parameters)
        {
            var json = JsonConvert.SerializeObject(parameters);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_client.BaseAddress + "/Cart/GetAllCartProducts", content);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<List<CartProductsListItems>>(data);
                return View(products);
            }
            return View();
        }
    }
}
