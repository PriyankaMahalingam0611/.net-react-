using FleetMaintenanceApi.DTOs;
using FleetMaintenanceApi.Models;
using FleetMaintenanceApi.Repositories.Interfaces;
using FleetMaintenanceApi.Services.Interfaces;

namespace FleetMaintenanceApi.Services.Implementations
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;

        public DriverService(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public async Task<List<DriverResponseDto>> GetAllDriversAsync()
        {
            var drivers = await _driverRepository.GetAllDriversAsync();
            return drivers.Select(d => new DriverResponseDto
            {
                DriverId = d.DriverId,
                DriverName = d.DriverName,
                LicenseNumber = d.LicenseNumber,
                PhoneNumber = d.PhoneNumber,
                City = d.City,
                IsAvailable = d.IsAvailable
            }).ToList();
        }

        public async Task<DriverResponseDto?> GetDriverByIdAsync(int driverId)
        {
            var driver = await _driverRepository.GetDriverByIdAsync(driverId);
            if (driver == null) return null;

            return new DriverResponseDto
            {
                DriverId = driver.DriverId,
                DriverName = driver.DriverName,
                LicenseNumber = driver.LicenseNumber,
                PhoneNumber = driver.PhoneNumber,
                City = driver.City,
                IsAvailable = driver.IsAvailable
            };
        }

        public async Task<(bool Success, string Message, DriverResponseDto? Data)> AddDriverAsync(DriverCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.DriverName) || string.IsNullOrWhiteSpace(dto.LicenseNumber) || 
                string.IsNullOrWhiteSpace(dto.PhoneNumber) || string.IsNullOrWhiteSpace(dto.City))
                return (false, "Driver Name, License Number, Phone Number, and City are required.", null); 

            var driver = new Driver
            {
                DriverName = dto.DriverName,
                LicenseNumber = dto.LicenseNumber,
                PhoneNumber = dto.PhoneNumber,
                City = dto.City,
                IsAvailable = dto.IsAvailable
            };

            await _driverRepository.AddDriverAsync(driver);

            var responseDto = new DriverResponseDto
            {
                DriverId = driver.DriverId,
                DriverName = driver.DriverName,
                LicenseNumber = driver.LicenseNumber,
                PhoneNumber = driver.PhoneNumber,
                City = driver.City,
                IsAvailable = driver.IsAvailable
            };

            return (true, "Driver added successfully", responseDto);
        }
    }
}