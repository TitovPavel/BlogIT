using FluentValidation;
using Microsoft.Extensions.Localization;
using BlogIT.MVC.ViewModels;
using System;

namespace BlogIT.MVC.FluentValidation
{
    public class EditNewsViewModelValidator : AbstractValidator<EditNewsViewModel>
    {
        public EditNewsViewModelValidator(IStringLocalizer<EditNewsViewModel> localizer)
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage(localizer["TitleRequired"]);

            RuleFor(x => x.DateTime)
                .NotEmpty()
                .WithMessage(localizer["DateTimeValidator"]);

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage(localizer["DescriptionRequired"]);
     
            RuleFor(x => x.Tags)
                .NotEmpty()
                .WithMessage(localizer["TagsRequired"]);

            RuleFor(x => x.NewsText)
                .NotEmpty()
                .WithMessage(localizer["NewsTextRequired"]);

            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .WithMessage(localizer["CategoryRequired"]);

        }
    }
}
