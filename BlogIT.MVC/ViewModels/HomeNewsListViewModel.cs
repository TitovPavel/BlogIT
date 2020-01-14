using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogIT.MVC.ViewModels
{
    public class HomeNewsListViewModel
    {
        public List<NewsAnnotationViewModel> NewsAnnotation { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterNewsViewModel FilterNewsViewModel { get; set; }
    }
}
