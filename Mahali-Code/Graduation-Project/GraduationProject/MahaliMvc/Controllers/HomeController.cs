using MahaliDtos;
using MahaliMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace MahaliMvc.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
       
        public async Task<IActionResult> Index()
        {
            var homePageViewModel = new HomePageViewModel();
            var categories = new List<CategoryListItems>();
            var Products = new List<ProductListItems>();

            // Fetch categories
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/Category/GetAll");
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                categories = JsonConvert.DeserializeObject<List<CategoryListItems>>(responseData);
            }

            // Fetch featured products (or any other products you want to display)
            response = await _client.GetAsync(_client.BaseAddress + "/Product/GetAll");
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                Products = JsonConvert.DeserializeObject<List<ProductListItems>>(responseData);
            }

            homePageViewModel.Categories = categories;
            homePageViewModel.Products = Products;

            return View(homePageViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public async Task<IActionResult> AddToCart(Guid Id)
        {
            var input = new AddCustomerProductToCard
            {
                ProductId = Id,
                CustomerId = Guid.Parse(HttpContext.Session.GetString("CustomerId"))
            };
            var jsonLasted = JsonConvert.SerializeObject(input);
            var contentnLasted = new StringContent(jsonLasted, Encoding.UTF8, "application/json");
            HttpResponseMessage responsenLasted = await _client.PostAsync(_client.BaseAddress + "/Cart/AddCustomerProductToCard", contentnLasted);
            return RedirectToAction("AllCart");



        }

        public async Task<IActionResult> AllCart()
        {
            List<CartProductsListItems> products = new List<CartProductsListItems>();

            var inputAll = new CustomerGetByParameters
            {
                CustomerId = Guid.Parse(HttpContext.Session.GetString("CustomerId"))
            };
            var jsonAll = JsonConvert.SerializeObject(inputAll);
            var contentnAll = new StringContent(jsonAll, Encoding.UTF8, "application/json");
            HttpResponseMessage responsenAll = await _client.PostAsync(_client.BaseAddress + "/Cart/GetAllCartProducts", contentnAll);
            if (responsenAll.IsSuccessStatusCode)
            {
                string data = await responsenAll.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<List<CartProductsListItems>>(data);
            }
            return View(products);
        }
            public async Task<IActionResult> DeleteCart(Guid Id)
        {
            var input = new CartProductsDeleteParameters
            {
                ProductId = Id,
                CartId = Guid.Parse(HttpContext.Session.GetString("CustomerId")),
                Quantity = 1 ,
                Color = AllEnums.AllEnums.Colors.Red,
                Size = AllEnums.AllEnums.Sizes.US_45
            };
            var jsonLasted = JsonConvert.SerializeObject(input);
            var contentnLasted = new StringContent(jsonLasted, Encoding.UTF8, "application/json");
            HttpResponseMessage responsenLasted = await _client.PostAsync(_client.BaseAddress + "/Cart/DeleteProduct", contentnLasted);
            return RedirectToAction("AllCart");
        }

        public async Task<IActionResult> SearchProducts(string searchQuery)
        {
            List<ProductListItems> allProducts = new List<ProductListItems>();

            // Make a request to the backend API to retrieve all products
            HttpResponseMessage response = await _client.GetAsync("/Shop/GetAllProducts");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                allProducts = JsonConvert.DeserializeObject<List<ProductListItems>>(data);
            }

            // Perform filtering based on the search query
            List<ProductListItems> filteredProducts = allProducts.Where(p => p.Name.Contains(searchQuery)).ToList();

            return View("Index", filteredProducts); // Render the Index view with the filtered products
        }

        public async Task<IActionResult> CategoryProducts(Guid Id)
        {
        

            return RedirectToAction("Index");
        }

    }
}
