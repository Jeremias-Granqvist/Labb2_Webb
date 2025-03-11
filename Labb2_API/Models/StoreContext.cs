using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Labb2_Shared;
using Labb2_Shared.Models;
namespace Labb2_API.Models;

public partial class StoreContext : DbContext
{
    public StoreContext()
    {
    }
    private readonly IConfiguration _configuration;
    public StoreContext(DbContextOptions<StoreContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Adress> Adresses { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
	}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Adress>(entity =>
        {
            entity.Property(e => e.AdressId).HasColumnName("AdressID");
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Country).HasMaxLength(50);
            entity.Property(e => e.StreetName).HasMaxLength(50);
            entity.Property(e => e.ZipCode).HasMaxLength(50);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK_Kategorier");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(50);
        });

        modelBuilder.Entity<Customer>((Action<Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Customer>>)(entity =>
        {
            entity.Property((System.Linq.Expressions.Expression<Func<Customer, int>>)(e => (int)e.CustomerId))
                .ValueGeneratedNever()
                .HasColumnName("CustomerID");
            entity.Property(e => e.AdressId).HasColumnName("AdressID");
            entity.Property((System.Linq.Expressions.Expression<Func<Customer, string?>>)(e => e.Email)).HasMaxLength(50);
            entity.Property((System.Linq.Expressions.Expression<Func<Customer, string?>>)(e => e.Firstname)).HasMaxLength(50);
            entity.Property((System.Linq.Expressions.Expression<Func<Customer, string?>>)(e => e.Lastname)).HasMaxLength(50);

            entity.HasOne(d => d.Adress).WithMany(p => p.Customers)
                .HasForeignKey(d => d.AdressId)
                .HasConstraintName("FK_Customers_Adresses");
        }));

        modelBuilder.Entity<Product>((Action<Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Product>>)(entity =>
        {
            entity.HasKey((System.Linq.Expressions.Expression<Func<Product, object?>>)(e => e.ProductId)).HasName("PK_Produkter");

            entity.Property((System.Linq.Expressions.Expression<Func<Product, int>>)(e => (int)e.ProductId)).HasColumnName("ProductID");
            entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");
            entity.Property((System.Linq.Expressions.Expression<Func<Product, string?>>)(e => e.ProductDescription)).HasMaxLength(50);
            entity.Property((System.Linq.Expressions.Expression<Func<Product, string?>>)(e => e.ProductName)).HasMaxLength(50);
            entity.Property((System.Linq.Expressions.Expression<Func<Product, string?>>)(e => e.Status)).HasMaxLength(50);

            entity.HasOne(d => d.ProductCategory).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductCategoryId)
                .HasConstraintName("FK_Produkter_Kategorier");
        }));

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
