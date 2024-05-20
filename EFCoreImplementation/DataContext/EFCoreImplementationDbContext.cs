using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreImplementation.DataContext
{
    public class EFCoreImplementationDbContext : DbContext
    {
        public EFCoreImplementationDbContext(DbContextOptions options)
            : base(options)
        { }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasKey(p => p.ProductId);
            modelBuilder.Entity<Product>().Property(p => p.Name)
                                          .IsRequired()
                                          .HasMaxLength(50);
            modelBuilder.Entity<Product>().Property(p => p.Status).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.Stock).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.Description)
                                          .IsRequired()
                                          .HasMaxLength(150);
            modelBuilder.Entity<Product>().Property(p => p.Price).IsRequired().HasColumnType("decimal(18, 2)");
            modelBuilder.Entity<Product>().Property(p => p.Discount)
                                          .HasDefaultValue(0)
                                          .HasAnnotation("Range", new int[] { 0, 100 });
            modelBuilder.Entity<Product>().Property(p => p.FinalPrice).HasColumnType("decimal(18, 2)");
        }

        // This method only was used to create migration
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);

        //    optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Initial Catalog=ProductsDb; Integrated Security=true;");
        //}
    }
}
