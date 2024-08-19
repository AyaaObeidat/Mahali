
using Mahali.Services;
using MahaliDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mahali.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ReportService _reportService;

        public ReportController(ReportService reportService)
        {
            _reportService = reportService;
        }


        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> WriteReportAsync([FromBody]ReportCreateParameters parameters)
        {
            await _reportService.WriteReportAsync(parameters);
            return Ok();
        }

        [HttpPatch]
        [Route("Update")]
        public async Task<IActionResult> EditReportTextAsync([FromBody]ReportUpdateParameters parameters)
        {
            await _reportService.EditReportTextAsync(parameters);
            return Ok();
        }


        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteReportAsync([FromBody]ReportDeleteParameters reportDelete)
        {
            await _reportService.DeleteReportAsync(reportDelete);
            return Ok();
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllReportsAsync()
        {
            return Ok(await _reportService.GetAllReportsAsync());
        }

        [HttpPost]
        [Route("GetById")]
        public async Task<IActionResult> GetByIdAsync([FromBody]ReportGetByParameters parameters)
        {
            return Ok(await _reportService.GetByIdAsync(parameters));
        }
    }
}
