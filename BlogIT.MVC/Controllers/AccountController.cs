using AutoMapper;
using BlogIT.DB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using BlogIT.MVC.ViewModels;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using BlogIT.DB.BL;

namespace BlogIT.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AccountController> _localizer;
        private readonly IPhotoService _photoService;
        private readonly IEmailService _emailService;

        public AccountController(UserManager<User> userManager, 
            SignInManager<User> signInManager, 
            IMapper mapper, 
            IStringLocalizer<AccountController> localizer,
            IPhotoService photoService,
            IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _localizer = localizer;
            _photoService = photoService;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                User user = _mapper.Map<User>(registerViewModel);

                var result = await _userManager.CreateAsync(user, registerViewModel.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "user");

                    var plus18Claim = new Claim(ClaimTypes.DateOfBirth, registerViewModel.Birthday.ToString(), typeof(DateTime).ToString());
                    await _userManager.AddClaimAsync(user, plus18Claim);

                    var gender = new Claim(ClaimTypes.Gender, registerViewModel.Sex.ToString(), typeof(String).ToString());
                    await _userManager.AddClaimAsync(user, gender);

                    string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action(
                        "ConfirmEmail",
                        "Account",
                        new { userId = user.Id, code = code },
                        protocol: HttpContext.Request.Scheme);
  
                    await _emailService.SendEmailAsync(user.Email, "Confirm your account",
                        $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>");

                    return RedirectToAction("Confirm", "Account");

                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(registerViewModel);
        }

        [HttpGet]
        public IActionResult Confirm()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");
            else
                return View("Error");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user = await _userManager.FindByNameAsync(model.Name);
                if (user != null)
                {
                    if (!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        ModelState.AddModelError(string.Empty, "Вы не подтвердили свой email");
                        return View(model);
                    }
                }

                var result = await _signInManager.PasswordSignInAsync(model.Name, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
   
                    return RedirectToAction("Index", "Home");
  
                }
                else
                {
                    ModelState.AddModelError("", _localizer["IncorrectUsername"]);
                }
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "user, admin")]
        public async Task<IActionResult> ProfileSettings(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ProfileSettingsViewModel model = _mapper.Map<ProfileSettingsViewModel>(user);
           
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "user, admin")]
        public async Task<IActionResult> ProfileSettings(ProfileSettingsViewModel model, IFormFile file)
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
                    if (file != null)
                    {
                        if (model.AvatarExist)
                        {
                            _photoService.UpdatePhoto((int)user.AvatarId, file);
                        }
                        else
                        {
                            user.Avatar = _photoService.AddPhoto(file);
                        }
                    }
                    else if(file == null && model.AvatarExist && HttpContext.Request.Form.Keys.Contains("file"))
                    {
                        _photoService.DeletePhotoFromUser(user);
                    }

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
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

        [HttpGet]
        [Authorize(Roles = "user, admin")]
        public async Task<IActionResult> Profile(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ProfileViewModel model = _mapper.Map<ProfileViewModel>(user);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        
        [HttpGet]
        public IActionResult AccessDenied(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
    }
}
