using Labb2_Infrastructure.Authentication.Models;
using Labb2_Shared.Models;
using Microsoft.EntityFrameworkCore;
namespace Labb2_Infrastructure;

public partial class StoreContext : DbContext
{
    public StoreContext()
    {
    }
    public StoreContext(DbContextOptions<StoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Adress> Adresses { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<ApplicationUser> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PK_Users");
            entity.Property(e => e.Password).HasColumnName("Password");
            entity.Property(e => e.Name);
        });

        //product
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey((e => e.Id)).HasName("PK_Produkter");

            entity.Property((e => e.Id)).HasColumnName("ProductID");
            entity.Property(e => e.CategoryId).HasColumnName("ProductCategoryID");
            entity.Property((e => e.Description)).HasMaxLength(50);
            entity.Property((e => e.Name)).HasMaxLength(50);
            entity.Property((e => e.Status));

            entity.HasOne(d => d.ProductCategory)
                .WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Produkter_Kategorier");
        });


        //orderitem
        modelBuilder.Entity<OrderItem>()
            .HasKey(oi => oi.OrderItemId);

        modelBuilder.Entity<OrderItem>()
        .HasOne(oi => oi.Order)  // Each OrderItem belongs to one Order
        .WithMany(o => o.OrderItems)  // Each Order has many OrderItems
        .HasForeignKey(oi => oi.OrderId)  // Define the foreign key in OrderItem
        .OnDelete(DeleteBehavior.Cascade);  // Define how deletes are handled (if needed)

        // Configure the relationship between Product and OrderItem
        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Product)  // Each OrderItem belongs to one Product
            .WithMany(p => p.OrderItems)  // Each Product has many OrderItems
            .HasForeignKey(oi => oi.ProductId)  // Define the foreign key in OrderItem
            .OnDelete(DeleteBehavior.Restrict);  // You can define the delete behavior as

        modelBuilder.Entity<OrderItem>()
            .Property(oi => oi.Quantity);
        modelBuilder.Entity<OrderItem>()
            .Property(oi => oi.Price);

        //customer
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK_Customers");
            entity.Property(e => e.CustomerId)
            .ValueGeneratedOnAdd()
            .HasColumnName("CustomerId");

            entity.Property(e => e.AdressId).HasColumnName("AdressID");
            entity.Property((e => e.Email)).HasMaxLength(50);
            entity.Property((e => e.Firstname)).HasMaxLength(50);
            entity.Property((e => e.Lastname)).HasMaxLength(50);

            entity.HasOne(d => d.Adress)
            .WithMany(p => p.Customers)
            .HasForeignKey(d => d.AdressId)
            .HasConstraintName("FK_Customers_Adresses");

            entity.HasMany(c => c.Orders)
            .WithOne(o => o.Customer)
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.SetNull);
    
        });
        
        //order
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(o => o.OrderId);

            entity.HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.SetNull);

            entity.Property(o => o.DateOfOrder).IsRequired();
        });

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
            entity.HasKey(e => e.Id).HasName("PK_Kategorier");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
