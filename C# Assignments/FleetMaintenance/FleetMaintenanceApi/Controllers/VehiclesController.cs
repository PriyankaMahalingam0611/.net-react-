using Microsoft.AspNetCore.Mvc;
using FleetMaintenanceApi.DTOs;
using FleetMaintenanceApi.Services.Interfaces;

namespace FleetMaintenanceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehiclesController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVehicles()
        {
            var vehicles = await _vehicleService.GetAllVehiclesAsync();
            return Ok(new { statusCode = 200, message = "Vehicles retrieved successfully", data = vehicles });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicleById(int id)
        {
            if (id <= 0) return BadRequest("Invalid vehicle ID."); 

            var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
            if (vehicle == null) return NotFound("Vehicle not found."); 

            return Ok(new { statusCode = 200, message = "Vehicle retrieved successfully", data = vehicle });
        }

        [HttpPost]
        public async Task<IActionResult> AddVehicle([FromBody] VehicleCreateDto vehicleDto)
        {
            var result = await _vehicleService.AddVehicleAsync(vehicleDto);

            if (!result.Success) return BadRequest(new { statusCode = 400, message = result.Message });

            return CreatedAtAction(nameof(GetVehicleById), new { id = result.Data!.VehicleId }, new { statusCode = 201, message = result.Message, data = result.Data });
        }
    }
}