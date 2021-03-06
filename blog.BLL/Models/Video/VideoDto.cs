﻿using System;
using System.Collections.Generic;
using System.Text;

namespace blog.BLL.Models.Video
{
    public class VideoDto
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}
