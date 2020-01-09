
using BlogIT.DB.Models;
using System.Linq;

namespace BlogIT.DB.BL
{
    public interface INewsService
    {
        IQueryable<News> ListAll();
    }
}
