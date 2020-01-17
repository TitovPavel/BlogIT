using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlogIT.DB.BL;
using BlogIT.DB.Models;
using BlogIT.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;

        public NewsController(IMapper mapper, 
            INewsService newsService,
            UserManager<User> userManager)
        {
            _mapper = mapper;
            _newsService = newsService;
            _userManager = userManager;
        }

        public IActionResult Index(int page = 1,
            SortState sortOrder = SortState.DateTimeDesc)
        {

            int pageSize = 3;

            IQueryable<News> source = _newsService.ListAll();

            // сортировка
            switch (sortOrder)
            {
                case SortState.TitleDesc:
                    source = source.OrderByDescending(s => s.Title);
                    break;
                case SortState.DateTimeAsc:
                    source = source.OrderBy(s => s.DateTime);
                    break;
                case SortState.DateTimeDesc:
                    source = source.OrderByDescending(s => s.DateTime);
                    break;
                case SortState.WriterAsc:
                    source = source.OrderBy(s => s.Writer.UserName);
                    break;
                case SortState.WriterDesc:
                    source = source.OrderByDescending(s => s.Writer.UserName);
                    break;
                default:
                    source = source.OrderBy(s => s.Title);
                    break;
            }

            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize).ProjectTo<ItemNewsListViewModel>(_mapper.ConfigurationProvider).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);

            NewsListViewModel newsListViewModel = new NewsListViewModel();
            newsListViewModel.ItemNewsListViewModel = items;
            newsListViewModel.PageViewModel = pageViewModel;
            newsListViewModel.SortNewsViewModel = new SortNewsViewModel(sortOrder);
                

            return View("List", newsListViewModel);

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
                news.WriterId = _userManager.GetUserId(User);
                int newsId =_newsService.AddNews(news);

                return RedirectToAction("Index", new { id = newsId });
            }

            createNewsViewModel.Categories = new SelectList(_newsService.GetCategories(), "Id", "Title");
            return View(createNewsViewModel);
        }

        public IActionResult Edit(int id)
        {
            News news = _newsService.GetNewsById(id);
            if (news == null)
            {
                return NotFound();
            }
            EditNewsViewModel editNewsViewModel = _mapper.Map<EditNewsViewModel>(news);
            editNewsViewModel.Categories = new SelectList(_newsService.GetCategories(), "Id", "Title");

            return View(editNewsViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditNewsViewModel editNewsViewModel)
        {
            if (ModelState.IsValid)
            {

                News news = _mapper.Map<News>(editNewsViewModel);
                _newsService.UpdateNews(news);

                return RedirectToAction("Index", new { id = news.Id });
            }

            editNewsViewModel.Categories = new SelectList(_newsService.GetCategories(), "Id", "Title");
            return View(editNewsViewModel);
        }

        [Route("[controller]/[action]/{id:int}")]
        public IActionResult Index(int id)
        {

            News news = _newsService.GetNewsById(id);
            NewsViewModel itemNewsListViewModel = _mapper.Map<NewsViewModel>(news);
          
            return View(itemNewsListViewModel);

        }

        [HttpPost]
        public ActionResult Delete(int id)
        {

            _newsService.DeleteNewsById(id);

            return RedirectToAction("Index");
        }
    }
}