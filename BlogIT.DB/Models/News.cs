using System;
using System.Collections.Generic;

namespace BlogIT.DB.Models
{
    public class News : BaseEntity
    {
        public string Title { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public string NewsText { get; set; }
        public User Writer { get; set; }
        public string WriterId { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public bool Deleted { get; set; }
        public List<ChatMessage> ChatMessages { get; set; }
        public List<NewsTag> NewsTag { get; set; }
        public List<Rating> Ratings { get; set; }
        public int RateCount { get; set; }
        public int RateAverage { get; set; }

    }
}
