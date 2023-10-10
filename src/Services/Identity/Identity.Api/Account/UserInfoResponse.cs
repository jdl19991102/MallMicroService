namespace Identity.Api.Account
{
    /// <summary>
    /// 根据token返回的用户信息
    /// </summary>
    public class UserInfoResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string MobilePhone { get; set; }       
        public string Role { get; set; }
    }
}
