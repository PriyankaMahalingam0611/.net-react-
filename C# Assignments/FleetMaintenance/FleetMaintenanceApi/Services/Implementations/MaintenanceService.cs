using Microsoft.EntityFrameworkCore;
using FleetMaintenanceApi.DTOs;
using FleetMaintenanceApi.Models;
using FleetMaintenanceApi.Repositories.Interfaces;
using FleetMaintenanceApi.Services.Interfaces;

namespace FleetMaintenanceApi.Services.Implementations
{
    public class MaintenanceService : IMaintenanceService
    {
        private readonly IMaintenanceRepository _maintenanceRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IDriverRepository _driverRepository;

        public MaintenanceService(IMaintenanceRepository maintenanceRepository, IVehicleRepository vehicleRepository, IDriverRepository driverRepository)
        {
            _maintenanceRepository = maintenanceRepository;
            _vehicleRepository = vehicleRepository;
            _driverRepository = driverRepository;
        }

        public async Task<(bool Success, string Message, PagedResponseDto<MaintenanceResponseDto>? Data)> GetPagedMaintenanceRecordsAsync(MaintenanceFilterRequestDto filter)
        {
            if (filter.PageNumber <= 0) return (false, "Page number must be greater than zero", null); 
            if (filter.PageSize <= 0) return (false, "Page size must be greater than zero", null); 
            if (filter.PageSize > 100) return (false, "Page size cannot be greater than 100", null); 

            var allowedSortDirections = new[] { "asc", "desc" };
            if (!string.IsNullOrWhiteSpace(filter.SortDirection) && !allowedSortDirections.Contains(filter.SortDirection.ToLower()))
                return (false, "Invalid sort direction. Allowed values are asc and desc", null); 

            var allowedSortFields = new[] { "maintenanceid", "servicedate", "servicetype", "servicecost", "servicestatus", "vehiclenumber", "drivername", "createddate" };
            if (!string.IsNullOrWhiteSpace(filter.SortBy) && !allowedSortFields.Contains(filter.SortBy.ToLower()))
                return (false, "Invalid sort field", null); 

            var query = _maintenanceRepository.GetMaintenanceRecordsQueryable();

            if (filter.VehicleId.HasValue)
                query = query.Where(m => m.VehicleId == filter.VehicleId.Value);

            if (filter.DriverId.HasValue)
                query = query.Where(m => m.DriverId == filter.DriverId.Value);

            if (!string.IsNullOrWhiteSpace(filter.ServiceStatus))
                query = query.Where(m => m.ServiceStatus.ToLower() == filter.ServiceStatus.ToLower());

            if (filter.FromDate.HasValue)
                query = query.Where(m => m.ServiceDate >= filter.FromDate.Value);

            if (filter.ToDate.HasValue)
                query = query.Where(m => m.ServiceDate <= filter.ToDate.Value);

            bool isDescending = filter.SortDirection?.ToLower() == "desc";
            query = filter.SortBy?.ToLower() switch
            {
                "maintenanceid" => isDescending ? query.OrderByDescending(m => m.MaintenanceId) : query.OrderBy(m => m.MaintenanceId),
                "servicedate" => isDescending ? query.OrderByDescending(m => m.ServiceDate) : query.OrderBy(m => m.ServiceDate),
                "servicetype" => isDescending ? query.OrderByDescending(m => m.ServiceType) : query.OrderBy(m => m.ServiceType),
                "servicecost" => isDescending ? query.OrderByDescending(m => m.ServiceCost) : query.OrderBy(m => m.ServiceCost),
                "servicestatus" => isDescending ? query.OrderByDescending(m => m.ServiceStatus) : query.OrderBy(m => m.ServiceStatus),
                "vehiclenumber" => isDescending ? query.OrderByDescending(m => m.Vehicle!.VehicleNumber) : query.OrderBy(m => m.Vehicle!.VehicleNumber),
                "drivername" => isDescending ? query.OrderByDescending(m => m.Driver!.DriverName) : query.OrderBy(m => m.Driver!.DriverName),
                "createddate" => isDescending ? query.OrderByDescending(m => m.CreatedDate) : query.OrderBy(m => m.CreatedDate),
                _ => query.OrderBy(m => m.ServiceDate)
            };

            int totalRecords = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalRecords / (double)filter.PageSize);

            var records = await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            var responseData = records.Select(m => new MaintenanceResponseDto
            {
                MaintenanceId = m.MaintenanceId,
                VehicleId = m.VehicleId,
                VehicleNumber = m.Vehicle!.VehicleNumber,
                VehicleType = m.Vehicle.VehicleType,
                DriverId = m.DriverId,
                DriverName = m.Driver!.DriverName,
                ServiceDate = m.ServiceDate,
                ServiceType = m.ServiceType,
                ServiceCost = m.ServiceCost,
                ServiceStatus = m.ServiceStatus,
                Remarks = m.Remarks,
                CreatedDate = m.CreatedDate
            }).ToList();

            var pagedResponse = new PagedResponseDto<MaintenanceResponseDto>
            {
                StatusCode = 200,
                Message = "Maintenance records retrieved successfully",
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                HasPreviousPage = filter.PageNumber > 1,
                HasNextPage = filter.PageNumber < totalPages,
                Data = responseData
            }; 

            return (true, pagedResponse.Message, pagedResponse);
        }

        public async Task<(bool Success, string Message, MaintenanceResponseDto? Data)> AddMaintenanceRecordAsync(MaintenanceCreateDto dto)
        {
            if (!await _vehicleRepository.VehicleExistsAsync(dto.VehicleId)) return (false, "VehicleId must exist.", null);
            if (!await _driverRepository.DriverExistsAsync(dto.DriverId)) return (false, "DriverId must exist.", null);
            if (dto.ServiceCost <= 0) return (false, "Service cost must be greater than 0.", null);
            if (string.IsNullOrWhiteSpace(dto.ServiceStatus)) return (false, "Service status is required.", null);

            var maintenanceRecord = new MaintenanceRecord
            {
                VehicleId = dto.VehicleId,
                DriverId = dto.DriverId,
                ServiceDate = dto.ServiceDate,
                ServiceType = dto.ServiceType,
                ServiceCost = dto.ServiceCost,
                ServiceStatus = dto.ServiceStatus,
                Remarks = dto.Remarks,
                CreatedDate = DateTime.UtcNow
            };

            await _maintenanceRepository.AddMaintenanceRecordAsync(maintenanceRecord);

            var vehicle = await _vehicleRepository.GetVehicleByIdAsync(dto.VehicleId);
            var driver = await _driverRepository.GetDriverByIdAsync(dto.DriverId);

            var responseDto = new MaintenanceResponseDto
            {
                MaintenanceId = maintenanceRecord.MaintenanceId,
                VehicleId = maintenanceRecord.VehicleId,
                VehicleNumber = vehicle!.VehicleNumber,
                VehicleType = vehicle.VehicleType,
                DriverId = maintenanceRecord.DriverId,
                DriverName = driver!.DriverName,
                ServiceDate = maintenanceRecord.ServiceDate,
                ServiceType = maintenanceRecord.ServiceType,
                ServiceCost = maintenanceRecord.ServiceCost,
                ServiceStatus = maintenanceRecord.ServiceStatus,
                Remarks = maintenanceRecord.Remarks,
                CreatedDate = maintenanceRecord.CreatedDate
            };

            return (true, "Maintenance record added successfully", responseDto);
        }
    }
}