using Microsoft.AspNetCore.Razor.TagHelpers;
using BlogIT.MVC.ViewModels;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;

namespace BlogIT.MVC.Helpers
{
    public class NewsTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;
        public NewsTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }


        public NewsAnnotationViewModel NewsView { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);

            output.TagName = "div";
            output.Attributes.SetAttribute("class", "article");

            TagBuilder tagDivRow = new TagBuilder("div");
            tagDivRow.AddCssClass("row");

            TagBuilder tagDivCol = new TagBuilder("div");
            tagDivCol.AddCssClass("col-md-10 ml-auto mr-auto");

            TagBuilder tagDivCard = new TagBuilder("div");
            tagDivCard.AddCssClass("row card card-blog card-plain text-center");

            TagBuilder tagDivCardBody = new TagBuilder("div");
            tagDivCardBody.AddCssClass("card-body col-md-12 ml-auto mr-auto");

            TagBuilder tagTitle = new TagBuilder("a");
            tagTitle.Attributes["href"] = urlHelper.Action(ActionName, ControllerName, new  {id = NewsView.Id });

            TagBuilder tagCardTitle = new TagBuilder("h3");
            tagCardTitle.AddCssClass("card-title");
            tagCardTitle.InnerHtml.Append(NewsView.Title);


            tagTitle.InnerHtml.AppendHtml(tagCardTitle);


            tagDivCardBody.InnerHtml.AppendHtml(tagTitle);

            TagBuilder tagDivCardCategory = new TagBuilder("div");
            tagDivCardCategory.AddCssClass("card-category");

            TagBuilder tagACategory = new TagBuilder("a");
            tagACategory.Attributes["href"] = urlHelper.Action("List", "Home", new { CategoryId = NewsView.CategoryId });


            TagBuilder tagSpan = new TagBuilder("span");
            tagSpan.AddCssClass("badge badge-primary main-tag");
            tagSpan.InnerHtml.Append(NewsView.Category);

            tagACategory.InnerHtml.AppendHtml(tagSpan);


            tagDivCardCategory.InnerHtml.AppendHtml(tagACategory);


            TagBuilder tagDivDateTime = new TagBuilder("div");
            tagDivDateTime.AddCssClass("pull-left");

            TagBuilder tagDateTime = new TagBuilder("h6");
            tagDateTime.AddCssClass("text-muted");
            tagDateTime.InnerHtml.Append(NewsView.DateTime.ToString("d"));


            tagDivDateTime.InnerHtml.AppendHtml(tagDateTime);

            tagDivCardCategory.InnerHtml.AppendHtml(tagDivDateTime);

          
                TagBuilder tagDivPullRight = new TagBuilder("div");
                tagDivPullRight.AddCssClass("pull-right");

                TagBuilder tagAComments = new TagBuilder("a");
                tagAComments.Attributes["href"] = urlHelper.Action(ActionName, ControllerName, new { id = NewsView.Id }, null, null, "chatList");


                TagBuilder tagCountComments = new TagBuilder("h6");
                tagCountComments.AddCssClass("text-muted");
                tagCountComments.InnerHtml.Append(NewsView.CountsOfComments.ToString() + " comments");

                tagAComments.InnerHtml.AppendHtml(tagCountComments);

                tagDivPullRight.InnerHtml.AppendHtml(tagAComments);




            TagBuilder tagDivStars = new TagBuilder("div");
            
            for(int i = 1; i <= 5 ; i++)
            {
                TagBuilder tagDivStar = new TagBuilder("img");
                tagDivStar.AddCssClass("small-rating");

                if(i <= NewsView.RateAverage)
                { 
                    tagDivStar.Attributes["src"] = "/Files/FilledStar.png";
                }
                else
                {
                    tagDivStar.Attributes["src"] = "/Files/EmptyStar.png";
                }

                tagDivStars.InnerHtml.AppendHtml(tagDivStar);
            }


            tagDivPullRight.InnerHtml.AppendHtml(tagDivStars);



            tagDivCardCategory.InnerHtml.AppendHtml(tagDivPullRight);



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