using Microsoft.EntityFrameworkCore;
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
        }
    }
}
