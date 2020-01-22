using BlogIT.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogIT.DB.Specifications
{
    public class NewsFilterPaginatedSpecification : BaseSpecification<News>
    {
        public NewsFilterPaginatedSpecification(int skip, int take, string searchString, List<String> tags, DateTime dateCalendar, int categoryId = 0, bool findByComments = false)
            : base(p => (dateCalendar == DateTime.MinValue || p.DateTime.Date == dateCalendar.Date) &&
                (categoryId == 0 || p.CategoryId == categoryId) &&
                (tags.Count == 0 || p.NewsTag.Any(s => tags.Contains(s.Tag.Title.ToUpper()))) &&
                (String.IsNullOrEmpty(searchString) || p.Title.Contains(searchString) || p.Description.Contains(searchString) || p.NewsText.Contains(searchString) || (findByComments && p.ChatMessages.Any(p => p.Message.Contains(searchString))))
           )
        {
            AddInclude(p => p.ChatMessages);
            ApplyPaging(skip, take);
        }
    }
}
