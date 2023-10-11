using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Identity.Api.Common
{
    public class GlobalExceptionFilterAsync : IAsyncExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilterAsync> _logger;

        public int Code { get; private set; }
        public string? Message { get; private set; }

        public GlobalExceptionFilterAsync(ILogger<GlobalExceptionFilterAsync> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 重写OnExceptionAsync方法
        /// </summary>
        /// <param name="context">异常信息</param>
        /// <returns></returns>
        public Task OnExceptionAsync(ExceptionContext context)
        {
            // 如果异常没有被处理，则进行处理
            if (context.ExceptionHandled == false)
            {
                // 记录错误信息
                _logger.LogError(context.Exception, context.Exception.Message);
                Code = 1;
                Message = context.Exception.Message;

                context.Result = new ContentResult
                {
                    StatusCode = 500,
                    ContentType = "application/json;charset=utf-8",
                    Content = JsonConvert.SerializeObject(new
                    {
                        Code,
                        Message,
                        Data = ""
                    })
                };

                // 设置为true，表示异常已经被处理了，其它捕获异常的地方就不会再处理了
                context.ExceptionHandled = true;

            }
            return Task.CompletedTask;
        }

    }
}
