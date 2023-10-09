using Identity.Api.Account;
using Identity.Api.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    /// <summary>
    /// 用户处理
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly LoginService _loginService;

        public AccountController(LoginService loginService)
        {
            _loginService = loginService;
        }


        private IActionResult LoginResponse(BaseResponse result)
        {
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginInputDTO model)
        {
            var userInfo = await _loginService.Login(model);
            return LoginResponse(userInfo);
        }


        /// <summary>
        /// 新增测试用户
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateTestUser(CreateTestUserDTO dto)
        {
            var result = await _loginService.CreateTestUser(dto);
            if (result)
            {
                return Ok("新增用户成功");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "新增用户失败");
            }
        }
    }
}
