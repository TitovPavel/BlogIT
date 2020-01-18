using System;
using System.Collections.Generic;
using System.Text;

namespace BlogIT.DB.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public List<NewsTag> NewsTag { get; set; }
    }
}
