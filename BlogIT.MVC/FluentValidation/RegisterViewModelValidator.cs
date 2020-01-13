using FluentValidation;
using Microsoft.Extensions.Localization;
using BlogIT.MVC.ViewModels;
using System;

namespace BlogIT.MVC.FluentValidation
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator(IStringLocalizer<RegisterViewModel> localizer)
        {

            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage(localizer["UserNameRequired"]);

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(localizer["EmailNameRequired"])
                .EmailAddress()
                .WithMessage(localizer["EmailValidator"]);

            RuleFor(x => x.Birthday)
                .NotEmpty()
                .WithMessage(localizer["BirthdayValidator"])
                .LessThan(p => DateTime.Now)
                .WithMessage(localizer["BirthdayMore"]);

            RuleFor(x => x.Sex)
                .NotEmpty()
                .WithMessage(localizer["SexRequired"]);

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(localizer["PasswordRequired"])
                .Equal(x => x.PasswordConfirm)
                .WithMessage(localizer["PasswordEqual"]);

        }
    }
}
