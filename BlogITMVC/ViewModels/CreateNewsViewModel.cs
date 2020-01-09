using System;
using System.ComponentModel.DataAnnotations;

namespace BlogIT.MVC.ViewModels
{
    public class CreateNewsViewModel
    {
        [Display(Name = "Title")]
        public string Title { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "DateTime")]
        public DateTime DateTime { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Tags")]
        public string Tags { get; set; }
        [Display(Name = "NewsText")]
        public string NewsText { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
    }
}
