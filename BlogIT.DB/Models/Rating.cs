using System;
using System.Collections.Generic;
using System.Text;

namespace BlogIT.DB.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int NewsId { get; set; }
        public News News { get; set; }
    }
}
