using ScientificResearch.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificResearch.Core.Entities
{
    [Table("LecturerInScientificWork")]
    public class LecturerInScientificWork : BaseEntity
    {
        public LecturerInScientificWork() : base()
        {

        }

        public Guid LecturerId { get; set; }

        public Lecturer Lecturer { get; set; }

        public Guid ScientificWorkId { get; set; }

        public ScientificWork ScientificWork { get; set; }
    }
}
