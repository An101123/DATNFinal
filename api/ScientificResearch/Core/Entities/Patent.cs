using ScientificResearch.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificResearch.Core.Entities
{
    public class Patent : BaseEntity
    {
        public Patent() : base()
        {

        }
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [Required]
        public string Field { get; set; }

        [Required]
        public string PlaceOfIssue { get; set; }

        [Required]
        public float Score { get; set; }

        [Required]
        public int HoursConverted { get; set; }
        public Guid LecturerId { get; set; }
        public virtual Lecturer Lecturer { get; set; }


    }
}
