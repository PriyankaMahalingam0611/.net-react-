using Microsoft.AspNetCore.Mvc;
using FleetMaintenanceApi.DTOs;
using FleetMaintenanceApi.Services.Interfaces;

namespace FleetMaintenanceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceRecordsController : ControllerBase
    {
        private readonly IMaintenanceService _maintenanceService;

        public MaintenanceRecordsController(IMaintenanceService maintenanceService)
        {
            _maintenanceService = maintenanceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMaintenanceRecords([FromQuery] MaintenanceFilterRequestDto filter) 
        {
            var result = await _maintenanceService.GetPagedMaintenanceRecordsAsync(filter);

            if (!result.Success)
            {
                return BadRequest(new { statusCode = 400, message = result.Message });
            }

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> AddMaintenanceRecord([FromBody] MaintenanceCreateDto dto)
        {
            var result = await _maintenanceService.AddMaintenanceRecordAsync(dto);

            if (!result.Success) return BadRequest(new { statusCode = 400, message = result.Message });

            return Ok(new { statusCode = 201, message = result.Message, data = result.Data });
        }
    }
}