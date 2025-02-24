using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Linq;
using Zomato.Entity;

namespace Zomato.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Define tables using DbSet
       
        public DbSet<Address> Address { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartItem> CartItem { get; set; }
        public DbSet<Consumer> Consumer { get; set; }
        public DbSet<DeliveryPartner> DeliveryPartner { get; set; }
        public DbSet<DeliveryRequest> DeliveryRequest { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<MenuItem> MenuItem { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<OrderRequests> OrderRequest { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Restaurant> Restaurant { get; set; }
        public DbSet<RestaurantPartner> RestaurantPartner { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Wallet> Wallet { get; set; }
        public DbSet<WalletTransaction> WalletTransaction { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Automatically apply enum-to-string conversion for all enums in the database
            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.ClrType == null) continue; // Skip shadow properties

                var enumProperties = entityType.ClrType.GetProperties()
                    .Where(p => p.PropertyType.IsEnum);

                foreach (var property in enumProperties)
                {
                    var entityBuilder = modelBuilder.Entity(entityType.ClrType);
                    entityBuilder.Property(property.PropertyType, property.Name).HasConversion<string>();
                }
            }

            // Iterate over all entity types
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var foreignKey in entityType.GetForeignKeys())
                {
                    foreignKey.DeleteBehavior = DeleteBehavior.NoAction;  // Prevent automatic deletion
                }
            }

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var entityClrType = entityType.ClrType;

                if (entityClrType == null) continue; // Skip shadow types

                var enumProperties = entityClrType.GetProperties()
                    .Where(p => p.PropertyType.IsEnum);

                foreach (var property in enumProperties)
                {
                    // Convert the enum to string
                    modelBuilder.Entity(entityClrType)
                        .Property(property.Name)
                        .HasConversion<string>();

                    // Get enum values
                    var enumValues = Enum.GetNames(property.PropertyType);
                    var enumConstraint = $"[{property.Name}] IN ({string.Join(", ", enumValues.Select(v => $"'{v}'"))})";

                    // Apply CHECK constraint
                    modelBuilder.Entity(entityClrType)
                        .ToTable(t => t.HasCheckConstraint($"CHK_{entityClrType.Name}_{property.Name}", enumConstraint));
                }
            }
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    var entity = entry.Entity;
                    var properties = entity.GetType().GetProperties()
                        .Where(p => p.PropertyType.IsEnum);

                    foreach (var property in properties)
                    {
                        var value = property.GetValue(entity);
                        if (value != null && !Enum.IsDefined(property.PropertyType, value))
                        {
                            throw new ArgumentException($"Invalid value '{value}' for enum {property.PropertyType.Name} in entity {entity.GetType().Name}.");
                        }
                    }
                }
            }
            return base.SaveChanges();
        }
    }
}
