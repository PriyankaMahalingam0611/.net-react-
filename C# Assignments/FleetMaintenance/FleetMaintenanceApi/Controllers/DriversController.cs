using Microsoft.AspNetCore.Mvc;
using FleetMaintenanceApi.DTOs;
using FleetMaintenanceApi.Services.Interfaces;

namespace FleetMaintenanceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly IDriverService _driverService;

        public DriversController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDrivers()
        {
            var drivers = await _driverService.GetAllDriversAsync();
            return Ok(new { statusCode = 200, message = "Drivers retrieved successfully", data = drivers });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDriverById(int id)
        {
            if (id <= 0) return BadRequest("Invalid driver ID.");

            var driver = await _driverService.GetDriverByIdAsync(id);
            if (driver == null) return NotFound("Driver not found.");

            return Ok(new { statusCode = 200, message = "Driver retrieved successfully", data = driver });
        }

        [HttpPost]
        public async Task<IActionResult> AddDriver([FromBody] DriverCreateDto driverDto)
        {
            var result = await _driverService.AddDriverAsync(driverDto);

            if (!result.Success) return BadRequest(new { statusCode = 400, message = result.Message });

            return CreatedAtAction(nameof(GetDriverById), new { id = result.Data!.DriverId }, new { statusCode = 201, message = result.Message, data = result.Data });
        }
    }
}