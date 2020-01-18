using System.Collections.Generic;

namespace BlogIT.MVC.ViewModels
{
    public class HomePageViewModel
    {      
        public List<NewsAnnotationViewModel> LastNews { get; set; }
        public List<NewsAnnotationViewModel> TopNews { get; set; }

    }
}
