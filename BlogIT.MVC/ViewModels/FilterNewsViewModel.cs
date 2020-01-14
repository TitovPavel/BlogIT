using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogIT.MVC.ViewModels
{
    public class FilterNewsViewModel
    {
        public FilterNewsViewModel(string findString, string tags, int categoryId = 0, bool findByComments = false)
        {
            Tags = tags;
            FindString = findString;
            CategoryId = categoryId;
            FindByComments = findByComments;

        }

        public string Tags { get; set; }
        public string FindString { get; set; }
        public int CategoryId { get; set; }
        public bool FindByComments { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
