using AutoMapper;
using BlogIT.DB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BlogIT.MVC.ViewModels;
using System;
using System.Threading.Tasks;
using BlogIT.DB.BL;
using System.Linq;
using AutoMapper.QueryableExtensions;

namespace BlogIT.MVC.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public UsersController(UserManager<User> userManager, IMapper mapper, IPhotoService photoService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _photoService = photoService;
        }

        public IActionResult Index(string searchString,
            int page = 1,
            SortStateUser sortOrder = SortStateUser.NameAsc,
            int pageSize = 3)
        {
            IQueryable<User> source = _userManager.Users;

            if (!String.IsNullOrEmpty(searchString))
            {
                source = source.Where(p => p.UserName.Contains(searchString) || p.Email.Contains(searchString));
            }

            switch (sortOrder)
            {
                case SortStateUser.NameDesc:
                    source = source.OrderByDescending(s => s.UserName);
                    break;
                case SortStateUser.BirthdayAsc:
                    source = source.OrderBy(s => s.Birthday);
                    break;
                case SortStateUser.BirthdayDesc:
                    source = source.OrderByDescending(s => s.Birthday);
                    break;
                case SortStateUser.EmailAsc:
                    source = source.OrderBy(s => s.Email);
                    break;
                case SortStateUser.EmailDesc:
                    source = source.OrderByDescending(s => s.Email);
                    break;
                case SortStateUser.SexAsc:
                    source = source.OrderBy(s => s.Sex);
                    break;
                case SortStateUser.SexDesc:
                    source = source.OrderByDescending(s => s.Sex);
                    break;
                default:
                    source = source.OrderBy(s => s.UserName);
                    break;
            }

            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize).ProjectTo<UserViewModel>(_mapper.ConfigurationProvider).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);

            UserListViewModel userListViewModel = new UserListViewModel();
            userListViewModel.UserViewModel = items;
            userListViewModel.PageViewModel = pageViewModel;
            userListViewModel.SortUsersViewModel = new SortUsersViewModel(sortOrder);
            userListViewModel.FilterUsersViewModel = new FilterUsersViewModel(searchString);

            return View(userListViewModel);
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
                User user = _mapper.Map<User>(createUserViewModel);
                var result = await _userManager.CreateAsync(user, createUserViewModel.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(createUserViewModel);
        }

        public async Task<IActionResult> Edit(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            EditUserViewModel model = _mapper.Map<EditUserViewModel>(user);
         
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.UserName;
                    user.Birthday = model.Birthday;
                    user.Sex = model.Sex;
                    if (user.Avatar == null)
                    {
                        user.Avatar = _photoService.AddPhoto(file);
                    }
                    
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Lock(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.SetLockoutEndDateAsync(user, new DateTimeOffset(DateTime.Now.AddYears(100)));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> UnLock(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.SetLockoutEndDateAsync(user, null);
            }
            return RedirectToAction("Index");
        }
    }
}
