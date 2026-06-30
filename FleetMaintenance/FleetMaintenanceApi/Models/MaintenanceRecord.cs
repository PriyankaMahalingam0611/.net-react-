using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FleetMaintenanceApi.Models
{
    public class MaintenanceRecord
    {
        [Key]
        public int MaintenanceId { get; set; } 
        [Required]
        public int VehicleId { get; set; } 
        [Required]
        public int DriverId { get; set; } 
        [Required]
        public DateTime ServiceDate { get; set; } 
        [Required, MaxLength(100)]
        public string ServiceType { get; set; } = string.Empty; 
        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal ServiceCost { get; set; } 
        [Required, MaxLength(30)]
        public string ServiceStatus { get; set; } = string.Empty; 
        [MaxLength(250)]
        public string? Remarks { get; set; } 
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow; 
        [ForeignKey("VehicleId")]
        public Vehicle? Vehicle { get; set; }

        [ForeignKey("DriverId")]
        public Driver? Driver { get; set; }
    }
}