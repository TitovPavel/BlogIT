using BlogIT.DB.Models;

namespace BlogIT.DB.BL
{
    public interface ILikeService
    {
        void SendLike(User user, int chatMessageId, bool turnOn, bool up);
        int GetLikeCount(User user, int chatMessageId);
    }
}
