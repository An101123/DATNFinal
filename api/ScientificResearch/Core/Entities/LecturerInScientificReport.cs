using ScientificResearch.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificResearch.Core.Entities
{
  
    [Table("LecturerInScientificReport")]
    public class LecturerInScientificReport : BaseEntity
    {
        public LecturerInScientificReport() : base()
        {

        }

        public Guid LecturerId { get; set; }

        public Lecturer Lecturer { get; set; }

        public Guid ScientificReportId { get; set; }

        public ScientificReport ScientificReport { get; set; }
    }
}
