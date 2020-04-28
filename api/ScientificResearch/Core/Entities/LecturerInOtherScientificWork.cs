using ScientificResearch.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificResearch.Core.Entities
{
  
    [Table("LecturerInOtherScientificWork")]
    public class LecturerInOtherScientificWork : BaseEntity
    {
        public LecturerInOtherScientificWork() : base()
        {

        }

        public Guid LecturerId { get; set; }

        public Lecturer Lecturer { get; set; }

        public Guid OtherScientificWorkId { get; set; }

        public OtherScientificWork OtherScientificWork { get; set; }
    }
}
