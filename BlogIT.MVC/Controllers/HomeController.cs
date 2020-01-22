using AutoMapper;
using AutoMapper.QueryableExtensions;
using AutoMapper.Collection;
using BlogIT.DB.BL;
using BlogIT.DB.Models;
using BlogIT.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using BlogIT.DB.Specifications;
using System.Threading.Tasks;

namespace BlogIT.MVC.Controllers
{
    public class HomeController : Controller
    {

        private readonly IMapper _mapper;
        private readonly INewsService _newsService;
        private readonly IConfiguration _configuration;

        public HomeController(IMapper mapper,
            INewsService newsService,
            IConfiguration configuration)
        {
            _mapper = mapper;
            _newsService = newsService;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            Int32.TryParse(_configuration["CountLastNews"], out int countLastNews);
            Int32.TryParse(_configuration["CountTopNews"], out int countTopNews);

            HomePageViewModel homePageViewModel = new HomePageViewModel()
            {
                LastNews = _mapper.Map<List<News>, List<NewsAnnotationViewModel>>(_newsService.GetLastNews(countLastNews)),
                TopNews = _mapper.Map<List<News>, List<NewsAnnotationViewModel>>(_newsService.GetTopNews(countTopNews)),
                ListTags = _newsService.GetTopTags()
            };

            return View(homePageViewModel);
        }

        public async Task<IActionResult> List(string searchString, string tags, DateTime dateCalendar, int categoryId = 0, bool findByComments = false, int page = 1)
        {

            int pageSize = 3;

            List<string> tagsList = new List<string>();

            if (!String.IsNullOrEmpty(tags))
            {
                tagsList = tags.ToUpper().Split(',').ToList();

            }

            var filterSpecification = new NewsFilterSpecification(searchString, tagsList, dateCalendar, categoryId, findByComments);
            var filterPaginatedSpecification =
                new NewsFilterPaginatedSpecification((page - 1) * pageSize, pageSize, searchString, tagsList, dateCalendar, categoryId, findByComments);

            var itemsOnPage = await _newsService.ListNewsAsync(filterPaginatedSpecification);
            var totalItems = await _newsService.CountNewsAsync(filterSpecification);

            var items = _mapper.Map<IReadOnlyList<News>, List<NewsAnnotationViewModel>>(itemsOnPage);



            PageViewModel pageViewModel = new PageViewModel(totalItems, page, pageSize);

            List<Category> listCategories = _newsService.GetCategories();

            FilterNewsViewModel filterNewsViewModel = new FilterNewsViewModel(listCategories, searchString, tags, dateCalendar, categoryId, findByComments);


            HomeNewsListViewModel homeNewsListViewModel = new HomeNewsListViewModel
            {
                NewsAnnotation = items,
                PageViewModel = pageViewModel,
                FilterNewsViewModel = filterNewsViewModel
            };

            return View(homeNewsListViewModel);
        }
    }
}