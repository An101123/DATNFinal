using ScientificResearch.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificResearch.Core.Entities
{
    
    [Table("LecturerInPublishBook")]
    public class LecturerInPublishBook : BaseEntity
    {
        public LecturerInPublishBook() : base()
        {

        }

        public Guid LecturerId { get; set; }

        public Lecturer Lecturer { get; set; }

        public Guid PublishBookId { get; set; }

        public PublishBook PublishBook { get; set; }
    }
}
