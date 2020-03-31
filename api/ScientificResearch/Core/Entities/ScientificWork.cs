﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificResearch.Entities
{
    public class ScientificWork : BaseEntity
    {
        public ScientificWork() : base()
        {

        }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime Time { get; set; }

        public Guid LevelId { get; set; }
        public virtual Level Level { get; set; }

        public Guid LecturerId { get; set; }
        public virtual Lecturer Lecturer { get; set; }

        //public Guid UserId { get; set; }
        //public virtual User User { get; set; }
    }
}
