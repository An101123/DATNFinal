using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificResearch.Entities
{
    public class Lecturer : BaseEntity
    {
        public Lecturer() : base()
        {
            Total = 0;
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Faculty { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public string ScientificWorkId { get; set; }

        public string ScientificReportId { get; set; }

        [Required]
        public int Total { get; set; }
        public virtual ICollection<ScientificWork> ScientificWorks { get; set; }

        public virtual ICollection<ScientificReport> ScientificReports { get; set; }
    }
}
