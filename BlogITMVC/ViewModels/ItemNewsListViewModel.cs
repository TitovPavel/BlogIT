using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogIT.MVC.ViewModels
{
    public class ItemNewsListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
        public string Writer { get; set; }
        public string Category { get; set; }
    }
}
