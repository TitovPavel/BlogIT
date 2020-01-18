using System;
using System.Collections.Generic;
using System.Text;

namespace BlogIT.DB.Models
{
    public class Like
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public ChatMessage ChatMessage { get; set; }
        public int ChatMessageId { get; set; }
        public bool LikeUp { get; set; }
        public bool LikeDown { get; set; }
        public int LikeCount { get; set; }

    }
}
