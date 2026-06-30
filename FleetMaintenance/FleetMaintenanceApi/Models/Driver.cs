using System.ComponentModel.DataAnnotations;
namespace FleetMaintenanceApi.Models
{
    public class Driver
    {
        [Key]
        public int DriverId { get; set; } 
        [Required, MaxLength(100)]
        public string DriverName { get; set; } = string.Empty; 
        [Required, MaxLength(50)]
        public string LicenseNumber { get; set; } = string.Empty; 
        [Required, MaxLength(15)]
        public string PhoneNumber { get; set; } = string.Empty; 
        [Required, MaxLength(50)]
        public string City { get; set; } = string.Empty; 
        [Required]
        public bool IsAvailable { get; set; } 
    }
}