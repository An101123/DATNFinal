using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificResearch.Entities
{
    [Table("Level")]

    public class Level : BaseEntity
    {
        public Level() : base()
        {

        }
        [Required]
        public string Name { get; set; }

        [Required]
        public float Score { get; set; }

        public int HoursConverted { get; set; }

        public virtual ICollection<ScientificWork> ScientificWorks { get; set; }
    }
}
