namespace Discount.gRPC.Services
{
    public class DiscountService
        (DiscountContext dbContext, ILogger<DiscountService> logger)
        : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.
                Coupons.
                FirstOrDefaultAsync(c => c.ProductId == request.ProductId);

            if (coupon == null)
                coupon = new Coupon { ProductId = "No Discount", Amount = 0, Description = "No Discount" };

            logger.LogInformation("Discount is retrieved for ProductId: {ProductId}, Amount: {Amount}", coupon.ProductId, coupon.Amount);

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Adapt<Coupon>();
            if (coupon == null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Discount Request"));

            dbContext.Coupons.Add(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount is successfully created. ProductId: {ProductId}", coupon.ProductId);

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Adapt<Coupon>();
            if (coupon == null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Discount Request"));

            var isExist = await dbContext.Coupons.AnyAsync(c => c.ProductId == coupon.ProductId);
            if (!isExist)
                throw new RpcException(new Status(StatusCode.NotFound, "Discount is not found"));

            dbContext.Coupons.Update(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount is successfully updated. ProductId: {ProductId}", coupon.ProductId);

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons.FirstOrDefaultAsync(c => c.ProductId == request.ProductId);
            if (coupon == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Discount is not found"));

            dbContext.Coupons.Remove(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount is successfully deleted. ProductId: {ProductId}", coupon.ProductId);

            return new DeleteDiscountResponse { Success = true };
        }
    }
}
