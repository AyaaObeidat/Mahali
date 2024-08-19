using MahaliDtos;
using Microsoft.AspNetCore.Mvc;

namespace MahaliMvc.Controllers
{
    public class ReportController : BaseController
    {
        [HttpGet]
        public IActionResult WriteReport()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> WriteReport(ReportCreateParameters parameters)
        {
            if (!ModelState.IsValid)
            {
                return View(parameters);
            }

            var apiEndpoint = "Report/Add"; 
            var response = await _client.PostAsJsonAsync(apiEndpoint, parameters);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home"); 
            }
            else
            {
                // Handle error
                ModelState.AddModelError(string.Empty, "Failed to write report.");
                return View(parameters);
            }
        }

        public IActionResult GetAllReports()
        {
            return View();
        }
    }
}
