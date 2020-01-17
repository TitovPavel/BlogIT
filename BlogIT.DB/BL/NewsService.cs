using BlogIT.DB.DAL;
using BlogIT.DB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
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

            UpdateNewsTags(news);

            _context.News.Add(news);
            _context.SaveChanges();
            return news.Id;
        }

        public void DeleteNewsById(int id)
        {
            News news = GetNewsById(id);
            if (news != null)
            {
                _context.News.Remove(news);
                _context.SaveChanges();
            }
        }

        public List<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public News GetNewsById(int id)
        {
            return _context.News
                .Include(p => p.Category)
                .Include(p => p.NewsTag)
                .ThenInclude(p => p.Tag)
                .SingleOrDefault(p => p.Id == id);
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
            return _context.ChatMessages
                .Where(c => c.NewsId == newsId)
                .Include(i => i.User)
                .ThenInclude(i => i.Avatar)
                .Include(i => i.Like);
        }

        public IQueryable<News> GetLastNews(int count)
        {
            return _context.News
                .Where(x => x.DateTime <= DateTime.Now && !x.Deleted)
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

        public IQueryable<News> ListActualNews(bool includeChatMessage = false)
        {
            IQueryable<News> news = ListAll().Where(p => p.DateTime <= DateTime.Now);
            if (includeChatMessage)
            {
                news = news.Include(i => i.ChatMessages);
            }
            return news;
        }

        public void UpdateNews(News news)
        {
            News updateNews = _context.News.Find(news.Id);

            if (updateNews != null)
            {
                updateNews.Title = news.Title;
                updateNews.DateTime = news.DateTime;
                updateNews.Description = news.Description;
                updateNews.NewsText = news.NewsText;
                updateNews.CategoryId = news.CategoryId;

                if(updateNews.Tags != news.Tags)
                {
                    updateNews.Tags = news.Tags;
                    UpdateNewsTags(updateNews);
                }

                _context.News.Update(updateNews);
            }
            _context.SaveChanges();

        }

        private void UpdateNewsTags(News news)
        {
            string[] tags = news.Tags.Split(',');
            news.NewsTag = new List<NewsTag>();

            foreach (string tagTitle in tags)
            {
                Tag tag = _context.Tags.FirstOrDefault(p => p.Title.ToUpper() == tagTitle.ToUpper());
                if (tag == null)
                {
                    tag = new Tag()
                    {
                        Title = tagTitle
                    };
                }

                news.NewsTag.Add(
                    new NewsTag()
                    {
                        News = news,
                        Tag = tag
                    }
                );
            };
        }
    }
}
