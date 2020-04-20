using ScientificResearch.Core.Entities;
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
       
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Faculty { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public Guid ScientificWorkId { get; set; }

        public Guid ScientificReportId { get; set; }

        public Guid PublishBookId { get; set; }

        public Guid StudyGuideId { get; set; }
        public Guid OtherScientificWorkId { get; set; }

        [Required]
        public float Total { get; set; }
        public int TotalHour { get; set; }
        public virtual ICollection<ScientificWork> ScientificWorks { get; set; }

        public virtual ICollection<ScientificReport> ScientificReports { get; set; }

        public virtual ICollection<PublishBook> PublishBooks { get; set; }

        public virtual ICollection<StudyGuide> StudyGuides { get; set; }
        public virtual ICollection<OtherScientificWork> OtherScientificWorks { get; set; }
    }
}
