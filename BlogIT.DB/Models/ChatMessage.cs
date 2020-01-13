using System;
using System.Collections.Generic;
using System.Text;

namespace BlogIT.DB.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public News News { get; set; }
        public int NewsId { get; set; }
    }
}
