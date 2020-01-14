using BlogIT.DB.DAL;
using BlogIT.DB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Linq;

namespace BlogIT.DB.BL
{
    public class NewsService : INewsService
    {
        private readonly BlogITContext _context;

        public NewsService(BlogITContext context)
        {
            _context = context;
        }

        public int AddNews(News news)
        {
            _context.News.Add(news);
            _context.SaveChanges();
            return news.Id;
        }

        public void DeleteNewsById(int id)
        {
            News news = GetNewsById(id);
            if (news != null) {
                _context.News.Remove(news);
                _context.SaveChanges();
            }
        }

        public IEnumerable GetCategories()
        {
            return _context.Categories;
        }

        public News GetNewsById(int id)
        {
            return _context.News.Include(p => p.Category).SingleOrDefault(p => p.Id == id);
        }

        public IQueryable<News> ListAll()
        {
            return _context.News.Where(x => !x.Deleted);
        }

        public void AddMessageChat(ChatMessage chatMessage)
        {
            _context.ChatMessages.Add(chatMessage);
            _context.SaveChanges();
        }

        public IQueryable<ChatMessage> GetChatMessagesByPartyId(int newsId)
        {
            return _context.ChatMessages.Where(c => c.NewsId == newsId).Include(i => i.User).ThenInclude(i => i.Avatar);
        }

        public IQueryable<News> GetLastNews(int count)
        {
            return _context.News
                .Where(x => x.DateTime<=DateTime.Now && !x.Deleted)
                .OrderByDescending(s => s.DateTime)
                .Take(count)
                .Include(i => i.Category)
                .Include(i => i.ChatMessages);
        }

        public IQueryable<News> GetTopNews(int count)
        {
            return _context.News
                .Take(count)
                .Include(i => i.Category);
        }
    }
}
