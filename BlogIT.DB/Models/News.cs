using System;

namespace BlogIT.DB.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public string NewsText { get; set; }
        public User Writer { get; set; }
        public string WriterId { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }

    }
}
