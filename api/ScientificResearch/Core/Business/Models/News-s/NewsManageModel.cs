using ScientificResearch.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificResearch.Core.Business.Models.News_s
{
    public class NewsManageModel : IValidatableObject
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string Image { get; set; }

        public string Link { get; set; }


        public void GetNewsFromModel(News news)
        {
            news.Title = Title;
            news.Content = Content;
            news.Image = Image;
            news.Link = Link;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Title))
            {
                yield return new ValidationResult("Title name is required!", new string[] { "Title" });
            }
        }
    }
}
