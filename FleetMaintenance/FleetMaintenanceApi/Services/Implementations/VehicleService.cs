using FleetMaintenanceApi.DTOs;
using FleetMaintenanceApi.Models;
using FleetMaintenanceApi.Repositories.Interfaces;
using FleetMaintenanceApi.Services.Interfaces;

namespace FleetMaintenanceApi.Services.Implementations
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<List<VehicleResponseDto>> GetAllVehiclesAsync()
        {
            var vehicles = await _vehicleRepository.GetAllVehiclesAsync();
            return vehicles.Select(v => new VehicleResponseDto
            {
                VehicleId = v.VehicleId,
                VehicleNumber = v.VehicleNumber,
                VehicleType = v.VehicleType,
                Brand = v.Brand,
                Model = v.Model,
                PurchaseYear = v.PurchaseYear,
                IsActive = v.IsActive
            }).ToList();
        }

        public async Task<VehicleResponseDto?> GetVehicleByIdAsync(int vehicleId)
        {
            var vehicle = await _vehicleRepository.GetVehicleByIdAsync(vehicleId);
            if (vehicle == null) return null;

            return new VehicleResponseDto
            {
                VehicleId = vehicle.VehicleId,
                VehicleNumber = vehicle.VehicleNumber,
                VehicleType = vehicle.VehicleType,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                PurchaseYear = vehicle.PurchaseYear,
                IsActive = vehicle.IsActive
            };
        }

        public async Task<(bool Success, string Message, VehicleResponseDto? Data)> AddVehicleAsync(VehicleCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.VehicleNumber) || string.IsNullOrWhiteSpace(dto.VehicleType) || string.IsNullOrWhiteSpace(dto.Brand))
                return (false, "Vehicle Number, Type, and Brand are required.", null); 

            if (dto.PurchaseYear <= 2000)
                return (false, "Purchase year must be greater than 2000.", null);

            var vehicle = new Vehicle
            {
                VehicleNumber = dto.VehicleNumber,
                VehicleType = dto.VehicleType,
                Brand = dto.Brand,
                Model = dto.Model,
                PurchaseYear = dto.PurchaseYear,
                IsActive = dto.IsActive
            };

            await _vehicleRepository.AddVehicleAsync(vehicle);

            var responseDto = new VehicleResponseDto
            {
                VehicleId = vehicle.VehicleId,
                VehicleNumber = vehicle.VehicleNumber,
                VehicleType = vehicle.VehicleType,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                PurchaseYear = vehicle.PurchaseYear,
                IsActive = vehicle.IsActive
            };

            return (true, "Vehicle added successfully", responseDto);
        }
        public async Task<(bool Success, string Message, VehicleResponseDto? Data)> UpdateVehicleAsync(int vehicleId, VehicleCreateDto dto)
        {
            var vehicle = await _vehicleRepository.GetVehicleByIdAsync(vehicleId);
            if (vehicle == null) return (false, "Vehicle not found", null);

            vehicle.VehicleNumber = dto.VehicleNumber;
            vehicle.VehicleType = dto.VehicleType;
            vehicle.Brand = dto.Brand;
            vehicle.Model = dto.Model;
            vehicle.PurchaseYear = dto.PurchaseYear;
            vehicle.IsActive = dto.IsActive;

            await _vehicleRepository.UpdateVehicleAsync(vehicle);

            var responseDto = new VehicleResponseDto
            {
                VehicleId = vehicle.VehicleId,
                VehicleNumber = vehicle.VehicleNumber,
                VehicleType = vehicle.VehicleType,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                PurchaseYear = vehicle.PurchaseYear,
                IsActive = vehicle.IsActive
            };

            return (true, "Vehicle updated successfully", responseDto);
        }

        public async Task<(bool Success, string Message)> DeleteVehicleAsync(int vehicleId)
        {
            var vehicle = await _vehicleRepository.GetVehicleByIdAsync(vehicleId);
            if (vehicle == null) return (false, "Vehicle not found");

            await _vehicleRepository.DeleteVehicleAsync(vehicle);
            return (true, "Vehicle deleted successfully");
        }
    }
}