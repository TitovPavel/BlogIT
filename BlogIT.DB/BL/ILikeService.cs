using BlogIT.DB.Models;

namespace BlogIT.DB.BL
{
    public interface ILikeService
    {
        void SendLikeUp(User user, int chatMessageId, bool turnOn);
    }
}
