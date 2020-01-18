using System;
using System.Collections.Generic;
using System.Text;

namespace BlogIT.DB.Models
{
    public class Category
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public List<News> News { get; set; }
    }
}
