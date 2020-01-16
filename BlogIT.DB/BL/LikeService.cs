using BlogIT.DB.DAL;
using BlogIT.DB.Models;
using System;
using System.Linq;

namespace BlogIT.DB.BL
{
    public class LikeService : ILikeService
    {
        private readonly BlogITContext _context;

        public LikeService(BlogITContext context)
        {
            _context = context;
        }

        public void SendLikeUp(User user, int chatMessageId, bool turnOn)
        {
            Like likeUp = _context.Likes.FirstOrDefault(p => p.User == user && p.ChatMessageId == chatMessageId);
            if (likeUp == null)
            {
                likeUp = new Like()
                {
                    User = user,
                    ChatMessageId = chatMessageId,
                    LikeUp = turnOn ? true : false,
                    LikeDown = false,
                    LikeCount = turnOn ? 1 : 0
                };
                _context.Likes.Add(likeUp);
                _context.SaveChanges();
            }
            else
            {
                if(turnOn != likeUp.LikeUp)
                {
                    likeUp.LikeUp = turnOn ? true : false;
                    likeUp.LikeCount = turnOn ? 1 : 0;
                    likeUp.LikeDown = false;
                    _context.Likes.Update(likeUp);
                    _context.SaveChanges();
                }
            }
        }
    }
}
