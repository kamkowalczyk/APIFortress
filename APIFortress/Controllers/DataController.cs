using ApiFortress.Application.DTOs;
using ApiFortress.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiiFortress.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DataController : ControllerBase
    {
        private readonly IDataService _dataService;

        public DataController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public async Task<IActionResult> GetData()
        {
            var dataItems = await _dataService.GetAllDataAsync();
            return Ok(dataItems);
        }

        [HttpPost]
        public async Task<IActionResult> PostData([FromBody] DataItemDTO dataDto)
        {
            if (dataDto == null)
                return BadRequest("Data is required.");

            var addedData = await _dataService.AddDataAsync(dataDto);
            return CreatedAtAction(nameof(GetData), new { id = addedData.Id }, addedData);
        }
    }
}