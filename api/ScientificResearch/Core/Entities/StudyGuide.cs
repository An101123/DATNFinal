using ScientificResearch.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificResearch.Core.Entities
{
    public class StudyGuide : BaseEntity
    {
        public StudyGuide() : base()
        {

        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Literacy { get; set; }
        
        [Required]
        public string PlaceOfTraining { get; set; }

        [Required]
        public DateTime InstructionTime { get; set; }

        [Required]
        public DateTime GraduationTime { get; set; }
        public Guid LevelStudyGuideId { get; set; }
        public virtual LevelStudyGuide LevelStudyGuide { get; set; }

        public Guid LecturerId { get; set; }
        public virtual Lecturer Lecturer { get; set; }

        //public Guid UserId { get; set; }
        //public virtual User User { get; set; }
    }
}
