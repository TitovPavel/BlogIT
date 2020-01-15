﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BlogIT.DB.Models
{
    public class NewsTag
    {
        public int NewsId { get; set; }
        public News News { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
