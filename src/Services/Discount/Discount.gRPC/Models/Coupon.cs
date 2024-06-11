﻿namespace Discount.gRPC.Models
{
    public class Coupon
    {
        public int Id { get; set; }
        public string ProductId { get; set; } = default;
        public string Description { get; set; } = default;
        public int Amount { get; set; }
    }
}
