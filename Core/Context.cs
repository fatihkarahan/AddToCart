using Core.Domain.Entities.Customer;
using Core.Domain.Entities.Product;
using Core.Domain.Entities.ShoppingCart;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Core
{
    public class Context : DbContext
    {
        /// <summary>
        /// context
        /// </summary>
        /// <param name="options"></param>
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        /// <summary>
        /// customer table set
        /// </summary>
        public DbSet<Customer> Customer { get; set; }

        /// <summary>
        /// product table set
        /// </summary>
        public DbSet<Product> Product { get; set; }

        /// <summary>
        /// shopping cart table set
        /// </summary>
        public DbSet<ShoppingCart> ShoppingCart { get; set; }

        /// <summary>
        /// model creating
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>(ConfigureProduct);
            builder.Entity<Customer>(ConfigureCustomer);
            builder.Entity<ShoppingCart>(ConfigureShoppingCart);
        }

        /// <summary>
        /// congigure table
        /// </summary>
        /// <param name="builder"></param>
        private void ConfigureProduct(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
               .IsRequired();

            builder.Property(cb => cb.Sku)
                .IsRequired()
                .HasMaxLength(10);
        }

        /// <summary>
        /// congigure table
        /// </summary>
        /// <param name="builder"></param>
        private void ConfigureCustomer(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
               .IsRequired();

            builder.Property(cb => cb.Email)
                .IsRequired()
                .HasMaxLength(100);
        }

        /// <summary>
        /// congigure table
        /// </summary>
        /// <param name="builder"></param>
        private void ConfigureShoppingCart(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.ToTable("ShoppingCart");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)////.ValueGeneratedNever()
               .IsRequired();

            builder.Property(cb => cb.ProductId)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(cb => cb.CustomerId)
               .IsRequired()
               .HasMaxLength(10);
        }

    }
}
