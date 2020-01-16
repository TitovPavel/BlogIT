using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace BlogIT.DB.Models
{
    public class User : IdentityUser
    {
        public DateTime Birthday { get; set; }
        public string Sex { get; set; }
        public List<News> News { get; set; }
        public List<Like> Like { get; set; }
        public FileModel Avatar { get; set; }
        public int? AvatarId { get; set; }
    }
}
