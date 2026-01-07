using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Identity.Application.Common.Interfaces;

namespace Identity.Infrastructure.Data;

public class IdentityDbContext : DbContext, IApplicationDbContext
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<MenuItem> MenuItems => Set<MenuItem>();
    public DbSet<FoodCategory> FoodCategories => Set<FoodCategory>();


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ConfigureWarnings(w =>
            w.Ignore(RelationalEventId.PendingModelChangesWarning));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<FoodCategory>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.NameKa).IsRequired().HasMaxLength(100);
            entity.Property(e => e.NameEn).IsRequired().HasMaxLength(100);
            entity.Property(e => e.DescriptionKa).IsRequired().HasMaxLength(100);
            entity.Property(e => e.DescriptionEn).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Priority).IsRequired();

            entity.HasQueryFilter(e => !e.IsDeleted);

        });
        // User Configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(256);
            entity.Property(e => e.UserName).IsRequired().HasMaxLength(256);
            entity.Property(e => e.PasswordHash).IsRequired();
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);

            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.UserName).IsUnique();

            entity.HasQueryFilter(e => !e.IsDeleted);
        });

        // Role Configuration
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);

            entity.HasIndex(e => e.Name).IsUnique();

            entity.HasQueryFilter(e => !e.IsDeleted);

            //Seed default roles
            entity.HasData(
                new { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Admin", Description = "Administrator role", CreatedAt = new DateTime(2026, 1, 5, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "User", Description = "Regular user role",  CreatedAt = new DateTime(2026, 1, 5, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false }
            );
        });

        // UserRole Configuration
        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasOne(e => e.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => new { e.UserId, e.RoleId }).IsUnique();

            entity.HasQueryFilter(e => !e.IsDeleted);
        });

        // menu 
        modelBuilder.Entity<MenuItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.NameKa).IsRequired().HasMaxLength(60);
            entity.Property(e => e.NameEn).IsRequired().HasMaxLength(60);
            entity.Property(e => e.DescriptionKa).IsRequired();
            entity.Property(e => e.DescriptionEn).IsRequired();
            entity.Property(e => e.Price).IsRequired().HasColumnType("decimal(18,2)");
            entity.HasOne(m => m.FoodCategory)
                  .WithMany(c => c.MenuItems)
                  .HasForeignKey(m => m.FoodCategoryId)
                  .OnDelete(DeleteBehavior.Restrict);
            entity.Property(e => e.ImageUrl).HasMaxLength(500); 
            entity.Property(e => e.VideoUrl).HasMaxLength(500);
            entity.Property(e => e.PreparationTimeMinutes);
            entity.Property(e => e.IsAvailable).IsRequired();
            entity.Property(e => e.Volume).HasMaxLength(50);
            entity.Property(e => e.AlcoholContent).HasMaxLength(50);
            entity.Property(e => e.Ingredients).IsRequired();
            entity.Property(e => e.IngredientsEn).IsRequired();
            entity.Property(e => e.IsVeganFood).IsRequired();
            entity.Property(e => e.Comment).HasMaxLength(300);
            entity.Property(e => e.Calories);
            entity.Property(e => e.SpicyLevel);
            entity.HasQueryFilter(e => !e.IsDeleted);
        });

  
    }
}
