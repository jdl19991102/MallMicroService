using System;
using System.Collections.Generic;

namespace Payment.Api.Models
{
    public partial class PaymentInfo
    {
        public int Id { get; set; }
        public string PaymentId { get; set; } = null!;
        public int OrderId { get; set; }
        public decimal PaymentAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        /// <summary>
        /// 0 待支付，1 已支付，2 支付失败
        /// </summary>
        public int PaymentStatus { get; set; }
        /// <summary>
        /// 支付方式：支付宝、微信、PayPal
        /// </summary>
        public string PaymentMethod { get; set; } = null!;
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
