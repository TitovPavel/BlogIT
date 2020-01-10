using BlogIT.DB.DAL;
using BlogIT.DB.Models;
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

        public IEnumerable GetCategories()
        {
            return _context.Categories;
        }

        public IQueryable<News> ListAll()
        {
            return _context.News;
        }
    }
}
