using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlogIT.DB.BL;
using BlogIT.DB.Models;
using BlogIT.DB.Specifications;
using BlogIT.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static BlogIT.DB.Specifications.NewsFilterPaginatedSpecification;

namespace BlogIT.MVC.Controllers
{
    //[Authorize(Roles = "admin")]
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

        public async Task<IActionResult> Index(string searchString, int page = 1,
            SortState sortOrder = SortState.DateTimeDesc)
        {

            int pageSize = 3;
       
          
            var filterSpecification = new NewsFilterSpecification(searchString, null, DateTime.MinValue, 0, false);

            var filterPaginatedSpecification =
                new NewsFilterPaginatedSpecification((page - 1) * pageSize, pageSize, searchString, null, DateTime.MinValue, 0, false);
            filterPaginatedSpecification.ApplyOrder(sortOrder);

            var itemsOnPage = await _newsService.ListNewsAsync(filterPaginatedSpecification);
            var totalItems = await _newsService.CountNewsAsync(filterSpecification);

            var items = _mapper.Map<IReadOnlyList<News>, List<ItemNewsListViewModel>>(itemsOnPage);



            PageViewModel pageViewModel = new PageViewModel(totalItems, page, pageSize);

            NewsListViewModel newsListViewModel = new NewsListViewModel();
            newsListViewModel.ItemNewsListViewModel = items;
            newsListViewModel.PageViewModel = pageViewModel;
            newsListViewModel.SortNewsViewModel = new SortNewsViewModel(sortOrder);
            newsListViewModel.FilterNewsViewModel = new FilterNewsViewModel(searchString);
                

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
            NewsViewModel newsViewModel = _mapper.Map<NewsViewModel>(news);
            newsViewModel.CurrentUserRating = _newsService.GetCurrentUserRating(news.Id, _userManager.GetUserId(User));

            return View(newsViewModel);

        }

        [HttpPost]
        public ActionResult Delete(int id)
        {

            _newsService.DeleteNewsById(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult PostRating(RatingViewModel ratingViewModel)
        {

            string userId = _userManager.GetUserId(User);

            if(userId!=null)
            {
                Rating rating = _mapper.Map<Rating>(ratingViewModel);
                rating.UserId = userId;

                _newsService.SetRating(rating);

                return Json("You rated this " + ratingViewModel.Rate.ToString() + " star(s)");
            }
            else
            {
                return Json("Need to register");
            }
        }
    }
}