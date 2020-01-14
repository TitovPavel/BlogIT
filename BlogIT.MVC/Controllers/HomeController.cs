using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlogIT.DB.BL;
using BlogIT.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
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
    }
}