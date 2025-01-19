using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using Shipping.Domain.Entities;

namespace Shipping.Infrastructure.Context
{
    public class ShippingDbContext : DbContext
    {
        public ShippingDbContext(DbContextOptions<ShippingDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<ShipmentTracking> ShipmentTrackings { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ShipmentType> ShipmentTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Order Configuration
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CustomerName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.DeliveryAddress).HasMaxLength(200).IsRequired();
                entity.Property(e => e.TotalAmount).HasPrecision(18, 2);
                entity.Property(e => e.Status).HasMaxLength(20).IsRequired();
            });

            // Shipment Configuration
            modelBuilder.Entity<Shipment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Status).HasMaxLength(20).IsRequired();
                entity.Property(e => e.ShippingCost).HasPrecision(18, 2);
                entity.Property(e => e.OriginName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.DestinationName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.TransportedGoods).HasMaxLength(250).IsRequired();

                entity.HasOne(e => e.Order)
                      .WithMany(e => e.Shipments)
                      .HasForeignKey(e => e.OrderId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Driver)
                      .WithMany(e => e.Shipments)
                      .HasForeignKey(e => e.DriverId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Vehicle)
                      .WithMany(e => e.Shipments)
                      .HasForeignKey(e => e.VehicleId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.ShipmentType)
                     .WithMany(st => st.Shipments)
                     .HasForeignKey(e => e.ShipmentTypeId)
                     .OnDelete(DeleteBehavior.Restrict);
            });

            // ShipmentType
            modelBuilder.Entity<ShipmentType>(entity =>
            {
                entity.HasKey(st => st.Id);
                entity.Property(st => st.Name).IsRequired().HasMaxLength(100);
                entity.Property(st => st.Description).HasMaxLength(500);
                entity.HasMany(st => st.Shipments)
                    .WithOne(s => s.ShipmentType)
                    .HasForeignKey(s => s.ShipmentTypeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ShipmentTracking Configuration
            modelBuilder.Entity<ShipmentTracking>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Status).HasMaxLength(20).IsRequired();
                entity.Property(e => e.EstimatedDistance).HasPrecision(18, 2);

                entity.HasOne(e => e.Shipment)
                      .WithMany(e => e.ShipmentTrackings)
                      .HasForeignKey(e => e.ShipmentId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Payment Configuration
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Amount).HasPrecision(18, 2);
                entity.Property(e => e.PaymentMethod).HasMaxLength(30).IsRequired();
                entity.Property(e => e.Status).HasMaxLength(20).IsRequired();

                entity.HasOne(e => e.Shipment)
                      .WithOne(e => e.Payment)
                      .HasForeignKey<Payment>(e => e.ShipmentId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Driver Configuration
            modelBuilder.Entity<Driver>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).HasMaxLength(50).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(50).IsRequired();
                entity.Property(e => e.License).HasMaxLength(20).IsRequired();
                entity.Property(e => e.Phone).HasMaxLength(20).IsRequired();
                entity.Property(e => e.Status).HasMaxLength(20).IsRequired();

                entity.HasMany(d => d.DriverVehicles)
                    .WithOne(dv => dv.Driver)
                    .HasForeignKey(dv => dv.DriverId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Vehicle Configuration
            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PlateNumber).HasMaxLength(20).IsRequired();
                entity.Property(e => e.Model).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Type).HasMaxLength(30).IsRequired();
                entity.Property(e => e.Status).HasMaxLength(20).IsRequired();
            });

            // Configuración de DriverVehicle
            modelBuilder.Entity<DriverVehicle>(entity =>
            {
                entity.HasKey(e => new { e.DriverId, e.VehicleId });
                entity.Property(e => e.AssignedDate).IsRequired();
                entity.Property(e => e.AssignmentStatus).HasMaxLength(20).IsRequired();

                entity.HasOne(dv => dv.Driver)
                      .WithMany(d => d.DriverVehicles)
                      .HasForeignKey(dv => dv.DriverId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(dv => dv.Vehicle)
                      .WithMany(v => v.DriverVehicles)
                      .HasForeignKey(dv => dv.VehicleId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Seed();
        }
    }

    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {// Seeding para ShipmentType
            var shipmentTypes = new[]
            {
                new ShipmentType { Id = Guid.NewGuid(), Name = "Standard", Description = "Standard delivery with normal shipping times" },
                new ShipmentType { Id = Guid.NewGuid(), Name = "Express", Description = "Faster delivery service with additional charges" },
                new ShipmentType { Id = Guid.NewGuid(), Name = "Overnight", Description = "Delivery guaranteed by the next business day" },
            };
            modelBuilder.Entity<ShipmentType>().HasData(shipmentTypes);

            // Seeding para Vehicles
            var vehicles = new[]
            {
                new Vehicle { Id = Guid.NewGuid(), PlateNumber = "XYZ123", Model = "Ford Transit", Type = "Van", Status = "Active" },
                new Vehicle { Id = Guid.NewGuid(), PlateNumber = "ABC789", Model = "Mercedes Sprinter", Type = "Truck", Status = "Active" },
                new Vehicle { Id = Guid.NewGuid(), PlateNumber = "LMN456", Model = "Toyota HiAce", Type = "Van", Status = "Maintenance" },
            };
            modelBuilder.Entity<Vehicle>().HasData(vehicles);

            // Seeding para Drivers
            var drivers = new[]
            {
                new Driver { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", License = "D123456", Phone = "555-1234", Status = "Available" },
                new Driver { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Smith", License = "S987654", Phone = "555-5678", Status = "Unavailable" },
            };
            modelBuilder.Entity<Driver>().HasData(drivers);

            // Seeding para DriverVehicle
            var driverVehicles = new[]
            {
                new DriverVehicle { DriverId = drivers[0].Id, VehicleId = vehicles[0].Id, AssignedDate = DateTime.Now.AddDays(-30), AssignmentStatus = "Assigned", Notes = "Regular route" },
                new DriverVehicle { DriverId = drivers[1].Id, VehicleId = vehicles[1].Id, AssignedDate = DateTime.Now.AddDays(-10), AssignmentStatus = "Assigned", Notes = "Backup driver" },
            };
            modelBuilder.Entity<DriverVehicle>().HasData(driverVehicles);

            // Seeding para Orders
            var orders = new[]
            {
                new Order { Id = Guid.NewGuid(), CustomerName = "Acme Corp", DeliveryAddress = "123 Elm Street", OrderDate = DateTime.Now.AddDays(-7), TotalAmount = 150.00m, Status = "Pending" },
                new Order { Id = Guid.NewGuid(), CustomerName = "Global Ltd", DeliveryAddress = "456 Oak Avenue", OrderDate = DateTime.Now.AddDays(-5), TotalAmount = 200.00m, Status = "Completed" },
            };
            modelBuilder.Entity<Order>().HasData(orders);

            // Seeding para Shipments
            var shipments = new[]
            {
                new Shipment
                {
                    Id = Guid.NewGuid(),
                    OrderId = orders[0].Id,
                    DriverId = drivers[0].Id,
                    VehicleId = vehicles[0].Id,
                    ShipmentTypeId = shipmentTypes[0].Id,
                    OriginLocation = new Point(-73.935242, 40.730610) { SRID = 4326 },
                    OriginName = "Warehouse A",
                    DestinationLocation = new Point(-74.0060, 40.7128) { SRID = 4326 },
                    DestinationName = "Retail Store B",
                    TransportedGoods = "Electronics - TVs, Laptops",
                    ScheduledDate = DateTime.Now.AddDays(-6),
                    DeliveryDate = DateTime.Now.AddDays(-5),
                    Status = "Delivered",
                    ShippingCost = 50.00m
                },
                new Shipment
                {
                    Id = Guid.NewGuid(),
                    OrderId = orders[1].Id,
                    DriverId = drivers[1].Id,
                    VehicleId = vehicles[1].Id,
                    ShipmentTypeId = shipmentTypes[1].Id,
                    OriginLocation = new Point(-118.243683, 34.052235) { SRID = 4326 },
                    OriginName = "Warehouse C",
                    DestinationLocation = new Point(-117.161083, 32.715736) { SRID = 4326 },
                    DestinationName = "Retail Store D",
                    TransportedGoods = "Furniture - Tables, Chairs",
                    ScheduledDate = DateTime.Now.AddDays(-4),
                    DeliveryDate = null,
                    Status = "In Transit",
                    ShippingCost = 75.00m
                }
            };
            modelBuilder.Entity<Shipment>().HasData(shipments);

            // Seeding para Payments
            var payments = new[]
            {
                new Payment { Id = Guid.NewGuid(), ShipmentId = shipments[0].Id, Amount = 50.00m, PaymentDate = DateTime.Now.AddDays(-5), PaymentMethod = "Credit Card", Status = "Completed" },
                new Payment { Id = Guid.NewGuid(), ShipmentId = shipments[1].Id, Amount = 75.00m, PaymentDate = DateTime.Now.AddDays(-3), PaymentMethod = "Bank Transfer", Status = "Pending" },
            };
            modelBuilder.Entity<Payment>().HasData(payments);

            // Seeding para ShipmentTracking
            var shipmentTrackings = new[]
            {
                new ShipmentTracking { Id = Guid.NewGuid(), ShipmentId = shipments[0].Id, CurrentLocation = new Point(-73.9915, 40.7308) { SRID = 4326 }, TrackedTime = DateTime.Now.AddDays(-6), Status = "On Route", EstimatedDistance = 10.0m, EstimatedMinutes = 30 },
                new ShipmentTracking { Id = Guid.NewGuid(), ShipmentId = shipments[1].Id, CurrentLocation = new Point(-118.2437, 34.0522) { SRID = 4326 }, TrackedTime = DateTime.Now.AddDays(-4), Status = "In Transit", EstimatedDistance = 100.0m, EstimatedMinutes = 120 },
            };
            modelBuilder.Entity<ShipmentTracking>().HasData(shipmentTrackings);
        }
    }
}
