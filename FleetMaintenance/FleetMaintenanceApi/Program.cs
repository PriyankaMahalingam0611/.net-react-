using Microsoft.EntityFrameworkCore;
using FleetMaintenanceApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<FleetMaintenanceApi.Repositories.Interfaces.IVehicleRepository, FleetMaintenanceApi.Repositories.Implementations.VehicleRepository>();
builder.Services.AddScoped<FleetMaintenanceApi.Repositories.Interfaces.IDriverRepository, FleetMaintenanceApi.Repositories.Implementations.DriverRepository>();
builder.Services.AddScoped<FleetMaintenanceApi.Repositories.Interfaces.IMaintenanceRepository, FleetMaintenanceApi.Repositories.Implementations.MaintenanceRepository>();

builder.Services.AddScoped<FleetMaintenanceApi.Services.Interfaces.IVehicleService, FleetMaintenanceApi.Services.Implementations.VehicleService>();
builder.Services.AddScoped<FleetMaintenanceApi.Services.Interfaces.IDriverService, FleetMaintenanceApi.Services.Implementations.DriverService>();
builder.Services.AddScoped<FleetMaintenanceApi.Services.Interfaces.IMaintenanceService, FleetMaintenanceApi.Services.Implementations.MaintenanceService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FleetMaintenanceDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173") 
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

app.Run();