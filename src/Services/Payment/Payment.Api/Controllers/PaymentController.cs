using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payment.Api.Infrastructure;
using Payment.Api.Models;

namespace Payment.Api.Controllers
{
    /// <summary>
    /// 订单模块
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentsContext _context;

        public PaymentController(PaymentsContext context)
        {
            _context = context;
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        /// <summary>
        /// 新增支付信息
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PaymentInfo> CreatePayment(PaymentInfo payment)
        {
            _context.PaymentInfos.Add(payment);
            payment.PaymentId = Guid.NewGuid().ToString("N");
            payment.PaymentDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return payment;
        }
    }
}
