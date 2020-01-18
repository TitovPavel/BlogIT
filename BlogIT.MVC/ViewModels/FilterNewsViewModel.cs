using BlogIT.DB.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace BlogIT.MVC.ViewModels
{
    public class FilterNewsViewModel
    {
        public FilterNewsViewModel(string searchString)
        {
            SearchString = searchString;
        }

        public FilterNewsViewModel(List<Category> listCategories, string searchString, string tags, DateTime dateCalendar, int categoryId = 0, bool findByComments = false)
            :this(searchString)
        {
            Tags = tags;
            CategoryId = categoryId;
            FindByComments = findByComments;
            DateCalendar = dateCalendar;
            listCategories.Insert(0, new Category { Title = "All", Id = 0 });
            Categories = new SelectList(listCategories, "Id", "Title", categoryId);

        }

        public string Tags { get; set; }
        public string SearchString { get; set; }
        public int CategoryId { get; set; }
        public bool FindByComments { get; set; }
        public DateTime DateCalendar { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
