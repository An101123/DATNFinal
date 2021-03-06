﻿using ScientificResearch.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificResearch.Core.Business.Models.News_s
{
    public class NewsViewModel
    {
        public NewsViewModel()
        {

        }

        public NewsViewModel(News news) : this()
        {
            if (news != null)
            {
                Id = news.Id;
                Title = news.Title;
                Content = news.Content;
                Image = news.Image;
                Link = news.Link;
            }
        }
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Image { get; set; }

        public string Link { get; set; }
    }
}
