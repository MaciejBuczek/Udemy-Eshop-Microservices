﻿using Discount.GRPC.Data;
using Discount.GRPC.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.GRPC.Services
{
    internal class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger) : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName.Equals(request.ProductName))
                ?? new Coupon { ProductName = request.ProductName, Description = "No Coupon", Amount = 0 };
            var couponModel = coupon.Adapt<CouponModel>();

            logger.LogInformation("Discount is retrieved for Productname: {productName}", request.ProductName);

            return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            if(request.Coupon is null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid required argument"));
            }

            var coupon = request.Coupon.Adapt<Coupon>();

            dbContext.Coupons.Add(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount is created as Productname: {productName}", request.Coupon.ProductName);

            var couponModel = coupon.Adapt<CouponModel>();

            return couponModel;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>() ?? throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid required argument"));
            dbContext.Coupons.Update(coupon);
            await dbContext.SaveChangesAsync();

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var couponModel = await GetDiscount(new GetDiscountRequest { ProductName = request.ProductName }, context) ?? throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid required argument"));
            dbContext.ChangeTracker.Clear();

            var coupon = couponModel.Adapt<Coupon>();
            dbContext.Coupons.Remove(coupon);
            await dbContext.SaveChangesAsync();
            return new DeleteDiscountResponse { Success = true };
        }
    }
}