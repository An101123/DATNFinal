using ScientificResearch.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificResearch.Core.Entities
{
    public class OtherScientificWork : BaseEntity
    {
        public OtherScientificWork() : base()
        {

        }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Time { get; set; }

        public Guid ClassificationOfScientificWorkId { get; set; }
        public virtual ClassificationOfScientificWork ClassificationOfScientificWork { get; set; }

        public List<LecturerInOtherScientificWork> LecturerInOtherScientificWorks { get; set; }
        //public Guid UserId { get; set; }
        //public virtual User User { get; set; }
    }
}
