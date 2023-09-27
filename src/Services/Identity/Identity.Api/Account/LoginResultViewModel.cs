namespace Identity.Api.Account
{
    public class LoginResultViewModel
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string IdToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
        public string[] Roles { get; set; }
    }
}
