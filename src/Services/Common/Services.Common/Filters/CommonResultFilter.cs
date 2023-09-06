using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Common.Filters
{
    public class CommonResultFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult objectResult)
            {
                // 查到的值为 null，返回错误信息
                if (objectResult.Value == null)
                {
                    objectResult.Value = new
                    {
                        Code = 1,
                        Data = "",
                        Message = "未找到数据"
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
            base.OnActionExecuted(context);
        }
    }
}
