using System.ComponentModel.DataAnnotations;

namespace FleetMaintenanceApi.Models
{
    public class Vehicle
    {
        [Key]
        public int VehicleId { get; set; }
        [Required, MaxLength(20)]
        public string VehicleNumber { get; set; } = string.Empty;
        [Required, MaxLength(50)]
        public string VehicleType { get; set; } = string.Empty;
        [Required, MaxLength(50)]
        public string Brand { get; set; } = string.Empty;
        [Required, MaxLength(50)]
        public string Model { get; set; } = string.Empty;
        [Required]
        public int PurchaseYear { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}