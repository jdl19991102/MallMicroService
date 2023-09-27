using Identity.Api.Account;
using Identity.Api.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
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

        public async Task<IActionResult> Login(LoginInputDTO model)
        {
            var userInfo = await _loginService.Login(model);
            return LoginResponse(userInfo);
        }
    }
}
