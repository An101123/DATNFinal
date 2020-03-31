using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificResearch.Entities
{
    public class News : BaseEntity
    {
        public News() : base()
        {

        }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string Image { get; set; }

        public string Link { get; set; }
    }
}
