using BlogIT.DB.DAL;
using BlogIT.DB.Models;
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

        public IQueryable<News> ListAll()
        {
            return _context.News;
        }
    }
}
