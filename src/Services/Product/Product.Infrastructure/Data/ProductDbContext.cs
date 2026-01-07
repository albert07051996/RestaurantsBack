using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;

namespace Product.Infrastructure.Data;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
    {
    }

    public DbSet<Product.Domain.Entities.Product> Products => Set<Product.Domain.Entities.Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<MenuItem> MenuItems => Set<MenuItem>();
    public DbSet<RestaurantSettings> RestaurantSettings => Set<RestaurantSettings>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Product Configuration
        modelBuilder.Entity<Product.Domain.Entities.Product>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
            entity.Property(e => e.ImageUrl).HasMaxLength(500);

            entity.HasOne(e => e.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasQueryFilter(e => !e.IsDeleted);
        });

        // Category Configuration
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);

            entity.HasQueryFilter(e => !e.IsDeleted);
        });

        // MenuItem Configuration
        modelBuilder.Entity<MenuItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.NameKa).IsRequired().HasMaxLength(200);
            entity.Property(e => e.NameEn).IsRequired().HasMaxLength(200);
            entity.Property(e => e.DescriptionKa).HasMaxLength(2000);
            entity.Property(e => e.DescriptionEn).HasMaxLength(2000);
            entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Category).IsRequired().HasMaxLength(100);
            entity.Property(e => e.ImageUrl).IsRequired().HasMaxLength(500);
            entity.Property(e => e.VideoUrl).HasMaxLength(500);
            entity.Property(e => e.IngredientsJson).HasColumnType("text");
            entity.Property(e => e.AllergensJson).HasColumnType("text");
            entity.Property(e => e.Volume).HasMaxLength(50);
            entity.Property(e => e.AlcoholContent).HasMaxLength(50);
            entity.Property(e => e.ServingTemperature).HasMaxLength(50);

            entity.HasIndex(e => e.Category);
            entity.HasIndex(e => e.IsAvailable);
            entity.HasIndex(e => e.SortOrder);

            entity.HasQueryFilter(e => !e.IsDeleted);
        });

        // RestaurantSettings Configuration
        modelBuilder.Entity<RestaurantSettings>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.RestaurantName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.RestaurantNameEn).IsRequired().HasMaxLength(200);
            entity.Property(e => e.TaglineKa).HasMaxLength(500);
            entity.Property(e => e.TaglineEn).HasMaxLength(500);
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.BackgroundImageUrl).HasMaxLength(500);
            entity.Property(e => e.LogoUrl).HasMaxLength(500);
            entity.Property(e => e.OpeningHours).HasMaxLength(500);
            entity.Property(e => e.PrimaryColor).HasMaxLength(20);
            entity.Property(e => e.SecondaryColor).HasMaxLength(20);

            entity.HasQueryFilter(e => !e.IsDeleted);
        });
    }
}
