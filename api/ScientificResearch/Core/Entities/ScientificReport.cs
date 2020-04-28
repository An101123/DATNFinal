using ScientificResearch.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificResearch.Entities
{
    public class ScientificReport : BaseEntity
    {
        public ScientificReport() : base()
        {

        }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime Time { get; set; }

        public Guid ScientificReportTypeId { get; set; }
        public virtual ScientificReportType ScientificReportType { get; set; }

        public List<LecturerInScientificReport> LecturerInScientificReports { get; set; }

        //public Guid UserId { get; set; }
        //public virtual User User { get; set; }



    }
}
