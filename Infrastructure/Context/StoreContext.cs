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
   // public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
   // public DbSet<OrderItem> OrderItems { get; set; }
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
            entity.HasKey(e => e.UserId).HasName("PK_Users");
            entity.Property(e => e.UserId)
            .ValueGeneratedOnAdd()
            .HasColumnName("UserId");

            entity.Property((e => e.Email))
            .IsRequired()
            .HasMaxLength(255);

            entity.Property(e => e.FirstName)
            .HasMaxLength(50);
            entity.Property(e => e.LastName)
            .HasMaxLength(50);

            entity.Property(e => e.Role)
            .HasMaxLength(50);

            entity.Property(e => e.Password);

            entity.Property(e => e.AddressId).HasColumnName("AdressID");

            entity.HasOne(d => d.Adress)
            .WithMany()
            .HasForeignKey(d => d.AddressId)
            .OnDelete(DeleteBehavior.SetNull);


            entity.HasMany(c => c.Orders)
            .WithOne(o => o.User)
            .HasForeignKey(o => o.UserID)
            .OnDelete(DeleteBehavior.SetNull);

            entity.HasIndex(e => e.Email).IsUnique();
        });

        //product
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Produkter");

            entity.Property(e => e.Id).HasColumnName("ProductID");
            entity.Property(e => e.CategoryId).HasColumnName("ProductCategoryID");
            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Status);

        });
            //entity.HasOne(d => d.ProductCategory)
            //      .WithMany(p => p.Products)
            //      .HasForeignKey(d => d.CategoryId)
            //      .HasConstraintName("FK_Produkter_Kategorier");


        //orderitem
        //modelBuilder.Entity<OrderItem>()
        //    .HasKey(oi => oi.OrderItemId);

        //modelBuilder.Entity<OrderItem>()
        //.HasOne(oi => oi.Order)  // Each OrderItem belongs to one Order
        //.WithMany(o => o.OrderItems)  // Each Order has many OrderItems
        //.HasForeignKey(oi => oi.OrderId)  // Define the foreign key in OrderItem
        //.OnDelete(DeleteBehavior.Cascade);  // Define how deletes are handled (if needed)

        //// Configure the relationship between Product and OrderItem
        //modelBuilder.Entity<OrderItem>()
        //    .HasOne(oi => oi.Product)  // Each OrderItem belongs to one Product
        //    .WithMany(p => p.OrderItems)  // Each Product has many OrderItems
        //    .HasForeignKey(oi => oi.ProductId)  // Define the foreign key in OrderItem
        //    .OnDelete(DeleteBehavior.Restrict);  // You can define the delete behavior as

        //modelBuilder.Entity<OrderItem>()
        //    .Property(oi => oi.Quantity);
        //modelBuilder.Entity<OrderItem>()
        //    .Property(oi => oi.Price);


        //order
        modelBuilder.Entity<Product>()
            .HasKey(p => p.Id);
        modelBuilder.Entity<Order>().HasKey(o => o.OrderId);

        modelBuilder.Entity<Order>()
            .HasMany(o => o.Products)
            .WithMany(p => p.Orders)
                .UsingEntity<OrderProduct>(
        j => j.HasOne(op => op.Product).WithMany(p => p.OrderProducts).HasForeignKey(op => op.ProductId),
        j => j.HasOne(op => op.Order).WithMany(o => o.OrderProducts).HasForeignKey(op => op.OrderId),
        j =>
        {
            j.HasKey(op => new { op.OrderId, op.ProductId });
        });


        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(o => o.OrderId);
            entity.Property(o => o.OrderId)
                .ValueGeneratedOnAdd();

            entity.HasOne(o => o.User)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.UserID)  // Ensure this matches the correct column in the database
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
