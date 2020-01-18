using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogIT.MVC.ViewModels
{
    public class ChatMessageViewModel
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Message { get; set; }
        public string UserName { get; set; }
        public string AvatarPath { get; set; }
        public int LikeCount { get; set; }
        public int LikeUpCount { get; set; }
        public int LikeDownCount { get; set; }
    }
}
