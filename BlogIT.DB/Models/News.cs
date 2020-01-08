using System;

namespace BlogIT.DB.Models
{
    public class News
    {
        public String Title { get; set; }
        public DateTime DateTime { get; set; }
        public String Description { get; set; }
        public Category Category { get; set; }
        public string CategoryId { get; set; }
        public String Tags { get; set; }
        public String NewsText { get; set; }
    }
}
