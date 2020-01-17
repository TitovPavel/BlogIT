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

        public int GetLikeCount(User user, int chatMessageId)
        {
            Like like = _context.Likes.FirstOrDefault(p => p.User == user && p.ChatMessageId == chatMessageId);
            return like == null ? 0 : like.LikeCount;
        }

        public void SendLike(User user, int chatMessageId, bool turnOn, bool up)
        {
            Like likeUp = _context.Likes.FirstOrDefault(p => p.User == user && p.ChatMessageId == chatMessageId);
            if (likeUp == null)
            {
                likeUp = new Like()
                {
                    User = user,
                    ChatMessageId = chatMessageId,
                    LikeUp = turnOn && up ? true : false,
                    LikeDown = turnOn && !up ? true : false,
                    LikeCount = turnOn ? (up ? 1 : -1) : 0
                };
                _context.Likes.Add(likeUp);
                _context.SaveChanges();
            }
            else
            {
                if(up && turnOn != likeUp.LikeUp || !up && turnOn != likeUp.LikeDown)
                {
                    likeUp.LikeUp = turnOn && up ? true : false;
                    likeUp.LikeDown = turnOn && !up ? true : false;
                    likeUp.LikeCount = turnOn ? (up ? 1 : -1) : 0;
                    _context.Likes.Update(likeUp);
                    _context.SaveChanges();
                }
            }
        }
    }
}
