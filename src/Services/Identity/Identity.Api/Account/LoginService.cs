using Identity.Api.Common;
using Identity.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Api.Account
{
    public class LoginService
    {
        private readonly ILogger<LoginService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IdentityAuthContext _context;

        public LoginService(ILogger<LoginService> logger, IConfiguration configuration, IdentityAuthContext context)
        {
            _logger = logger;
            _configuration = configuration;
            _context = context;
        }

        public async Task<BaseResponse> Login(LoginInputDTO model)
        {
            var flag = ValidateUser(model);
            if (!flag)
            {
                return new BaseResponse { Success = false, ErrorMessage = "用户名或密码错误" };
            }
            var token = await GenerateToken(model.Username);
            var userResult = new LoginResultViewModel
            {
                UserName = model.Username,
                UserId = "",
                AccessToken = token,
                RefreshToken = "",
                IdToken = "",
                AccessTokenExpiration = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Jwt:ExpireDays"])),
                RefreshTokenExpiration = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Jwt:ExpireDays"])),
                Roles = new string[] { }
            };
            return new BaseResponse { Success = true, Data = userResult };
        }

        /// <summary>
        /// 根据用户名生成token
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        private async Task<string> GenerateToken(string username)
        {
            var user = await _context.UsersInfos.FirstOrDefaultAsync(u => u.UserName == username);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                //new Claim(ClaimTypes.Role, user.Role)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Jwt:ExpireDays"]));
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: expires,
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// 验证用户名和密码是否正确
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public bool ValidateUser(LoginInputDTO model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            var user = _context.UsersInfos.FirstOrDefault(u => u.UserName == model.Username && u.Password == model.Password);
            return user != null;
        }
    }
}
