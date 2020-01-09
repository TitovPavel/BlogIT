using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlogIT.DB.BL;
using BlogIT.DB.Models;
using BlogIT.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogIT.MVC.Controllers
{
    [Authorize(Roles = "admin")]
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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel createUserViewModel)
        {
            if (ModelState.IsValid)
            {

                return RedirectToAction("Index");
            }

            return View(createUserViewModel);
        }
    }
}