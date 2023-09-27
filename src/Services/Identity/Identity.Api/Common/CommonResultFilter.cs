using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Common
{
    public class CommonResultFilter : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ObjectResult objectResult)
            {
                if (objectResult.Value is BaseResponse baseResponse)
                {
                    objectResult.Value = new
                    {
                        Code = baseResponse.Success ? 0 : 1,
                        Data = baseResponse.Success ? baseResponse.Data : null,
                        Message = baseResponse.ErrorMessage ?? "请求成功"
                    };
                }
                else
                {
                    objectResult.Value = new
                    {
                        Code = 0,
                        Data = objectResult.Value,
                        Message = "请求成功"
                    };
                }
            }
        }
    }
}
