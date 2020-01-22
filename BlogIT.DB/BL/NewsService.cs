using BlogIT.DB.DAL;
using BlogIT.DB.Interfaces;
using BlogIT.DB.Models;
using BlogIT.DB.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public List<ChatMessage> GetChatMessagesByPartyId(int newsId)
        {
            return _context.ChatMessages
                .Where(c => c.NewsId == newsId)
                .Include(i => i.User)
                .ThenInclude(i => i.Avatar)
                .Include(i => i.Like)
                .ToList();
        }

        public List<News> GetLastNews(int count)
        {
            return _context.News
                .Where(x => x.DateTime <= DateTime.Now && !x.Deleted)
                .OrderByDescending(s => s.DateTime)
                .Take(count)
                .Include(i => i.Category)
                .Include(i => i.ChatMessages)
                .ToList();
        }

        public List<News> GetTopNews(int count)
        {
            return _context.News
                .OrderByDescending(p => p.RateAverage)
                .ThenByDescending(p => p.RateCount)
                .ThenByDescending(p => p.DateTime)
                .Take(count)
                .Include(i => i.Category)
                .ToList();
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
            News updateNews = _context.News.Include(p => p.NewsTag).FirstOrDefault(p => p.Id == news.Id);

            if (updateNews != null)
            {
                updateNews.Title = news.Title;
                updateNews.DateTime = news.DateTime;
                updateNews.Description = news.Description;
                updateNews.NewsText = news.NewsText;
                updateNews.CategoryId = news.CategoryId;

                if (updateNews.Tags != news.Tags)
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

        public List<Tag> GetTags(string tag)
        {
            return _context.Tags
                .Where(p => p.Title.Contains(tag))
                .Take(10)
                .ToList();
        }

        public void SetRating(Rating rating)
        {
            Rating updateRating = _context.Ratings.FirstOrDefault(p => p.NewsId == rating.NewsId && p.UserId == rating.UserId);

            if (updateRating != null)
            {
                updateRating.Rate = rating.Rate;
                _context.Ratings.Update(updateRating);
            }
            else
            {
                _context.Ratings.Add(rating);
            }
            _context.SaveChanges();

            UpdateNewsRating(rating.NewsId);

        }

        private void UpdateNewsRating(int newsId)
        {

            News news = _context.News.Include(p => p.Ratings).SingleOrDefault(p => p.Id == newsId);

            if (news != null)
            {
                news.RateCount = news.Ratings.Count;
                news.RateAverage = news.RateCount == 0 ? 0 : news.Ratings.Sum(m => m.Rate)/ news.RateCount;
                _context.News.Update(news);
                _context.SaveChanges();

            }
        }

        public int GetCurrentUserRating(int newsId, string userId)
        {
            if(!String.IsNullOrEmpty(userId))
            {
                Rating rating = _context.Ratings.FirstOrDefault(p => p.NewsId == newsId && p.UserId == userId);

                if (rating != null)
                {
                    return rating.Rate;
                }
            }

            return 0;
        }

        public List<string> GetTopTags()
        {
            return _context.Tags.Where(p => p.NewsTag.Count > 0).OrderByDescending(p => p.NewsTag.Count).Take(10).Select(p => p.Title).ToList();
        }

        public async Task<IReadOnlyList<News>> ListNewsAsync(ISpecification<News> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<int> CountNewsAsync(ISpecification<News> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        private IQueryable<News> ApplySpecification(ISpecification<News> spec)
        {
            return SpecificationEvaluator<News>.GetQuery(_context.News.AsQueryable(), spec);
        }
    }
}
