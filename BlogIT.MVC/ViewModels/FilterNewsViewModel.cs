using BlogIT.DB.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogIT.MVC.ViewModels
{
    public class FilterNewsViewModel
    {
        public FilterNewsViewModel(List<Category> listCategories, string findString, string tags, DateTime dateCalendar, int categoryId = 0, bool findByComments = false)
        {
            Tags = tags;
            FindString = findString;
            CategoryId = categoryId;
            FindByComments = findByComments;
            DateCalendar = dateCalendar;
            listCategories.Insert(0, new Category { Title = "All", Id = 0 });
            Categories = new SelectList(listCategories, "Id", "Title", categoryId);

        }

        public string Tags { get; set; }
        public string FindString { get; set; }
        public int CategoryId { get; set; }
        public bool FindByComments { get; set; }
        public DateTime DateCalendar { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
