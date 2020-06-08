using System;

namespace BlogIT.MVC.ViewModels
{
    public class NewsAnnotationViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int CategoryId { get; set; }
        public int CountsOfComments { get; set; }
        public int RateAverage { get; set; }

    }
}
