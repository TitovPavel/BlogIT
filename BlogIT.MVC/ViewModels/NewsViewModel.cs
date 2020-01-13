using System;
using System.ComponentModel.DataAnnotations;

namespace BlogIT.MVC.ViewModels
{
    public class NewsViewModel
    {
        public int Id { get; set; }
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
        public string Category { get; set; }
    }
}
