using System.ComponentModel.DataAnnotations;

namespace Identity.Api.Account
{
    public class CreateTestUserDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
    }
}
