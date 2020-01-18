using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BlogIT.DB.Models;
using Newtonsoft.Json;
using BlogIT.DB.BL;

namespace BlogIT.MVC.Controllers
{
    public class SearchController : Controller
    {
        private readonly INewsService _newsService;

        public SearchController(INewsService newsService)
        {
            _newsService = newsService;
        }
        public IActionResult GetTags(string term)
        {
            IQueryable<Tag> tags = _newsService.GetTags(term);

            var respons = tags.Select(a => new { value = a.Title, label = a.Title }).ToArray();

            return Content(JsonConvert.SerializeObject(respons));
        }
    }
}