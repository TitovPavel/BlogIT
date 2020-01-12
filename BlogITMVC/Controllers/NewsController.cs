using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlogIT.DB.BL;
using BlogIT.DB.Models;
using BlogIT.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogIT.MVC.Controllers
{
    [Authorize(Roles = "admin")]
    [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
    public class NewsController : Controller
    {

        private readonly IMapper _mapper;
        private readonly INewsService _newsService;

        public NewsController(IMapper mapper, INewsService newsService)
        {
            _mapper = mapper;
            _newsService = newsService;
        }

        public IActionResult Index(int page = 1)
        {

            int pageSize = 3;

            IQueryable<News> source = _newsService.ListAll();
            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize).ProjectTo<ItemNewsListViewModel>(_mapper.ConfigurationProvider).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);

            NewsListViewModel newsListViewModel = new NewsListViewModel();
            newsListViewModel.ItemNewsListViewModel = items;
            newsListViewModel.PageViewModel = pageViewModel;

            return View(newsListViewModel);

        }

        public IActionResult Create()
        {
            CreateNewsViewModel createNewsViewModel = new CreateNewsViewModel();
            createNewsViewModel.Categories = new SelectList(_newsService.GetCategories(), "Id", "Title");
            return View(createNewsViewModel);
        }

        [HttpPost]
        public IActionResult Create(CreateNewsViewModel createNewsViewModel)
        {
            if (ModelState.IsValid)
            {

                News news = _mapper.Map<News>(createNewsViewModel);

                int newsId =_newsService.AddNews(news);

                return RedirectToAction("Index");
            }

            createNewsViewModel.Categories = new SelectList(_newsService.GetCategories(), "Id", "Title");
            return View(createNewsViewModel);
        }
    }
}