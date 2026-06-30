using Microsoft.EntityFrameworkCore;
using FleetMaintenanceApi.Models;

namespace FleetMaintenanceApi.Data
{
    public class FleetMaintenanceDbContext : DbContext
    {
        public FleetMaintenanceDbContext(DbContextOptions<FleetMaintenanceDbContext> options) : base(options) { }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<MaintenanceRecord> MaintenanceRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vehicle>().HasData(
                new Vehicle { VehicleId = 1, VehicleNumber = "TN38AB1234", VehicleType = "Truck", Brand = "Tata", Model = "Ace", PurchaseYear = 2021, IsActive = true }
            );

            modelBuilder.Entity<Driver>().HasData(
                new Driver { DriverId = 1, DriverName = "Ramesh Kumar", LicenseNumber = "DL2026TN1001", PhoneNumber = "9876543210", City = "Coimbatore", IsAvailable = true }
            );

            modelBuilder.Entity<MaintenanceRecord>().HasData(
                new MaintenanceRecord
                {
                    MaintenanceId = 1,
                    VehicleId = 1,
                    DriverId = 1,
                    ServiceDate = new DateTime(2026, 6, 15),
                    ServiceType = "Oil Change",
                    ServiceCost = 2500m,
                    ServiceStatus = "Completed",
                    Remarks = "Regular oil replacement",
                    CreatedDate = new DateTime(2026, 6, 1)
                }
            );
        }
    }
}