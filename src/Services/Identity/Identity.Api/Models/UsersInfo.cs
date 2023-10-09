using System;
using System.Collections.Generic;

namespace Identity.Api.Models
{
    public partial class UsersInfo
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
