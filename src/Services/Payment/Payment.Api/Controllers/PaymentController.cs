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
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(PaymentsContext context, ILogger<PaymentController> logger)
        {
            _context = context;
            _logger = logger;
        }


        /// <summary>
        /// 新增支付信息
        /// </summary>
        /// <param name="OrderId">订单Id</param>
        /// <returns>新增的订单数据</returns>
        [HttpPost]
        public async Task<PaymentInfo> CreatePayment(int OrderId)
        {
            var payment = new PaymentInfo();
            payment.OrderId = OrderId;
            payment.PaymentId = Guid.NewGuid().ToString("N");
            payment.PaymentDate = DateTime.Now;
            _context.PaymentInfos.Add(payment);
            await _context.SaveChangesAsync();
            return payment;
        }

        /// <summary>
        /// 更新支付状态
        /// </summary>
        /// <param name="paymentId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> UpdatePaymentStatus(string paymentId, int status)
        {
            var payment = await _context.PaymentInfos.FirstOrDefaultAsync(x => x.PaymentId == paymentId);
            if (payment == null)
            {
                _logger.LogInformation("没有找到对应的支付信息");
                return false;
            }
            payment.PaymentStatus = status;
            _context.PaymentInfos.Update(payment);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
