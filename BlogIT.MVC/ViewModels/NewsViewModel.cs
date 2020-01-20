using System;
using System.Collections.Generic;
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
        public int CategoryId { get; set; }
        public List<TagViewModel> ListTag { get; set; }
        public int RateCount { get; set; }
        public int RateAverage { get; set; }

    }
}
