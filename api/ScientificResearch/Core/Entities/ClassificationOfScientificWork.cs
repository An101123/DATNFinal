﻿using ScientificResearch.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificResearch.Core.Entities
{
    
    [Table("ClassificationOfScientificWork")]

    public class ClassificationOfScientificWork : BaseEntity
    {
        public ClassificationOfScientificWork() : base()
        {

        }
        [Required]
        public string Name { get; set; }

        [Required]
        public float Score { get; set; }

        public int HoursConverted { get; set; }

        public virtual ICollection<OtherScientificWork> OtherScientificWorks { get; set; }
    }
}
