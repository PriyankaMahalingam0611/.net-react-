namespace FleetMaintenanceApi.DTOs
{
    public class MaintenanceCreateDto
    {
        public int VehicleId { get; set; }
        public int DriverId { get; set; }
        public DateTime ServiceDate { get; set; }
        public string ServiceType { get; set; } = string.Empty;
        public decimal ServiceCost { get; set; }
        public string ServiceStatus { get; set; } = string.Empty;
        public string? Remarks { get; set; }
    }
}
