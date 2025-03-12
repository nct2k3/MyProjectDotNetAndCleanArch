using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Common.Dbcontext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<FoodItems> FoodItems { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderDetail> OrderDetails { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Thiết lập quan hệ giữa Order và User
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany() 
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Thiết lập quan hệ giữa OrderDetail và Order
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany()
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Thiết lập quan hệ giữa OrderDetail và FoodItems
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.FoodItems)
                .WithMany() 
                .HasForeignKey(od => od.FoodId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}