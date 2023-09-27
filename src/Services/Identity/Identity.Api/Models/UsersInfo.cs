using System;
using System.Collections.Generic;

namespace Identity.Api.Models
{
    public partial class UsersInfo
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? UserId { get; set; }
        public string? Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
