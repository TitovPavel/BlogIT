
using BlogIT.DB.Models;
using System.Collections;
using System.Linq;

namespace BlogIT.DB.BL
{
    public interface INewsService
    {
        IQueryable<News> ListAll();
        IEnumerable GetCategories();
        int AddNews(News news);
    }
}
