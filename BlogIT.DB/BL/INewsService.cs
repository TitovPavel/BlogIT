using BlogIT.DB.Interfaces;
using BlogIT.DB.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogIT.DB.BL
{
    public interface INewsService
    {
        IQueryable<News> ListAll();
        List<Category> GetCategories();
        int AddNews(News news);
        News GetNewsById(int id);
        void DeleteNewsById(int id);
        void AddMessageChat(ChatMessage chatMessage);
        List<ChatMessage> GetChatMessagesByPartyId(int partyId);
        List<News> GetLastNews(int count);
        List<Tag> GetTags(string tag);
        List<News> GetTopNews(int count);
        IQueryable<News> ListActualNews(bool includeChatMessage = false);
        void UpdateNews(News news);
        void SetRating(Rating rating);
        int GetCurrentUserRating(int newsId, string userId);
        List<string> GetTopTags();
        Task<IReadOnlyList<News>> ListNewsAsync(ISpecification<News> spec);
        Task<int> CountNewsAsync(ISpecification<News> spec);
    }
}
