using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificResearch.Entities
{
    public class ScientificReportType : BaseEntity
    {
        public ScientificReportType() : base()
        {

        }
        [Required]
        public string Name { get; set; }

        [Required]
        public float Score { get; set; }

        public int HoursConverted { get; set; }

        public virtual ICollection<ScientificReport> ScientificReports { get; set; }

    }
}
