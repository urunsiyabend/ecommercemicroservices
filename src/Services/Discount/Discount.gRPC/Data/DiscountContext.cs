using Discount.gRPC.Models;

namespace Discount.gRPC.Data
{
    public class DiscountContext: DbContext
    {
        public DbSet<Coupon> Coupons { get; set; } = default!;

        public DiscountContext(DbContextOptions<DiscountContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon { Id = 1, ProductId = "1", Description = "Discount 10%", Amount = 10 },
                new Coupon { Id = 2, ProductId = "2", Description = "Discount 20%", Amount = 20 },
                new Coupon { Id = 3, ProductId = "3", Description = "Discount 30%", Amount = 30 }
            );
        }
    }
}
