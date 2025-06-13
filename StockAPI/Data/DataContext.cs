using Microsoft.EntityFrameworkCore;
using StockAPI.Models;

namespace StockAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        // Tabla Products
        public DbSet<Product> Products { get; set; }

        // Tabla Boxes
        public DbSet<Box> Boxes { get; set; }

        // Tabla BoxProductTransactions
        public DbSet<BoxProductTransaction> BoxProductTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Box>().ToTable("Boxes");
            modelBuilder.Entity<BoxProductTransaction>().ToTable("BoxProductTransactions");

            // Relación Product → BoxProductTransaction
            modelBuilder.Entity<BoxProductTransaction>()
                .HasOne(bpt => bpt.Product)
                .WithMany(p => p.Transactions)
                .HasForeignKey(bpt => bpt.ProductId);

            // Relación Box → BoxProductTransaction
            modelBuilder.Entity<BoxProductTransaction>()
                .HasOne(bpt => bpt.Box)
                .WithMany(b => b.Transactions)
                .HasForeignKey(bpt => bpt.BoxId);
        }
    }
}
