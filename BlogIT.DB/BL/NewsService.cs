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
            return _context.News.Find(id);
        }

        public IQueryable<News> ListAll()
        {
            return _context.News.Where(x => !x.Deleted);
        }
    }
}
