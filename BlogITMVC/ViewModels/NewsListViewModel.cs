﻿using System.Collections.Generic;

namespace BlogIT.MVC.ViewModels
{
    public class NewsListViewModel
    {
        public List<ItemNewsListViewModel> ItemNewsListViewModel { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
