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
            var user = await _context.UsersInfos.FirstOrDefaultAsync(u => u.UserName == model.Username && u.Password == model.Password);
            if (user == null)
            {
                return new BaseResponse { Success = false, ErrorMessage = "用户名或密码错误" };
            }
            var accessToken = GenerateToken(user);
            var idToken = GenerateIdToken(user);
            var userResult = new LoginResultViewModel
            {
                UserName = model.Username,
                UserId = user.UserId,
                AccessToken = accessToken,
                RefreshToken = "",
                IdToken = idToken,
                AccessTokenExpiration = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Jwt:AccessExpiration"])),
                RefreshTokenExpiration = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Jwt:RefreshExpiration"])),
                Roles = new string[] { }
            };
            return new BaseResponse { Success = true, Data = userResult };
        }

        /// <summary>
        /// 新增测试用户
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        internal async Task<bool> CreateTestUser(CreateTestUserDTO dto)
        {
            // 新增用户，请完善相关的逻辑
            var user = new UsersInfo();
            user.UserName = dto.UserName;
            user.UserId = Guid.NewGuid().ToString("N");
            user.Password = AesEncryption.Encrypt(dto.Password);
            user.Phone = dto.Phone;
            user.CreatedDate = DateTime.Now;
            user.UpdatedDate = DateTime.Now;
            _context.UsersInfos.Add(user);
            var flag = await _context.SaveChangesAsync();
            return flag > 0;
        }



        /// <summary>
        /// 根据用户信息生成access_token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string GenerateToken(UsersInfo user)
        {
            //初始化payload
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };
            //生成对称秘钥
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            //生成签名证书
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToInt32(_configuration["Jwt:AccessExpiration"]));
            var accessToken = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"], //设置签发者
                audience: _configuration["Jwt:Audience"], //设置接收者
                claims: claims, //设置payload信息
                expires: expires, //设置过期时间
                signingCredentials: creds); //安全签名
            return new JwtSecurityTokenHandler().WriteToken(accessToken);
        }

        /// <summary>
        /// 根据用户信息生成id_token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string GenerateIdToken(UsersInfo user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("sub", user.Id.ToString(),ClaimValueTypes.Integer32),
                new Claim(ClaimTypes.NameIdentifier, user.UserId),
                new Claim(ClaimTypes.MobilePhone,user.Phone),
                new Claim(ClaimTypes.Role,"admin")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToInt32(_configuration["Jwt:IdExpiration"]));
            var idToken = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"], //设置签发者
                audience: _configuration["Jwt:Audience"], //设置接收者
                claims: claims, //设置payload信息
                expires: expires, //设置过期时间
                signingCredentials: creds); //安全签名
            return new JwtSecurityTokenHandler().WriteToken(idToken);
        }

        /// <summary>
        /// 根据id_token获取用户信息
        /// </summary>
        /// <param name="idToken"></param>
        /// <returns></returns>
        internal UserInfoResponse GetUserInfoByIdToken(string idToken)
        {
            // 解析id_token
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(idToken);
            var claims = token.Claims;
            var id = int.Parse(claims.FirstOrDefault(c => c.Type == "sub")!.Value);
            var userName = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var userId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var phone = claims.FirstOrDefault(c => c.Type == ClaimTypes.MobilePhone)?.Value;
            var role = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            var userInfo = new UserInfoResponse
            {
                Id = id,
                UserName = userName,
                UserId = userId,
                MobilePhone = phone,
                Role = role
            };
            return userInfo;
        }
    }
}
