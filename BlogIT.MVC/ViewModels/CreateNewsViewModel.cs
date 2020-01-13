using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogIT.MVC.ViewModels
{
    public class CreateNewsViewModel
    {
        [Display(Name = "Title")]
        public string Title { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Date")]
        public DateTime DateTime { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Tags")]
        public string Tags { get; set; }
        [Display(Name = "NewsText")]
        public string NewsText { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
