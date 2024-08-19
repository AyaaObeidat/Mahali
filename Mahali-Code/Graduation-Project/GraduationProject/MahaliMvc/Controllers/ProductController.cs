using MahaliDtos;
using MahaliMvc.Models.Product;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MahaliMvc.Controllers
{
    public class ProductController : BaseController
    {
        

        public IActionResult AddProduct()
        {
            return View();
        }

        public async Task<IActionResult> Product(Guid Id)
        {
            var product = new ProductDetails();
            List<LatestProductsVisitedDetails> latestProducts = new List<LatestProductsVisitedDetails>();

            var req = new ProductGetById
            {
               ProductId = Id,
            };
            var json = JsonConvert.SerializeObject(req);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/Product/GetById", content);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                product = JsonConvert.DeserializeObject<ProductDetails>(data);
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserType")) && !string.IsNullOrEmpty(HttpContext.Session.GetString("CustomerId")))
                {
                    var input = new LatestProductsVisitedCreateParameters
                    {
                        ProductId = Id,
                        CustomerId =Guid.Parse(HttpContext.Session.GetString("CustomerId"))
                    };
                    var jsonLasted = JsonConvert.SerializeObject(input);
                    var contentnLasted = new StringContent(jsonLasted, Encoding.UTF8, "application/json");
                    HttpResponseMessage responsenLasted = await _client.PostAsync(_client.BaseAddress + "/Customer/AddLatestProductVisited", contentnLasted);
                    //***


                    HttpResponseMessage responsenLastedAll = await _client.PostAsync(_client.BaseAddress + "/Customer/GetAllLatestProductVisited", contentnLasted);
                    string dataAll = await responsenLastedAll.Content.ReadAsStringAsync();
                    latestProducts = JsonConvert.DeserializeObject<List<LatestProductsVisitedDetails>>(dataAll);


                }
            }
            var res = new ProductDto
            {
                latestProducts = latestProducts,
                product = product
            };
            return View(res);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductCreateParameters product)
        {
            var shopId = HttpContext.Session.GetString("ShopId");
            product.ShopId = Guid.Parse(shopId);
            var json = JsonConvert.SerializeObject(product);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/Product/Add", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Shop");
            }
            return View();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(Guid productId)
        {
            var parameters = new ProductDeleteParameters { ProductId = productId };
            var json = JsonConvert.SerializeObject(parameters);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(_client.BaseAddress + "/Product/Delete"),
                Content = content
            };

            HttpResponseMessage response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Shop");
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/Product/GetAll");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<List<ProductListItems>>(result);
                return View(products);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetProductById(ProductGetById parameter)
        {
            var json = JsonConvert.SerializeObject(parameter);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/Product/GetById", content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<ProductDetails>(result);
                return View(product);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProductToCategory(ProductGetByParameter parameter)
        {
            var json = JsonConvert.SerializeObject(parameter);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/Product/AddToCategory", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Shop");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ModifyProductName(ProductUpdateParameters parameter)
        {
            var json = JsonConvert.SerializeObject(parameter);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PatchAsync(_client.BaseAddress + "/Product/ModifyProductName", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Shop");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ModifyProductDescription(ProductUpdateParameters parameter)
        {
            var json = JsonConvert.SerializeObject(parameter);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PatchAsync(_client.BaseAddress + "/Product/ModifyProductDescription", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Shop");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ModifyProductImageUri(ProductUpdateParameters parameter)
        {
            var json = JsonConvert.SerializeObject(parameter);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PatchAsync(_client.BaseAddress + "/Product/ModifyProductImageUri", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Shop");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ModifyProductQuantity(ProductUpdateParameters parameter)
        {
            var json = JsonConvert.SerializeObject(parameter);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PatchAsync(_client.BaseAddress + "/Product/ModifyProductQuantity", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Shop");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ModifyProductPrice(ProductUpdateParameters parameter)
        {
            var json = JsonConvert.SerializeObject(parameter);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PatchAsync(_client.BaseAddress + "/Product/ModifyProductPrice", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Shop");
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Search(string searchQuery)
        {
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                // If the search query is empty or null, return an empty view
                return RedirectToAction("Index","Home");
            }

            HttpResponseMessage response = await _client.GetAsync($"{_client.BaseAddress}/Product/GetAll");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var allProducts = JsonConvert.DeserializeObject<List<ProductListItems>>(result);

                // Filter products by name
                var filteredProducts = allProducts.Where(p => p.Name.Contains(searchQuery)).ToList();

                return View("SearchResults", filteredProducts);
            }

            // If the request fails or there are no products, return an empty view
            return View();
        }


        public IActionResult SearchResults(List<ProductListItems> filteredProducts)
        {
            return View(filteredProducts);
        }
    }
}
