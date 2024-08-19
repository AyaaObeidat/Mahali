using MahaliDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;


namespace MahaliMvc.Controllers
{
    public class CategoryController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> AddCategory()
        {
            return View();
        }
        public async Task<IActionResult> AddCategory(MahaliDtos.CategoryCreateParameters parameters)
        {
            var json = JsonConvert.SerializeObject(parameters);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/Category/Add", content);
            return RedirectToAction("GetAllCategory");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            var categories = new List<CategoryListItems>();
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/Category/GetAll");
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                categories = JsonConvert.DeserializeObject<List<CategoryListItems>>(responseData);
            }

            return View(categories); 
        }

    }
}
