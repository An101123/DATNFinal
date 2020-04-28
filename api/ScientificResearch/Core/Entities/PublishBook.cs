using ScientificResearch.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificResearch.Core.Entities
{
    public class PublishBook : BaseEntity
    {
        public PublishBook() : base()
        {

        }
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Time { get; set; }

        public string PlaceOfPublication { get; set; }

        public Guid BookCategoryId { get; set; }
        public virtual BookCategory BookCategory { get; set; }
        public List<LecturerInPublishBook> LecturerInPublishBooks { get; set; }


        //public Guid UserId { get; set; }
        //public virtual User User { get; set; }
    }
}
