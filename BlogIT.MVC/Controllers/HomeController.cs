using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlogIT.DB.BL;
using BlogIT.DB.Models;
using BlogIT.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogIT.MVC.Controllers
{
    public class HomeController : Controller
    {

        private readonly IMapper _mapper;
        private readonly INewsService _newsService;

        public HomeController(IMapper mapper,
            INewsService newsService)
        {
            _mapper = mapper;
            _newsService = newsService;
        }

        public IActionResult Index()
        {

            HomePageViewModel homePageViewModel = new HomePageViewModel()
            {
                LastNews = _newsService.GetLastNews(3).ProjectTo<NewsAnnotationViewModel>(_mapper.ConfigurationProvider).ToList(),
                TopNews = _newsService.GetTopNews(3).ProjectTo<NewsAnnotationViewModel>(_mapper.ConfigurationProvider).ToList()
            };

            return View(homePageViewModel);
        }

        public IActionResult List(string searchString, string tags, DateTime dateCalendar, int categoryId = 0, bool findByComments = false, int page = 1)
        {

            int pageSize = 3;

            IQueryable<News> source = _newsService.ListActualNews(findByComments);


            if(dateCalendar> DateTime.MinValue)
            {
                source = source.Where(p => p.DateTime.Date == dateCalendar.Date);
            }
            if (categoryId != 0)
            {
                source = source.Where(p => p.CategoryId == categoryId);
            }
            if (!String.IsNullOrEmpty(tags))
            {
                string[] tagsArray = tags.Split(',');

                foreach (string tagTitle in tagsArray)
                {
                    source = source.Where(p => p.NewsTag.Any(s => s.Tag.Title.ToUpper() == tagTitle.ToUpper()));
                }
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                source = source.Where(p => p.Title.Contains(searchString) || p.Description.Contains(searchString) || p.NewsText.Contains(searchString) || (findByComments && p.ChatMessages.Any(p => p.Message.Contains(searchString))));
            }

            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize).ProjectTo<NewsAnnotationViewModel>(_mapper.ConfigurationProvider).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);

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