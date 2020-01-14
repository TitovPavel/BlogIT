using Microsoft.AspNetCore.Razor.TagHelpers;
using BlogIT.MVC.ViewModels;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogIT.MVC.Helpers
{
    public class NewsTagHelper : TagHelper
    {

        public NewsAnnotationViewModel NewsView { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "article");

            TagBuilder tagDivRow = new TagBuilder("div");
            tagDivRow.AddCssClass("row");

            TagBuilder tagDivCol = new TagBuilder("div");
            tagDivCol.AddCssClass("col-md-8 ml-auto mr-auto");

            TagBuilder tagDivCard = new TagBuilder("div");
            tagDivCard.AddCssClass("card card-blog card-plain text-center");

            TagBuilder tagDivCardBody = new TagBuilder("div");
            tagDivCardBody.AddCssClass("card-body");

            TagBuilder tagTitle = new TagBuilder("a");
            tagTitle.Attributes.Add("href", String.Concat("/", ControllerName, "/", ActionName, "/", NewsView.Id));

            TagBuilder tagCardTitle = new TagBuilder("h3");
            tagCardTitle.AddCssClass("card-title");
            tagCardTitle.InnerHtml.Append(NewsView.Title);


            tagTitle.InnerHtml.AppendHtml(tagCardTitle);


            tagDivCardBody.InnerHtml.AppendHtml(tagTitle);

            TagBuilder tagDivCardCategory = new TagBuilder("div");
            tagDivCardCategory.AddCssClass("card-category");

            TagBuilder tagSpan = new TagBuilder("span");
            tagSpan.AddCssClass("badge badge-primary main-tag");
            tagSpan.InnerHtml.Append(NewsView.Category);


            tagDivCardCategory.InnerHtml.AppendHtml(tagSpan);


            TagBuilder tagDivDateTime = new TagBuilder("div");
            tagDivDateTime.AddCssClass("pull-left");

            TagBuilder tagDateTime = new TagBuilder("h6");
            tagDateTime.AddCssClass("text-muted");
            tagDateTime.InnerHtml.Append(NewsView.DateTime.ToString("d"));


            tagDivDateTime.InnerHtml.AppendHtml(tagDateTime);

            tagDivCardCategory.InnerHtml.AppendHtml(tagDivDateTime);


            tagDivCardBody.InnerHtml.AppendHtml(tagDivCardCategory);

            TagBuilder tagDivCardDescription = new TagBuilder("div");
            tagDivCardDescription.AddCssClass("card-description");

            TagBuilder tagPDescription = new TagBuilder("p");

            tagPDescription.InnerHtml.Append(NewsView.Description);

            tagDivCardDescription.InnerHtml.AppendHtml(tagPDescription);


            tagDivCardBody.InnerHtml.AppendHtml(tagDivCardDescription);


            tagDivCard.InnerHtml.AppendHtml(tagDivCardBody);

            tagDivCol.InnerHtml.AppendHtml(tagDivCard);

            tagDivRow.InnerHtml.AppendHtml(tagDivCol);

            output.Content.AppendHtml(tagDivRow);
        }
    }
}