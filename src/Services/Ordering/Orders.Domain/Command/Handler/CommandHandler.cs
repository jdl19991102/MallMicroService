using FluentValidation.Results;
using Orders.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Domain.Command.Handler
{
    /// <summary>
    /// 命令处理程序的基类，提供一些公共的方法
    /// </summary>
    public abstract class CommandHandler
    {
        // 参数验证错误的时候，记录相关错误信息
        protected List<string> ErrorMessages { get; private set; }
        protected CommandHandler()
        {
            ErrorMessages = new List<string>();
        }
        /// <summary>
        /// 添加错误信息
        /// </summary>
        /// <param name="message"></param>
        protected void AddErrorMessage(string message)
        {
            ErrorMessages.Add(message);
        }
        /// <summary>
        /// 获取所有错误信息
        /// </summary>
        /// <returns></returns>
        public string GetErrorMessages()
        {
            return string.Join(";", ErrorMessages);
        }

        /// <summary>
        /// 处理参数验证错误信息
        /// </summary>
        /// <param name="result"></param>
        /// <exception cref="MyException"></exception>
        protected static void HandleErrorMessages(ValidationResult result)
        {
            string prefix = "参数校验错误：";
            var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
            var errorStr = string.Join(";", errors);
            var message = prefix + errorStr;
            throw new OrderingDomainException(3, message);
        }
    }
}
