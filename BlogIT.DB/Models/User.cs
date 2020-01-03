using Microsoft.AspNetCore.Identity;
using System;


namespace BlogIT.DB.Models
{
    public class User : IdentityUser
    {
        public DateTime Birthday { get; set; }
        public string Sex { get; set; }
        public FileModel Avatar { get; set; }
        public int? AvatarId { get; set; }
    }
}
