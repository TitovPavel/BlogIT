
using BlogIT.DB.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
        IQueryable<ChatMessage> GetChatMessagesByPartyId(int partyId);
        IQueryable<News> GetLastNews(int count);
        IQueryable<News> GetTopNews(int count);
    }
}
