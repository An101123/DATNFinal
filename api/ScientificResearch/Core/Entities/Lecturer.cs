using ScientificResearch.Core.Entities;
using ScientificResearch.Entities.Enums;
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

        public string AcademicDegree { get; set; }
        public string AcademicRank { get; set; }
        public UserEnums.UserGender? Gender { get; set; }
        [Required]
        public float Total { get; set; }
        public int TotalHour { get; set; }
        public List<LecturerInScientificWork> LecturerInScientificWorks { get; set; }
        public List<LecturerInScientificReport> LecturerInScientificReports { get; set; }
        public List<LecturerInPublishBook> LecturerInPublishBooks { get; set; }
        public List<LecturerInOtherScientificWork> LecturerInOtherScientificWorks { get; set; }
        public virtual ICollection<StudyGuide> StudyGuides { get; set; }
    }
}
