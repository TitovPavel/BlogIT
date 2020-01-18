using FluentValidation;
using Microsoft.Extensions.Localization;
using BlogIT.MVC.ViewModels;

namespace BlogIT.MVC.FluentValidation
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator(IStringLocalizer<LoginViewModel> localizer)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(localizer["NameRequired"]);


            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(localizer["PasswordRequired"]);

        }
    }
}
